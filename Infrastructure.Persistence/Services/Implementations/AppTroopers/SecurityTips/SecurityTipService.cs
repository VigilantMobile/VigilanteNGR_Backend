using Application.Enums;
using Application.Features.AppTroopers.SecurityTips;
using Application.Features.AppTroopers.SecurityTips.Commands;
using Application.Features.AppTroopers.SecurityTips.Commands.CreateSecurityTip;
using Application.Features.Location;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Application.Interfaces.Repositories.Location;
using Application.Services.Interfaces.AppTroopers.SecurityTips;
using Application.Services.Interfaces.Location;
using Application.Services.Interfaces.UserProfile;
using Domain.Common.Enums;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.Identity;
using Domain.Entities.LocationEntities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories.Location;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services.Implementations.AppTroopers.SecurityTips
{
    public class SecurityTipService : ISecurityTipService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStateRepositoryAsync _stateRepositoryAsync;
        private readonly ILGARepositoryAsync _lGARepositoryAsync;
        private readonly ITownRepositoryAsync _townRepositoryAsync;
        private readonly ISecurityTipEligibilityService _securityTipEligibilityService;
        private readonly ISecurityTipRepositoryAsync _securityTipRepositoryAsync;
        private readonly IEscalatedTipsRepositoryAsync _escalatedTipsRepositoryAsync;
        private readonly IAlertLevelRepositoryAsync _alertLevelRespositoryAsync;
        private readonly ISecurityTipCategoryRepositoryAsync _securityTipCategoryRepositoryAsync;
        private readonly IGeoCodingService _geocodingService;
        private readonly ILogger _logger;
        private readonly ICustomerService _customerService;

        public SecurityTipService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, 
            IStateRepositoryAsync stateRepositoryAsync, 
            ILGARepositoryAsync lGARepositoryAsync,
            ITownRepositoryAsync townRepositoryAsync, 
            ISecurityTipEligibilityService securityTipEligibilityService,  ILogger logger,
            ISecurityTipRepositoryAsync securityTipRepositoryAsync,
            IEscalatedTipsRepositoryAsync escalatedTipsRepositoryAsync,
            IAlertLevelRepositoryAsync alertLevelRespositoryAsync,
            ISecurityTipCategoryRepositoryAsync securityTipCategoryRepositoryAsync,
            IGeoCodingService geocodingService,
            ICustomerService customerService
           )

        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _stateRepositoryAsync = stateRepositoryAsync;
            _lGARepositoryAsync = lGARepositoryAsync;
            _townRepositoryAsync = townRepositoryAsync; 
            _securityTipEligibilityService = securityTipEligibilityService; 
            _securityTipRepositoryAsync = securityTipRepositoryAsync;
            _escalatedTipsRepositoryAsync = escalatedTipsRepositoryAsync;
            _alertLevelRespositoryAsync = alertLevelRespositoryAsync;
            _securityTipCategoryRepositoryAsync = securityTipCategoryRepositoryAsync;
            _geocodingService = geocodingService;
            _customerService = customerService;
        }

        public async Task<CreateSecurityTipResponse> CreateSecurityTipAsync(CreateSecurityTipCommand securityTipRequest)
        {
            CreateSecurityTipResponse createSecurityTipResponse = new CreateSecurityTipResponse();
            try
            {
                // Get the user to check if active
                var user = await _userManager.FindByIdAsync(securityTipRequest.BroadcasterUserId);
                if (user == null || !user.isActive)
                {
                    createSecurityTipResponse.IsCreated = false;
                    createSecurityTipResponse.Message = "User is not authorized to create security tips";
                    return createSecurityTipResponse;
                }

                // Get source - default to VGNGAUser for regular users
                var source = await _context.Sources
                    .FirstOrDefaultAsync(x => x.SourceName == SourcesEnum.VGNGAUser.ToString());

                if (source == null)
                {
                    createSecurityTipResponse.IsCreated = false;
                    createSecurityTipResponse.Message = "Invalid source configuration";
                    return createSecurityTipResponse;
                }

                // Determine town/city for the incident location
                var incidentLocationInfo = await _geocodingService.GetCustomerLiveAddresses(securityTipRequest.IncidentCoordinates);
                if (incidentLocationInfo == null || incidentLocationInfo.status == APIResponseStatus.fail.ToString())
                {
                    createSecurityTipResponse.IsCreated = false;
                    createSecurityTipResponse.Message = "Could not determine incident location from coordinates";
                    return createSecurityTipResponse;
                }

                // Get or create townId from geocoding results
                Guid incidentTownId;
                try
                {
                    incidentTownId = await _geocodingService.GetOrCreateTownIdAsync(
                        incidentLocationInfo.Data.TownOrDistrict,
                        incidentLocationInfo.Data.CountryOrDistrictOrLGA,
                        incidentLocationInfo.Data.StateOrProvinceOrRegion,
                        incidentLocationInfo.Data.Country);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to get or create town for coordinates {Coordinates}",
                        securityTipRequest.IncidentCoordinates);
                    createSecurityTipResponse.IsCreated = false;
                    createSecurityTipResponse.Message = "Failed to identify town for incident location";
                    return createSecurityTipResponse;
                }

                // Get user's registered town/city if available
                string userRegisteredTownId = null;
                var customerProfile = await _customerService.GetCustomerProfileAsync(securityTipRequest.BroadcasterUserId);
                if (customerProfile?.userLocation?.city != null)
                {
                    userRegisteredTownId = customerProfile.userLocation.city.cityId;
                }

                // Check if the incident is in the user's registered city
                bool isInRegisteredCity = false;
                if (!string.IsNullOrEmpty(userRegisteredTownId) && userRegisteredTownId == incidentTownId.ToString())
                {
                    isInRegisteredCity = true;
                }

                // Calculate distance between user's current location and incident location
                bool isNearIncident = false;
                if (!string.IsNullOrEmpty(securityTipRequest.UserCurrentCoordinates))
                {
                    double distance = CalculateDistanceInMiles(
                        securityTipRequest.IncidentCoordinates,
                        securityTipRequest.UserCurrentCoordinates);

                    isNearIncident = distance <= 10; // Within 10 miles
                }

                // Apply validation rules
                if (!isInRegisteredCity && !isNearIncident)
                {
                    createSecurityTipResponse.IsCreated = false;
                    createSecurityTipResponse.Message = "Security tip can only be created for your registered city or within 10 miles of your current location";
                    return createSecurityTipResponse;
                }

                // Determine alert level based on category and casualties
                var categoryId = Guid.Parse(securityTipRequest.CategoryId);
                var alertLevel = await DetermineAlertLevelAsync(categoryId, securityTipRequest.Casualties);

                // Create security tip
                var securityTip = new SecurityTip
                {
                    Subject = securityTipRequest.Subject,
                    Body = securityTipRequest.Description,
                    AlertLevel = alertLevel,
                    Coordinates = securityTipRequest.IncidentCoordinates,
                    BroadcasterId = securityTipRequest.BroadcasterUserId,
                    SecurityTipCategoryId = categoryId,
                    Casualties = securityTipRequest.Casualties,
                    SourceId = source.Id,
                    TownId = incidentTownId, // Save the town ID explicitly
                    CreatedBy = securityTipRequest.BroadcasterUserId,
                    Created = DateTime.UtcNow.AddHours(1),
                    IsBroadcasted = true,
                    Status = SecurityTipStatusEnum.Approved,
                    IncidentDateTime = securityTipRequest.IncidentDateTime ?? DateTime.UtcNow,
                    IsOngoing = securityTipRequest.IsOngoing,
                    BroadcasterType = BroadcasterTypeEnum.VigilantUser,
                    // Initialize vote and view counts
                    UpvoteCount = 0,
                    DownvoteCount = 0,
                    ViewCount = 0,
                    Comments = new List<Comment>(), // Initialize empty comments collection
                    Votes = new List<SecurityTipVote>() // Initialize empty votes collection
                };


                // Get default BroadcasterTypeId
                var broadcasterType = await _context.BroadcasterTypes
                    .FirstOrDefaultAsync(bt => bt.Name == BroadcasterTypeEnum.VigilantUser.ToString());
                if (broadcasterType != null)
                {
                    securityTip.BroadcasterTypeId = broadcasterType.Id;
                }

                // Save the tip
                var savedTip = await _securityTipRepositoryAsync.AddAsync(securityTip);

                // Prepare response
                createSecurityTipResponse.SecurityTipStatus = savedTip.Status.ToString();
                createSecurityTipResponse.IsDispatched = savedTip.IsBroadcasted;
                createSecurityTipResponse.IsCreated = true;
                createSecurityTipResponse.Message = "Security Tip Created Successfully";

                return createSecurityTipResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving security tip for user {UserId}",
                    securityTipRequest.BroadcasterUserId);

                createSecurityTipResponse.IsCreated = false;
                createSecurityTipResponse.Message = "An error occurred while creating the security tip. Please try again later.";
                return createSecurityTipResponse;
            }
        }

        // Helper method to calculate distance between two coordinates
        private double CalculateDistanceInMiles(string coordinates1, string coordinates2)
        {
            try
            {
                // Parse coordinates
                var parts1 = coordinates1.Split(',');
                var parts2 = coordinates2.Split(',');

                if (parts1.Length != 2 || parts2.Length != 2)
                    throw new ArgumentException("Coordinates must be in format 'latitude,longitude'");

                double lat1 = double.Parse(parts1[0]);
                double lon1 = double.Parse(parts1[1]);
                double lat2 = double.Parse(parts2[0]);
                double lon2 = double.Parse(parts2[1]);

                // Haversine formula to calculate distance between two points on Earth
                const double EarthRadius = 3958.8; // Earth radius in miles

                double dLat = ToRadians(lat2 - lat1);
                double dLon = ToRadians(lon2 - lon1);

                double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                          Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                          Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

                return EarthRadius * c;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating distance between coordinates");
                return double.MaxValue; // Return a large distance to fail validation
            }
        }

        private double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }


        /// <summary>
        /// Creates a security tip with additional admin options (such as source selection)
        /// </summary>
        /// <param name="securityTipRequest">The security tip details including source selection</param>
        /// <returns>Response indicating success or failure</returns>
        public async Task<CreateSecurityTipResponse> CreateSecurityTipForAdminAsync(CreateSecurityTipCommand securityTipRequest)
        {
            CreateSecurityTipResponse createSecurityTipResponse = new CreateSecurityTipResponse();
            try
            {
                // Get the admin user to check if active and has admin role
                var user = await _userManager.FindByIdAsync(securityTipRequest.BroadcasterUserId);
                var userRoles = await _userManager.GetRolesAsync(user);

                if (user == null || !user.isActive ||
                    !(userRoles.Contains(Roles.SuperAdmin.ToString()) ||
                      userRoles.Contains(Roles.Admin.ToString()) ||
                      userRoles.Contains(Roles.Moderator.ToString())))
                {
                    createSecurityTipResponse.IsCreated = false;
                    createSecurityTipResponse.Message = "User is not authorized to create security tips as admin";
                    return createSecurityTipResponse;
                }

                // Get source from request instead of hardcoding
                var source = await _context.Sources.FindAsync(Guid.Parse(securityTipRequest.SourceId));
                if (source == null)
                {
                    createSecurityTipResponse.IsCreated = false;
                    createSecurityTipResponse.Message = "Invalid source selection";
                    return createSecurityTipResponse;
                }

                // Determine alert level
                var categoryId = Guid.Parse(securityTipRequest.CategoryId);
                var alertLevel = await DetermineAlertLevelAsync(categoryId, securityTipRequest.Casualties);

                // Get town from coordinates
                string townId = null;
                var locationDetails = await _geocodingService.GetCustomerLiveAddresses(securityTipRequest.IncidentCoordinates);
                if (locationDetails != null && locationDetails.Data != null)
                {
                    // Get or create town ID from geocoding results
                    townId = (await _geocodingService.GetOrCreateTownIdAsync(
                        locationDetails.Data.TownOrDistrict,
                        locationDetails.Data.CountryOrDistrictOrLGA,
                        locationDetails.Data.StateOrProvinceOrRegion,
                        locationDetails.Data.Country)).ToString();
                }

                if (string.IsNullOrEmpty(townId))
                {
                    createSecurityTipResponse.IsCreated = false;
                    createSecurityTipResponse.Message = "Could not determine town from coordinates";
                    return createSecurityTipResponse;
                }

                // Create security tip
                var securityTip = new SecurityTip
                {
                    Subject = securityTipRequest.Subject,
                    Body = securityTipRequest.Description,
                    AlertLevel = alertLevel, // Using enum directly
                    Coordinates = securityTipRequest.IncidentCoordinates,
                    BroadcasterId = securityTipRequest.BroadcasterUserId,
                    SecurityTipCategoryId = categoryId,
                    Casualties = securityTipRequest.Casualties,
                    SourceId = source.Id,
                    TownId = Guid.Parse(townId),
                    CreatedBy = securityTipRequest.BroadcasterUserId,
                    Created = DateTime.UtcNow.AddHours(1),
                    IsBroadcasted = true, // Auto-approve for admin users
                    Status = SecurityTipStatusEnum.Approved,
                    IncidentDateTime = securityTipRequest.IncidentDateTime ?? DateTime.UtcNow,
                    IsOngoing = securityTipRequest.IsOngoing,
                    BroadcasterType = BroadcasterTypeEnum.SecurityAuthority,
                    BroadcasterTypeId = await GetBroadcasterTypeIdByEnum(BroadcasterTypeEnum.SecurityAuthority)
                };

                // Save the tip
                var savedTip = await _securityTipRepositoryAsync.AddAsync(securityTip);

                // Prepare response
                createSecurityTipResponse.SecurityTipStatus = savedTip.Status.ToString();
                createSecurityTipResponse.IsDispatched = savedTip.IsBroadcasted;
                createSecurityTipResponse.IsCreated = true;
                createSecurityTipResponse.Message = "Security Tip Created Successfully by Admin";

                return createSecurityTipResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while admin saving security tip for user {UserId}",
                    securityTipRequest.BroadcasterUserId);

                createSecurityTipResponse.IsCreated = false;
                createSecurityTipResponse.Message = "An error occurred while creating the security tip. Please try again later.";
                return createSecurityTipResponse;
            }
        }

        private async Task<Guid> GetBroadcasterTypeIdByEnum(BroadcasterTypeEnum broadcasterType)
        {
            var type = await _context.BroadcasterTypes
                .FirstOrDefaultAsync(bt => bt.Name == broadcasterType.ToString());

            if (type == null)
            {
                throw new KeyNotFoundException($"BroadcasterType {broadcasterType} not found in database");
            }

            return type.Id;
        }

        public async Task<AlertLevelEnum> DetermineAlertLevelAsync(Guid categoryId, int casualties)
        {
            try
            {
                // If there are casualties, automatically set to Critical
                if (casualties > 0)
                {
                    return AlertLevelEnum.Critical;
                }

                // Get the category and its type
                var category = await _securityTipCategoryRepositoryAsync.GetByIdAsync(categoryId.ToString());
                if (category == null)
                {
                    _logger.LogWarning($"Category with ID {categoryId} was not found");
                    return AlertLevelEnum.Neutral; // Default to Neutral if category not found
                }

                var categoryType = await _context.SecurityTipCategoryTypes
                    .FirstOrDefaultAsync(ct => ct.Id == category.CategoryTypeId);

                if (categoryType == null)
                {
                    _logger.LogWarning($"Category type not found for category ID {categoryId}");
                    return AlertLevelEnum.Neutral; // Default to Neutral if category type not found
                }

                // Determine alert level based on category type
                switch (categoryType.Name)
                {
                    case "Property Crime":
                        return AlertLevelEnum.Moderate;

                    case "Public Order":
                        // Check if it's "Suspicious Activity" category
                        if (category.Name == "Suspicious Activity")
                        {
                            return AlertLevelEnum.Neutral;
                        }
                        return AlertLevelEnum.Moderate;

                    case "Public Safety":
                        return AlertLevelEnum.Moderate;

                    case "Terrorism & Major Threats":
                        return AlertLevelEnum.High;

                    case "Violence and Crime":
                        return AlertLevelEnum.High;

                    case "Environmental/Natural":
                        return AlertLevelEnum.Moderate;

                    default:
                        return AlertLevelEnum.Neutral; // Default case
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while determining alert level for category ID {CategoryId}", categoryId);
                return AlertLevelEnum.Neutral; // Default to Neutral in case of error
            }
        }
        public async Task<GetSecurityTipResponse> GetSecurityTipByIdAsync(string securityTipId)
        {
            try
            {
                var securityTip = await (from tip in _context.SecurityTips
                                        .Include(st => st.Comments)
                                            .ThenInclude(c => c.ApplicationUser)
                                        .Include(st => st.Votes)
                                         join broadcaster in _context.Users on tip.BroadcasterId equals broadcaster.Id
                                         join category in _context.SecurityTipCategories on tip.SecurityTipCategoryId equals category.Id
                                         join categoryType in _context.SecurityTipCategoryTypes on category.CategoryTypeId equals categoryType.Id
                                         where tip.Id == Guid.Parse(securityTipId)
                                         select new GetSecurityTipResponse
                                         {
                                             Id = tip.Id.ToString(),
                                             Subject = tip.Subject,
                                             Description = tip.Body,
                                             Coordinates = tip.Coordinates,
                                             SecurityTipStatus = tip.Status.ToString(),
                                             BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                             AlertLevel = tip.AlertLevel.ToString(),
                                             IsBroadcasted = tip.IsBroadcasted,
                                             Created = tip.Created,
                                             UpvoteCount = tip.UpvoteCount.ToString(),
                                             DownvoteCount = tip.DownvoteCount.ToString(),
                                             ViewCount = tip.ViewCount.ToString(),
                                             AlertCategoryType = new AlertCategoryType
                                             {
                                                 Id = categoryType.Id.ToString(),
                                                 Name = categoryType.Name,
                                                 Description = categoryType.Description
                                             },
                                             SecurityTipCategory = new AlertCategory
                                             {
                                                 Id = category.Id.ToString(),
                                                 Name = category.Name,
                                                 Description = category.Description
                                             },
                                             Broadcaster = new Broadcaster
                                             {
                                                 Id = broadcaster.Id,
                                                 FullName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                                 ProfilePhotoUrl = broadcaster.CustomerProfileUrl
                                             },
                                             Comments = tip.Comments.Select(c => new CommentViewModel
                                             {
                                                 Id = c.Id.ToString(),
                                                 Comment = c.UserComment,
                                                 CommenterId = c.CommenterId,
                                                 CommenterName = $"{c.ApplicationUser.FirstName} {c.ApplicationUser.LastName}",
                                                 CommenterProfileUrl = c.ApplicationUser.CustomerProfileUrl,
                                                 Created = c.Created,
                                                 UpvoteCount = c.UpvoteCount,
                                                 DownvoteCount = c.DownvoteCount
                                             }).ToList()
                                         }).FirstOrDefaultAsync();

                if (securityTip == null)
                {
                    _logger.LogWarning($"Security tip with ID {securityTipId} was not found");
                    return null;
                }

                // Get location details using coordinates
                var locationDetails = await _geocodingService.GetCustomerLiveAddresses(securityTip.Coordinates);
                if (locationDetails != null && locationDetails.Data != null)
                {
                    securityTip.AlertLocation = new AlertLocation
                    {
                        City = locationDetails.Data.TownOrDistrict,
                        StateOrProvince = locationDetails.Data.StateOrProvinceOrRegion,
                        Country = locationDetails.Data.Country
                    };
                }

                // Increment view count
                await IncrementViewCount(securityTipId);

                return securityTip;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving security tip with ID {securityTipId}: {ex.StackTrace}");
                return null;
            }
        }

        public async Task<GetSecurityTipResponse> GetSecurityTipDetailByIdAsync(string securityTipId)
        {
            try
            {
                var securityTip = await (from tip in _context.SecurityTips
                                        .Include(st => st.Comments)
                                        .Include(st => st.Votes)
                                         join broadcaster in _context.Users on tip.BroadcasterId equals broadcaster.Id
                                         join category in _context.SecurityTipCategories on tip.SecurityTipCategoryId equals category.Id
                                         join categoryType in _context.SecurityTipCategoryTypes on category.CategoryTypeId equals categoryType.Id
                                         where tip.Id == Guid.Parse(securityTipId)
                                         select new GetSecurityTipResponse
                                         {
                                             Id = tip.Id.ToString(),
                                             Subject = tip.Subject,
                                             Description = tip.Body,
                                             Coordinates = tip.Coordinates,
                                             SecurityTipStatus = tip.Status.ToString(),
                                             BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                             AlertLevel = tip.AlertLevel.ToString(),
                                             IsBroadcasted = tip.IsBroadcasted,
                                             UpvoteCount = tip.UpvoteCount.ToString(),
                                             DownvoteCount = tip.DownvoteCount.ToString(),
                                             ViewCount = tip.ViewCount.ToString(),
                                             AlertCategoryType = new AlertCategoryType
                                             {
                                                 Id = categoryType.Id.ToString(),
                                                 Name = categoryType.Name,
                                                 Description = categoryType.Description
                                             },
                                             SecurityTipCategory = new AlertCategory
                                             {
                                                 Id = category.Id.ToString(),
                                                 Name = category.Name,
                                                 Description = category.Description
                                             },
                                             Broadcaster = new Broadcaster
                                             {
                                                 Id = broadcaster.Id,
                                                 FullName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                                 ProfilePhotoUrl = broadcaster.CustomerProfileUrl
                                             },
                                             Comments = tip.Comments.Select(c => new CommentViewModel
                                             {
                                                 Id = c.Id.ToString(),
                                                 Comment = c.UserComment,
                                                 CommenterId = c.CommenterId,
                                                 CommenterName = $"{c.ApplicationUser.FirstName} {c.ApplicationUser.LastName}",
                                                 CommenterProfileUrl = c.ApplicationUser.CustomerProfileUrl,
                                                 Created = c.Created,
                                                 UpvoteCount = c.UpvoteCount,
                                                 DownvoteCount = c.DownvoteCount
                                             }).ToList()
                                         }).FirstOrDefaultAsync();

                if (securityTip == null)
                {
                    _logger.LogWarning($"Security tip with ID {securityTipId} was not found");
                    return null;
                }

                // Get location details using coordinates
                var locationDetails = await _geocodingService.GetCustomerLiveAddresses(securityTip.Coordinates);
                if (locationDetails != null && locationDetails.Data != null)
                {
                    securityTip.AlertLocation = new AlertLocation
                    {
                        City = locationDetails.Data.TownOrDistrict,
                        StateOrProvince = locationDetails.Data.StateOrProvinceOrRegion,
                        Country = locationDetails.Data.Country
                    };
                }

                // Increment view count
                await IncrementViewCount(securityTipId);

                return securityTip;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving security tip with ID {securityTipId}: {ex.StackTrace}");
                return null;
            }
        }

        private async Task IncrementViewCount(string securityTipId)
        {
            try
            {
                var tip = await _context.SecurityTips.FindAsync(Guid.Parse(securityTipId));
                if (tip != null)
                {
                    tip.ViewCount++;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error incrementing view count");
            }
        }

        public async Task<GetSecurityTipsListResponse> GetSecurityTipsPostedByUser(string userId, int pageNumber, int pageSize)
        {
            GetSecurityTipsListResponse response = new GetSecurityTipsListResponse();

            try
            {
                var userSecurityTips = await (from securityTip in _context.SecurityTips
                                              .Include(st => st.Comments)
                                              .Include(st => st.Votes)
                                              join broadcaster in _context.Users on securityTip.BroadcasterId equals broadcaster.Id
                                              join category in _context.SecurityTipCategories on securityTip.SecurityTipCategoryId equals category.Id
                                              join categoryType in _context.SecurityTipCategoryTypes on category.CategoryTypeId equals categoryType.Id
                                              where broadcaster.Id == userId
                                              orderby securityTip.Created descending
                                              select new GetSecurityTipResponse
                                              {
                                                  Id = securityTip.Id.ToString(),
                                                  Subject = securityTip.Subject,
                                                  Description = securityTip.Body,
                                                  Coordinates = securityTip.Coordinates,
                                                  SecurityTipStatus = securityTip.Status.ToString(),
                                                  BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                                  AlertLevel = securityTip.AlertLevel.ToString(),
                                                  IsBroadcasted = securityTip.IsBroadcasted,
                                                  UpvoteCount = securityTip.UpvoteCount.ToString(),
                                                  DownvoteCount = securityTip.DownvoteCount.ToString(),
                                                  ViewCount = securityTip.ViewCount.ToString(),
                                                  AlertCategoryType = new AlertCategoryType
                                                  {
                                                      Id = categoryType.Id.ToString(),
                                                      Name = categoryType.Name,
                                                      Description = categoryType.Description
                                                  },
                                                  SecurityTipCategory = new AlertCategory
                                                  {
                                                      Id = category.Id.ToString(),
                                                      Name = category.Name,
                                                      Description = category.Description
                                                  },
                                                  Broadcaster = new Broadcaster
                                                  {
                                                      Id = broadcaster.Id,
                                                      FullName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                                      ProfilePhotoUrl = broadcaster.CustomerProfileUrl
                                                  },
                                                  Comments = securityTip.Comments.Select(c => new CommentViewModel
                                                  {
                                                      Id = c.Id.ToString(),
                                                      Comment = c.UserComment,
                                                      CommenterId = c.CommenterId,
                                                      CommenterName = $"{c.ApplicationUser.FirstName} {c.ApplicationUser.LastName}",
                                                      CommenterProfileUrl = c.ApplicationUser.CustomerProfileUrl,
                                                      Created = c.Created,
                                                      UpvoteCount = c.UpvoteCount,
                                                      DownvoteCount = c.DownvoteCount
                                                  }).ToList()
                                              })
                                              .Skip((pageNumber - 1) * pageSize)
                                              .Take(pageSize)
                                              .AsNoTracking()
                                              .ToListAsync();

                if (userSecurityTips == null || !userSecurityTips.Any())
                {
                    response.Success = true;
                    response.Message = $"No security tips found for user with ID {userId}";
                    response.SecurityTipsList = new List<GetSecurityTipResponse>();
                    return response;
                }

                response.SecurityTipsList = userSecurityTips;
                response.Success = true;

                // Enrich with location details
                foreach (var securityTip in response.SecurityTipsList)
                {
                    var locationDetails = await _geocodingService.GetCustomerLiveAddresses(securityTip.Coordinates);
                    if (locationDetails?.Data != null)
                    {
                        securityTip.AlertLocation = new AlertLocation
                        {
                            City = locationDetails.Data.TownOrDistrict,
                            StateOrProvince = locationDetails.Data.StateOrProvinceOrRegion,
                            Country = locationDetails.Data.Country
                        };
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving security tips for user {UserId}", userId);
                response.Success = false;
                response.Message = "An error occurred while retrieving security tips. Please try again later.";
                return response;
            }
        }


        public async Task<GetSecurityTipsListResponse> GetSecurityTipsForUserLiveLocationCity(string userId, string coordinates, int pageNumber, int pageSize)
        {
            GetSecurityTipsListResponse response = new GetSecurityTipsListResponse();

            try
            {
                // Get location info from coordinates
                var customerLiveLocation = await _geocodingService.GetCustomerLiveAddresses(coordinates);
                if (customerLiveLocation == null || customerLiveLocation.status == APIResponseStatus.fail.ToString())
                {
                    response.Success = false;
                    response.Message = customerLiveLocation?.Message ?? "Failed to determine location from coordinates";
                    return response;
                }

                // Get town/city name from coordinates
                string townName = customerLiveLocation.Data?.TownOrDistrict;
                if (string.IsNullOrWhiteSpace(townName))
                {
                    response.Success = false;
                    response.Message = "Could not determine town/city from the provided coordinates";
                    return response;
                }

                // Find town in database
                var town = await _context.Towns.FirstOrDefaultAsync(x => x.Name == townName);
                if (town == null)
                {
                    _logger.LogWarning($"Town/city with name '{townName}' not found in database for coordinates {coordinates}");
                    response.Success = false;
                    response.Message = $"Town '{townName}' does not exist in our records";
                    return response;
                }

                // Get security tips for the town/city with included relationships
                var tips = await _context.SecurityTips
                    .Include(st => st.Comments)
                        .ThenInclude(c => c.ApplicationUser)
                    .Include(st => st.Votes)
                    .Include(st => st.ApplicationUser)
                    .Include(st => st.SecurityTipCategory)
                        .ThenInclude(c => c.CategoryType)
                    .Where(st => st.TownId == town.Id)
                    .OrderByDescending(st => st.Created)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(tip => new GetSecurityTipResponse
                    {
                        Id = tip.Id.ToString(),
                        Subject = tip.Subject,
                        Description = tip.Body,
                        Coordinates = tip.Coordinates,
                        SecurityTipStatus = tip.Status.ToString(),
                        BroadcasterName = $"{tip.ApplicationUser.FirstName} {tip.ApplicationUser.LastName}",
                        AlertLevel = tip.AlertLevel.ToString(),
                        IsBroadcasted = tip.IsBroadcasted,
                        Created = tip.Created,
                        UpvoteCount = tip.UpvoteCount.ToString(),
                        DownvoteCount = tip.DownvoteCount.ToString(),
                        ViewCount = tip.ViewCount.ToString(),
                        AlertCategoryType = new AlertCategoryType
                        {
                            Id = tip.SecurityTipCategory.CategoryType.Id.ToString(),
                            Name = tip.SecurityTipCategory.CategoryType.Name,
                            Description = tip.SecurityTipCategory.CategoryType.Description
                        },
                        SecurityTipCategory = new AlertCategory
                        {
                            Id = tip.SecurityTipCategory.Id.ToString(),
                            Name = tip.SecurityTipCategory.Name,
                            Description = tip.SecurityTipCategory.Description
                        },
                        Broadcaster = new Broadcaster
                        {
                            Id = tip.ApplicationUser.Id,
                            FullName = $"{tip.ApplicationUser.FirstName} {tip.ApplicationUser.LastName}",
                            ProfilePhotoUrl = tip.ApplicationUser.CustomerProfileUrl
                        },
                        Comments = tip.Comments.Select(c => new CommentViewModel
                        {
                            Id = c.Id.ToString(),
                            Comment = c.UserComment,
                            CommenterId = c.CommenterId,
                            CommenterName = $"{c.ApplicationUser.FirstName} {c.ApplicationUser.LastName}",
                            CommenterProfileUrl = c.ApplicationUser.CustomerProfileUrl,
                            Created = c.Created,
                            UpvoteCount = c.UpvoteCount,
                            DownvoteCount = c.DownvoteCount
                        }).ToList()
                    })
                    .ToListAsync();

                if (!tips.Any())
                {
                    return new GetSecurityTipsListResponse
                    {
                        Success = true,
                        SecurityTipsList = new List<GetSecurityTipResponse>(),
                        Message = $"No security tips found for {townName}"
                    };
                }

                // Enrich with location details
                foreach (var tip in tips)
                {
                    // Get location details using coordinates
                    var locationDetails = await _geocodingService.GetCustomerLiveAddresses(tip.Coordinates);
                    if (locationDetails?.Data != null)
                    {
                        tip.AlertLocation = new AlertLocation
                        {
                            City = locationDetails.Data.TownOrDistrict,
                            StateOrProvince = locationDetails.Data.StateOrProvinceOrRegion,
                            Country = locationDetails.Data.Country
                        };
                    }

                    // Increment view count
                    await IncrementViewCount(tip.Id);
                }

                return new GetSecurityTipsListResponse
                {
                    Success = true,
                    SecurityTipsList = tips,
                    Message = $"Found {tips.Count} security tips for {townName}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving security tips for live location city {Coordinates}", coordinates);
                response.Success = false;
                response.Message = "An error occurred while retrieving security tips. Please try again later.";
                return response;
            }
        }

        public async Task<GetSecurityTipsListResponse> GetSecurityTipsForUserRegisteredCityOnly(string userId, int pageNumber, int pageSize)
        {
            GetSecurityTipsListResponse response = new GetSecurityTipsListResponse();

            try
            {
                // Get the user's profile to find their registered town/city
                var customerProfile = await _customerService.GetCustomerProfileAsync(userId);
                if (customerProfile == null || string.IsNullOrEmpty(customerProfile.userLocation?.city?.cityId))
                {
                    response.Success = false;
                    response.Message = "User profile or city information not found";
                    return response;
                }

                string townId = customerProfile.userLocation.city.cityId;

                // Get the town details
                var townDetails = await _townRepositoryAsync.GetByIdAsync(townId);
                if (townDetails == null)
                {
                    response.Success = false;
                    response.Message = "Town information not found";
                    return response;
                }

                // Get town-level tips with related data
                var tips = await _context.SecurityTips
                    .Include(st => st.Comments)
                        .ThenInclude(c => c.ApplicationUser)
                    .Include(st => st.Votes)
                    .Include(st => st.ApplicationUser)
                    .Include(st => st.SecurityTipCategory)
                        .ThenInclude(c => c.CategoryType)
                    .Where(st => st.TownId == Guid.Parse(townId))
                    .OrderByDescending(st => st.Created)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(tip => new GetSecurityTipResponse
                    {
                        Id = tip.Id.ToString(),
                        Subject = tip.Subject,
                        Description = tip.Body,
                        Coordinates = tip.Coordinates,
                        SecurityTipStatus = tip.Status.ToString(),
                        BroadcasterName = $"{tip.ApplicationUser.FirstName} {tip.ApplicationUser.LastName}",
                        AlertLevel = tip.AlertLevel.ToString(),
                        IsBroadcasted = tip.IsBroadcasted,
                        Created = tip.Created,
                        UpvoteCount = tip.UpvoteCount.ToString(),
                        DownvoteCount = tip.DownvoteCount.ToString(),
                        ViewCount = tip.ViewCount.ToString(),
                        AlertCategoryType = new AlertCategoryType
                        {
                            Id = tip.SecurityTipCategory.CategoryType.Id.ToString(),
                            Name = tip.SecurityTipCategory.CategoryType.Name,
                            Description = tip.SecurityTipCategory.CategoryType.Description
                        },
                        SecurityTipCategory = new AlertCategory
                        {
                            Id = tip.SecurityTipCategory.Id.ToString(),
                            Name = tip.SecurityTipCategory.Name,
                            Description = tip.SecurityTipCategory.Description
                        },
                        Broadcaster = new Broadcaster
                        {
                            Id = tip.ApplicationUser.Id,
                            FullName = $"{tip.ApplicationUser.FirstName} {tip.ApplicationUser.LastName}",
                            ProfilePhotoUrl = tip.ApplicationUser.CustomerProfileUrl
                        },
                        Comments = tip.Comments.Select(c => new CommentViewModel
                        {
                            Id = c.Id.ToString(),
                            Comment = c.UserComment,
                            CommenterId = c.CommenterId,
                            CommenterName = $"{c.ApplicationUser.FirstName} {c.ApplicationUser.LastName}",
                            CommenterProfileUrl = c.ApplicationUser.CustomerProfileUrl,
                            Created = c.Created,
                            UpvoteCount = c.UpvoteCount,
                            DownvoteCount = c.DownvoteCount
                        }).ToList()
                    })
                    .ToListAsync();

                if (!tips.Any())
                {
                    return new GetSecurityTipsListResponse
                    {
                        Success = true,
                        SecurityTipsList = new List<GetSecurityTipResponse>(),
                        Message = $"No security tips found for your registered town/city {townDetails.Name}"
                    };
                }

                // Enrich with location details and increment view counts
                foreach (var tip in tips)
                {
                    var locationDetails = await _geocodingService.GetCustomerLiveAddresses(tip.Coordinates);
                    if (locationDetails?.Data != null)
                    {
                        tip.AlertLocation = new AlertLocation
                        {
                            City = locationDetails.Data.TownOrDistrict,
                            StateOrProvince = locationDetails.Data.StateOrProvinceOrRegion,
                            Country = locationDetails.Data.Country
                        };
                    }

                    // Increment view count
                    await IncrementViewCount(tip.Id);
                }

                return new GetSecurityTipsListResponse
                {
                    Success = true,
                    SecurityTipsList = tips,
                    Message = $"Found {tips.Count} security tips for your registered town/city {townDetails.Name}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving security tips for user's registered city {UserId}", userId);
                response.Success = false;
                response.Message = "An error occurred while retrieving security tips. Please try again later.";
                return response;
            }
        }

        public async Task<GetSecurityTipResponse> ToggleCommentVoteAsync(string userId, string commentId, CommentVoteEnum voteType)
        {
            try
            {
                var comment = await _context.Comments
                    .Include(c => c.SecurityTip)
                        .ThenInclude(st => st.Comments)
                            .ThenInclude(c => c.ApplicationUser)
                    .Include(c => c.SecurityTip)
                        .ThenInclude(st => st.Votes)
                    .Include(c => c.SecurityTip)
                        .ThenInclude(st => st.ApplicationUser)
                    .Include(c => c.SecurityTip)
                        .ThenInclude(st => st.SecurityTipCategory)
                            .ThenInclude(c => c.CategoryType)
                    .FirstOrDefaultAsync(c => c.Id == Guid.Parse(commentId));

                if (comment == null)
                {
                    _logger.LogWarning($"Comment with ID {commentId} not found");
                    return null;
                }

                // Check if the user has already voted
                var existingVote = comment.CommentVote;

                if (existingVote == voteType)
                {
                    // If same vote type, remove vote
                    if (voteType == CommentVoteEnum.Upvote)
                    {
                        comment.UpvoteCount = Math.Max(0, comment.UpvoteCount - 1);
                    }
                    else
                    {
                        comment.DownvoteCount = Math.Max(0, comment.DownvoteCount - 1);
                    }
                    comment.CommentVote = CommentVoteEnum.None; // Reset the vote
                }
                else
                {
                    // If different vote type, update vote
                    if (existingVote == CommentVoteEnum.Upvote)
                    {
                        comment.UpvoteCount = Math.Max(0, comment.UpvoteCount - 1);
                    }
                    else if (existingVote == CommentVoteEnum.Downvote)
                    {
                        comment.DownvoteCount = Math.Max(0, comment.DownvoteCount - 1);
                    }

                    if (voteType == CommentVoteEnum.Upvote)
                    {
                        comment.UpvoteCount++;
                    }
                    else
                    {
                        comment.DownvoteCount++;
                    }
                    comment.CommentVote = voteType; // Update the vote
                }

                await _context.SaveChangesAsync();

                var tip = comment.SecurityTip;
                return new GetSecurityTipResponse
                {
                    Id = tip.Id.ToString(),
                    Subject = tip.Subject,
                    Description = tip.Body,
                    Coordinates = tip.Coordinates,
                    SecurityTipStatus = tip.Status.ToString(),
                    BroadcasterName = $"{tip.ApplicationUser.FirstName} {tip.ApplicationUser.LastName}",
                    AlertLevel = tip.AlertLevel.ToString(),
                    IsBroadcasted = tip.IsBroadcasted,
                    Created = tip.Created,
                    UpvoteCount = tip.UpvoteCount.ToString(),
                    DownvoteCount = tip.DownvoteCount.ToString(),
                    ViewCount = tip.ViewCount.ToString(),
                    AlertCategoryType = new AlertCategoryType
                    {
                        Id = tip.SecurityTipCategory.CategoryType.Id.ToString(),
                        Name = tip.SecurityTipCategory.CategoryType.Name,
                        Description = tip.SecurityTipCategory.CategoryType.Description
                    },
                    SecurityTipCategory = new AlertCategory
                    {
                        Id = tip.SecurityTipCategory.Id.ToString(),
                        Name = tip.SecurityTipCategory.Name,
                        Description = tip.SecurityTipCategory.Description
                    },
                    Broadcaster = new Broadcaster
                    {
                        Id = tip.ApplicationUser.Id,
                        FullName = $"{tip.ApplicationUser.FirstName} {tip.ApplicationUser.LastName}",
                        ProfilePhotoUrl = tip.ApplicationUser.CustomerProfileUrl
                    },
                    Comments = tip.Comments.Select(c => new CommentViewModel
                    {
                        Id = c.Id.ToString(),
                        Comment = c.UserComment,
                        CommenterId = c.CommenterId,
                        CommenterName = $"{c.ApplicationUser.FirstName} {c.ApplicationUser.LastName}",
                        CommenterProfileUrl = c.ApplicationUser.CustomerProfileUrl,
                        Created = c.Created,
                        UpvoteCount = c.UpvoteCount,
                        DownvoteCount = c.DownvoteCount
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling comment vote for comment {CommentId} by user {UserId}", commentId, userId);
                return null;
            }
        }


        //Comments
        public async Task<GetSecurityTipResponse> ToggleSecurityTipVoteAsync(string userId, string securityTipId, CommentVoteEnum voteType)
        {
            try
            {
                var tip = await _context.SecurityTips
                    .Include(st => st.Comments)
                        .ThenInclude(c => c.ApplicationUser)
                    .Include(st => st.Votes)
                    .Include(st => st.ApplicationUser)
                    .Include(st => st.SecurityTipCategory)
                        .ThenInclude(c => c.CategoryType)
                    .FirstOrDefaultAsync(st => st.Id == Guid.Parse(securityTipId));

                if (tip == null)
                {
                    _logger.LogWarning($"Security tip with ID {securityTipId} not found");
                    return null;
                }

                var existingVote = await _context.SecurityTipVotes
                    .FirstOrDefaultAsync(v => v.SecurityTipId == tip.Id && v.VoterId == userId);

                if (existingVote != null)
                {
                    // If same vote type, remove vote
                    if (existingVote.VoteType == voteType)
                    {
                        _context.SecurityTipVotes.Remove(existingVote);
                        if (voteType == CommentVoteEnum.Upvote)
                            tip.UpvoteCount--;
                        else
                            tip.DownvoteCount--;
                    }
                    // If different vote type, update vote
                    else
                    {
                        existingVote.VoteType = voteType;
                        if (voteType == CommentVoteEnum.Upvote)
                        {
                            tip.UpvoteCount++;
                            tip.DownvoteCount--;
                        }
                        else
                        {
                            tip.DownvoteCount++;
                            tip.UpvoteCount--;
                        }
                    }
                }
                else
                {
                    // Add new vote
                    var vote = new SecurityTipVote
                    {
                        SecurityTipId = tip.Id,
                        VoterId = userId,
                        VoteType = voteType,
                        Created = DateTime.UtcNow,
                        CreatedBy = userId
                    };
                    await _context.SecurityTipVotes.AddAsync(vote);

                    if (voteType == CommentVoteEnum.Upvote)
                        tip.UpvoteCount++;
                    else
                        tip.DownvoteCount++;
                }

                await _context.SaveChangesAsync();

                // Return detailed response
                return new GetSecurityTipResponse
                {
                    Id = tip.Id.ToString(),
                    Subject = tip.Subject,
                    Description = tip.Body,
                    Coordinates = tip.Coordinates,
                    SecurityTipStatus = tip.Status.ToString(),
                    BroadcasterName = $"{tip.ApplicationUser.FirstName} {tip.ApplicationUser.LastName}",
                    AlertLevel = tip.AlertLevel.ToString(),
                    IsBroadcasted = tip.IsBroadcasted,
                    Created = tip.Created,
                    UpvoteCount = tip.UpvoteCount.ToString(),
                    DownvoteCount = tip.DownvoteCount.ToString(),
                    ViewCount = tip.ViewCount.ToString(),
                    AlertCategoryType = new AlertCategoryType
                    {
                        Id = tip.SecurityTipCategory.CategoryType.Id.ToString(),
                        Name = tip.SecurityTipCategory.CategoryType.Name,
                        Description = tip.SecurityTipCategory.CategoryType.Description
                    },
                    SecurityTipCategory = new AlertCategory
                    {
                        Id = tip.SecurityTipCategory.Id.ToString(),
                        Name = tip.SecurityTipCategory.Name,
                        Description = tip.SecurityTipCategory.Description
                    },
                    Broadcaster = new Broadcaster
                    {
                        Id = tip.ApplicationUser.Id,
                        FullName = $"{tip.ApplicationUser.FirstName} {tip.ApplicationUser.LastName}",
                        ProfilePhotoUrl = tip.ApplicationUser.CustomerProfileUrl
                    },
                    Comments = tip.Comments.Select(c => new CommentViewModel
                    {
                        Id = c.Id.ToString(),
                        Comment = c.UserComment,
                        CommenterId = c.CommenterId,
                        CommenterName = $"{c.ApplicationUser.FirstName} {c.ApplicationUser.LastName}",
                        CommenterProfileUrl = c.ApplicationUser.CustomerProfileUrl,
                        Created = c.Created,
                        UpvoteCount = c.UpvoteCount,
                        DownvoteCount = c.DownvoteCount
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling security tip vote");
                return null;
            }
        }
        public async Task<GetSecurityTipResponse> CreateCommentAsync(string userId, string securityTipId, string commentText)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null || !user.isActive)
                {
                    return null;
                }

                var tip = await _context.SecurityTips
                    .Include(st => st.Comments)
                        .ThenInclude(c => c.ApplicationUser)
                    .Include(st => st.Votes)
                    .Include(st => st.ApplicationUser)
                    .Include(st => st.SecurityTipCategory)
                        .ThenInclude(c => c.CategoryType)
                    .FirstOrDefaultAsync(st => st.Id == Guid.Parse(securityTipId));

                if (tip == null)
                {
                    return null;
                }

                var comment = new Comment
                {
                    UserComment = commentText,
                    CommenterId = userId,
                    SecurityTipId = tip.Id,
                    Created = DateTime.UtcNow,
                    CreatedBy = userId,
                    UpvoteCount = 0,
                    DownvoteCount = 0,
                    CommentVote = CommentVoteEnum.None
                };

                tip.Comments.Add(comment);
                await _context.SaveChangesAsync();

                // Return full tip response with updated comments
                return new GetSecurityTipResponse
                {
                    Id = tip.Id.ToString(),
                    Subject = tip.Subject,
                    Description = tip.Body,
                    Coordinates = tip.Coordinates,
                    SecurityTipStatus = tip.Status.ToString(),
                    BroadcasterName = $"{tip.ApplicationUser.FirstName} {tip.ApplicationUser.LastName}",
                    AlertLevel = tip.AlertLevel.ToString(),
                    IsBroadcasted = tip.IsBroadcasted,
                    Created = tip.Created,
                    UpvoteCount = tip.UpvoteCount.ToString(),
                    DownvoteCount = tip.DownvoteCount.ToString(),
                    ViewCount = tip.ViewCount.ToString(),
                    AlertCategoryType = new AlertCategoryType
                    {
                        Id = tip.SecurityTipCategory.CategoryType.Id.ToString(),
                        Name = tip.SecurityTipCategory.CategoryType.Name,
                        Description = tip.SecurityTipCategory.CategoryType.Description
                    },
                    SecurityTipCategory = new AlertCategory
                    {
                        Id = tip.SecurityTipCategory.Id.ToString(),
                        Name = tip.SecurityTipCategory.Name,
                        Description = tip.SecurityTipCategory.Description
                    },
                    Broadcaster = new Broadcaster
                    {
                        Id = tip.ApplicationUser.Id,
                        FullName = $"{tip.ApplicationUser.FirstName} {tip.ApplicationUser.LastName}",
                        ProfilePhotoUrl = tip.ApplicationUser.CustomerProfileUrl
                    },
                    Comments = tip.Comments.Select(c => new CommentViewModel
                    {
                        Id = c.Id.ToString(),
                        Comment = c.UserComment,
                        CommenterId = c.CommenterId,
                        CommenterName = $"{c.ApplicationUser.FirstName} {c.ApplicationUser.LastName}",
                        CommenterProfileUrl = c.ApplicationUser.CustomerProfileUrl,
                        Created = c.Created,
                        UpvoteCount = c.UpvoteCount,
                        DownvoteCount = c.DownvoteCount
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating comment for security tip {SecurityTipId} by user {UserId}", securityTipId, userId);
                return null;
            }
        }

        public async Task<GetSecurityTipResponse> UpdateCommentAsync(string userId, string commentId, string updatedComment)
        {
            try
            {
                var comment = await _context.Comments
                    .Include(c => c.SecurityTip)
                        .ThenInclude(st => st.Comments)
                            .ThenInclude(c => c.ApplicationUser)
                    .Include(c => c.SecurityTip)
                        .ThenInclude(st => st.Votes)
                    .Include(c => c.SecurityTip)
                        .ThenInclude(st => st.ApplicationUser)
                    .Include(c => c.SecurityTip)
                        .ThenInclude(st => st.SecurityTipCategory)
                            .ThenInclude(c => c.CategoryType)
                    .FirstOrDefaultAsync(c => c.Id == Guid.Parse(commentId));

                if (comment == null)
                {
                    return null;
                }

                // Only the comment creator can update it
                if (comment.CommenterId != userId)
                {
                    return null;
                }

                comment.UserComment = updatedComment;
                comment.LastModified = DateTime.UtcNow;
                comment.LastModifiedBy = userId;

                await _context.SaveChangesAsync();

                var tip = comment.SecurityTip;
                return new GetSecurityTipResponse
                {
                    Id = tip.Id.ToString(),
                    Subject = tip.Subject,
                    Description = tip.Body,
                    Coordinates = tip.Coordinates,
                    SecurityTipStatus = tip.Status.ToString(),
                    BroadcasterName = $"{tip.ApplicationUser.FirstName} {tip.ApplicationUser.LastName}",
                    AlertLevel = tip.AlertLevel.ToString(),
                    IsBroadcasted = tip.IsBroadcasted,
                    Created = tip.Created,
                    UpvoteCount = tip.UpvoteCount.ToString(),
                    DownvoteCount = tip.DownvoteCount.ToString(),
                    ViewCount = tip.ViewCount.ToString(),
                    AlertCategoryType = new AlertCategoryType
                    {
                        Id = tip.SecurityTipCategory.CategoryType.Id.ToString(),
                        Name = tip.SecurityTipCategory.CategoryType.Name,
                        Description = tip.SecurityTipCategory.CategoryType.Description
                    },
                    SecurityTipCategory = new AlertCategory
                    {
                        Id = tip.SecurityTipCategory.Id.ToString(),
                        Name = tip.SecurityTipCategory.Name,
                        Description = tip.SecurityTipCategory.Description
                    },
                    Broadcaster = new Broadcaster
                    {
                        Id = tip.ApplicationUser.Id,
                        FullName = $"{tip.ApplicationUser.FirstName} {tip.ApplicationUser.LastName}",
                        ProfilePhotoUrl = tip.ApplicationUser.CustomerProfileUrl
                    },
                    Comments = tip.Comments.Select(c => new CommentViewModel
                    {
                        Id = c.Id.ToString(),
                        Comment = c.UserComment,
                        CommenterId = c.CommenterId,
                        CommenterName = $"{c.ApplicationUser.FirstName} {c.ApplicationUser.LastName}",
                        CommenterProfileUrl = c.ApplicationUser.CustomerProfileUrl,
                        Created = c.Created,
                        UpvoteCount = c.UpvoteCount,
                        DownvoteCount = c.DownvoteCount
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating comment {CommentId} by user {UserId}", commentId, userId);
                return null;
            }
        }

        public async Task<GetSecurityTipResponse> DeleteCommentAsync(string userId, string securityTipId, string commentId)
        {
            try
            {
                var comment = await _context.Comments
                    .Include(c => c.SecurityTip)
                        .ThenInclude(st => st.Comments)
                            .ThenInclude(c => c.ApplicationUser)
                    .Include(c => c.SecurityTip)
                        .ThenInclude(st => st.Votes)
                    .Include(c => c.SecurityTip)
                        .ThenInclude(st => st.ApplicationUser)
                    .Include(c => c.SecurityTip)
                        .ThenInclude(st => st.SecurityTipCategory)
                            .ThenInclude(c => c.CategoryType)
                    .FirstOrDefaultAsync(c => c.Id == Guid.Parse(commentId));

                if (comment == null)
                {
                    return null;
                }

                // Check if user is either the comment creator or the security tip owner
                if (comment.CommenterId != userId && comment.SecurityTip.BroadcasterId != userId)
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    var userRoles = await _userManager.GetRolesAsync(user);

                    // Allow admins and moderators to delete comments
                    if (!userRoles.Any(r => r == Roles.Admin.ToString() ||
                                          r == Roles.SuperAdmin.ToString() ||
                                          r == Roles.Moderator.ToString()))
                    {
                        return null;
                    }
                }

                var tip = comment.SecurityTip;
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();

                return new GetSecurityTipResponse
                {
                    Id = tip.Id.ToString(),
                    Subject = tip.Subject,
                    Description = tip.Body,
                    Coordinates = tip.Coordinates,
                    SecurityTipStatus = tip.Status.ToString(),
                    BroadcasterName = $"{tip.ApplicationUser.FirstName} {tip.ApplicationUser.LastName}",
                    AlertLevel = tip.AlertLevel.ToString(),
                    IsBroadcasted = tip.IsBroadcasted,
                    Created = tip.Created,
                    UpvoteCount = tip.UpvoteCount.ToString(),
                    DownvoteCount = tip.DownvoteCount.ToString(),
                    ViewCount = tip.ViewCount.ToString(),
                    AlertCategoryType = new AlertCategoryType
                    {
                        Id = tip.SecurityTipCategory.CategoryType.Id.ToString(),
                        Name = tip.SecurityTipCategory.CategoryType.Name,
                        Description = tip.SecurityTipCategory.CategoryType.Description
                    },
                    SecurityTipCategory = new AlertCategory
                    {
                        Id = tip.SecurityTipCategory.Id.ToString(),
                        Name = tip.SecurityTipCategory.Name,
                        Description = tip.SecurityTipCategory.Description
                    },
                    Broadcaster = new Broadcaster
                    {
                        Id = tip.ApplicationUser.Id,
                        FullName = $"{tip.ApplicationUser.FirstName} {tip.ApplicationUser.LastName}",
                        ProfilePhotoUrl = tip.ApplicationUser.CustomerProfileUrl
                    },
                    Comments = tip.Comments.Select(c => new CommentViewModel
                    {
                        Id = c.Id.ToString(),
                        Comment = c.UserComment,
                        CommenterId = c.CommenterId,
                        CommenterName = $"{c.ApplicationUser.FirstName} {c.ApplicationUser.LastName}",
                        CommenterProfileUrl = c.ApplicationUser.CustomerProfileUrl,
                        Created = c.Created,
                        UpvoteCount = c.UpvoteCount,
                        DownvoteCount = c.DownvoteCount
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting comment {CommentId} from security tip {SecurityTipId} by user {UserId}",
                    commentId, securityTipId, userId);
                return null;
            }
        }


        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////Not in use
        /// </summary>
        /// <param name="StateId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// 

        public async Task<GetSecurityTipsListResponse> GetSecurityTipsForUserLiveLocation(string userId, string coordinates, int pageNumber, int pageSize)
        {
            GetSecurityTipsListResponse response = new GetSecurityTipsListResponse();

            try
            {
                // Get location info from coordinates
                var customerLiveLocation = await _geocodingService.GetCustomerLiveAddresses(coordinates);
                if (customerLiveLocation == null || customerLiveLocation.status == APIResponseStatus.fail.ToString())
                {
                    response.Success = false;
                    response.Message = customerLiveLocation?.Message ?? "Failed to determine location from coordinates";
                    return response;
                }

                // First try to get town-level tips
                string townName = customerLiveLocation.Data?.TownOrDistrict;
                if (!string.IsNullOrWhiteSpace(townName))
                {
                    var town = await _context.Towns.FirstOrDefaultAsync(x => x.Name == townName);
                    if (town != null)
                    {
                        var townTips = await _securityTipRepositoryAsync.GetSecurityTipDataForTown(town.Id.ToString(), pageNumber, pageSize);
                        if (townTips?.SecurityTipsList != null && townTips.SecurityTipsList.Any())
                        {
                            return new GetSecurityTipsListResponse
                            {
                                Success = true,
                                SecurityTipsList = townTips.SecurityTipsList,
                                Message = $"Found town-level tips for {townName}"
                            };
                        }
                    }
                }

                // If no town or no town tips, try LGA-level
                string lgaName = customerLiveLocation.Data?.CountryOrDistrictOrLGA;
                if (!string.IsNullOrWhiteSpace(lgaName))
                {
                    var lga = await _context.LGAs.FirstOrDefaultAsync(x => x.Name == lgaName);
                    if (lga != null)
                    {
                        var lgaTips = await _securityTipRepositoryAsync.GetSecurityTipDataForLGA(lga.Id.ToString(), pageNumber, pageSize);
                        if (lgaTips?.SecurityTipsList != null && lgaTips.SecurityTipsList.Any())
                        {
                            return new GetSecurityTipsListResponse
                            {
                                Success = true,
                                SecurityTipsList = lgaTips.SecurityTipsList,
                                Message = $"Found LGA-level tips for {lgaName}"
                            };
                        }
                    }
                }

                // If no LGA or no LGA tips, try state-level
                string stateName = customerLiveLocation.Data?.StateOrProvinceOrRegion;
                if (!string.IsNullOrWhiteSpace(stateName))
                {
                    var state = await _context.States.FirstOrDefaultAsync(x => x.Name == stateName);
                    if (state != null)
                    {
                        var stateTips = await _securityTipRepositoryAsync.GetSecurityTipDataForState(state.Id.ToString(), pageNumber, pageSize);
                        if (stateTips?.SecurityTipsList != null && stateTips.SecurityTipsList.Any())
                        {
                            return new GetSecurityTipsListResponse
                            {
                                Success = true,
                                SecurityTipsList = stateTips.SecurityTipsList,
                                Message = $"Found state-level tips for {stateName}"
                            };
                        }
                    }
                }

                // No tips found at any level
                return new GetSecurityTipsListResponse
                {
                    Success = true,
                    SecurityTipsList = new List<GetSecurityTipResponse>(),
                    Message = "No security tips found for your current location"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving security tips for live location {Coordinates}", coordinates);
                response.Success = false;
                response.Message = "An error occurred while retrieving security tips. Please try again later.";
                return response;
            }
        }
        public async Task<BroadcasterandTipLocations> GetBroadcasterandTipLocations(string BroadcastLevelId, string BroadcastLocationId, string BroadcasterTownId)
        {
            BroadcasterandTipLocations broadcasterandTipLocations = new BroadcasterandTipLocations();
            try
            {
                var broadcastLevel = await _context.BroadcastLevels.Where(x => x.Id == Guid.Parse(BroadcastLevelId)).FirstOrDefaultAsync();
                //Get Tip Broadcast Location
                if (broadcastLevel.Name == BroadcastLevelEnum.State.ToString())
                {
                    var state = await _stateRepositoryAsync.GetByIdAsync(BroadcastLocationId.ToString());
                    broadcasterandTipLocations.BroadcastLocationLevel = BroadcastLevelEnum.State.ToString();
                    broadcasterandTipLocations.BroadcastLocation = state.Name;
                }
                else if (broadcastLevel.Name == BroadcastLevelEnum.LGA.ToString())
                {
                    var lga = await _lGARepositoryAsync.GetByIdAsync(BroadcastLocationId.ToString());
                    broadcasterandTipLocations.BroadcastLocationLevel = BroadcastLevelEnum.LGA.ToString();
                    broadcasterandTipLocations.BroadcastLocation = lga.Name;
                }
                else if (broadcastLevel.Name == BroadcastLevelEnum.Town.ToString())
                {
                    var town = await _townRepositoryAsync.GetByIdAsync(BroadcastLocationId.ToString());
                    broadcasterandTipLocations.BroadcastLocationLevel = BroadcastLevelEnum.Town.ToString();
                    broadcasterandTipLocations.BroadcastLocation = town.Name;
                }

                //Get Broadcaster Location Details 
                var townWithState = await _townRepositoryAsync.GetTownStateAndLGAAsync(BroadcasterTownId);

                broadcasterandTipLocations.BroadcasterFullLocation = $"{townWithState.TownName}, {townWithState.LGAName}, {townWithState.StateName} ";

                return broadcasterandTipLocations;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving user and tip locations: {ex.StackTrace}:");
                broadcasterandTipLocations = null;
                return broadcasterandTipLocations;
            }
        }

        public async Task<GetSecurityTipsListResponse> GetSecurityTipsForState(string StateId, int pageNumber, int pageSize)
        {
            GetSecurityTipsListResponse getSecurityTipsListResponse = new GetSecurityTipsListResponse();

            try
            {
                var stateSecuritytips = await _securityTipRepositoryAsync.GetSecurityTipDataForLGA(StateId, pageNumber, pageSize);

                // Get UserandTipLocations
                //var broadcasterAndTipLocations = await GetBroadcasterandTipLocations(stateSecuritytips.SecurityTipsList.First().BroadcastLevelId, stateSecuritytips.SecurityTipsList.First().BroadcastLocationId, stateSecuritytips.SecurityTipsList.First().BroadcasterTownId);

                //if (broadcasterAndTipLocations == null)
                //{
                //    getSecurityTipsListResponse = null;
                //    return getSecurityTipsListResponse;
                //}
                getSecurityTipsListResponse.SecurityTipsList = stateSecuritytips.SecurityTipsList;

                //foreach (var securityTip in getSecurityTipsListResponse.SecurityTipsList)
                //{
                //    securityTip.BroadcastLevel = broadcasterAndTipLocations.BroadcastLocationLevel;
                //    securityTip.BroadcastLocation = broadcasterAndTipLocations.BroadcastLocation;
                //    securityTip.BroadcasterFullLocation = broadcasterAndTipLocations.BroadcasterFullLocation;
                //}

                return getSecurityTipsListResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving security tip: {ex.StackTrace}");

                getSecurityTipsListResponse.Success = false;
                getSecurityTipsListResponse.Message = $"{ex}";
                return getSecurityTipsListResponse;
            }
        }

        public async Task<GetSecurityTipsListResponse> GetSecurityTipsForLGA(string LGAId, int pageNumber, int pageSize)
        {
            GetSecurityTipsListResponse getSecurityTipsListResponse = new GetSecurityTipsListResponse();

            try
            {
                var lgaSecuritytips = await _securityTipRepositoryAsync.GetSecurityTipDataForLGA(LGAId, pageNumber, pageSize);

                getSecurityTipsListResponse.SecurityTipsList = lgaSecuritytips.SecurityTipsList;

                return getSecurityTipsListResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving security tip: {ex}");

                getSecurityTipsListResponse.Success = false;
                getSecurityTipsListResponse.Message = $"{ex}";
                return getSecurityTipsListResponse;

            }
        }

        public async Task<GetSecurityTipsListResponse> GetSecurityTipsForTown(string TownId, int pageNumber, int pageSize)
        {
            GetSecurityTipsListResponse getSecurityTipsListResponse = new GetSecurityTipsListResponse();

            try
            {
                var UserSecuritytips = await _securityTipRepositoryAsync.GetSecurityTipDataForTown(TownId, pageNumber, pageSize);
                               
                getSecurityTipsListResponse.SecurityTipsList = UserSecuritytips.SecurityTipsList;

                return getSecurityTipsListResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving security tip: {ex.StackTrace}");
                
                getSecurityTipsListResponse.Success = false;
                getSecurityTipsListResponse.Message = $"{ex}";
                return getSecurityTipsListResponse;
            }
        }

        public async Task<GetSecurityTipsListResponse> GetSecurityTipsForUserRegisteredLocation(string userId, int pageNumber, int pageSize)
        {
            GetSecurityTipsListResponse response = new GetSecurityTipsListResponse();

            try
            {
                // Get the user's profile to find their registered town/city
                var customerProfile = await _customerService.GetCustomerProfileAsync(userId);
                if (customerProfile == null || string.IsNullOrEmpty(customerProfile.userLocation?.city?.cityId))
                {
                    response.Success = false;
                    response.Message = "User profile or location information not found";
                    return response;
                }

                string townId = customerProfile.userLocation.city.cityId;

                // Get the town, LGA and state hierarchy
                var locationHierarchy = await _townRepositoryAsync.GetTownStateAndLGAAsync(townId);
                if (locationHierarchy == null)
                {
                    response.Success = false;
                    response.Message = "Location information not found";
                    return response;
                }

                // First get town-level tips
                var townTips = await _securityTipRepositoryAsync.GetSecurityTipDataForTown(townId, pageNumber, pageSize);

                // If no town-level tips, try LGA-level
                if (townTips?.SecurityTipsList == null || !townTips.SecurityTipsList.Any())
                {
                    var lgaTips = await _securityTipRepositoryAsync.GetSecurityTipDataForLGA(locationHierarchy.LGAId, pageNumber, pageSize);

                    // If no LGA-level tips, try state-level
                    if (lgaTips?.SecurityTipsList == null || !lgaTips.SecurityTipsList.Any())
                    {
                        var stateTips = await _securityTipRepositoryAsync.GetSecurityTipDataForState(locationHierarchy.StateId, pageNumber, pageSize);

                        if (stateTips?.SecurityTipsList == null || !stateTips.SecurityTipsList.Any())
                        {
                            response.Success = true;
                            response.Message = "No security tips found for your registered location";
                            response.SecurityTipsList = new List<GetSecurityTipResponse>();
                            return response;
                        }

                        response = new GetSecurityTipsListResponse
                        {
                            Success = true,
                            SecurityTipsList = stateTips.SecurityTipsList,
                            Message = $"Found state-level tips for {locationHierarchy.StateName}"
                        };
                    }
                    else
                    {
                        response = new GetSecurityTipsListResponse
                        {
                            Success = true,
                            SecurityTipsList = lgaTips.SecurityTipsList,
                            Message = $"Found LGA-level tips for {locationHierarchy.LGAName}"
                        };
                    }
                }
                else
                {
                    response = new GetSecurityTipsListResponse
                    {
                        Success = true,
                        SecurityTipsList = townTips.SecurityTipsList,
                        Message = $"Found town-level tips for {locationHierarchy.TownName}"
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving security tips for user's registered location {UserId}", userId);
                response.Success = false;
                response.Message = "An error occurred while retrieving security tips. Please try again later.";
                return response;
            }
        }
    }
}
