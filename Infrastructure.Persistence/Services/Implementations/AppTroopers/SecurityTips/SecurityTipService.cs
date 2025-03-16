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
            CreateSecurityTipResponse createSecurityTipResponse   = new CreateSecurityTipResponse();
            bool isCreated = false;
            SecurityTip saveTipResult = new SecurityTip();
            try
            {// Get the user to check if active
                var user = await _userManager.FindByIdAsync(securityTipRequest.BroadcasterId);
                if (user == null || !user.isActive)
                {
                    createSecurityTipResponse.IsCreated = false;
                    createSecurityTipResponse.Message = "User is not authorized to create security tips";
                    return createSecurityTipResponse;
                }

                // Get source
                var source = await _context.Sources
                    .FirstOrDefaultAsync(x => x.SourceName == SourcesEnum.VGNGAUser.ToString());

                if (source == null)
                {
                    createSecurityTipResponse.IsCreated = false;
                    createSecurityTipResponse.Message = "Invalid source configuration";
                    return createSecurityTipResponse;
                }

                // Create security tip
                var securityTip = new SecurityTip
                {
                    Subject = securityTipRequest.Subject,
                    Body = securityTipRequest.Description,
                    AlertLevelId = Guid.Parse(securityTipRequest.AlertLevelId),
                    Coordinates = securityTipRequest.Coordinates,
                    BroadcasterId = securityTipRequest.BroadcasterId,
                    SecurityTipCategoryId = Guid.Parse(securityTipRequest.CategoryId),
                    Casualties = securityTipRequest.Casualties,
                    SourceId = source.Id,
                    CreatedBy = securityTipRequest.BroadcasterId,
                    Created = DateTime.UtcNow.AddHours(1),
                    IsBroadcasted = true, // Auto-approve for active users
                    Status = SecurityTipStatusEnum.Approved
                };

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
                    securityTipRequest.BroadcasterId);

                createSecurityTipResponse.IsCreated = false;
                createSecurityTipResponse.Message = "An error occurred while creating the security tip. Please try again later.";
                return createSecurityTipResponse;
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

        public async Task<GetSecurityTipResponse> GetSecurityTip(string SecurityTipId)
        {
            try
            {
                var securityTip = await (from tip in _context.SecurityTips
                                         join broadcaster in _context.Users on tip.BroadcasterId equals broadcaster.Id
                                         join category in _context.SecurityTipCategories on tip.SecurityTipCategoryId equals category.Id
                                         join alertLevel in _context.AlertLevels on tip.AlertLevelId equals alertLevel.Id
                                         join categoryType in _context.SecurityTipCategoryTypes on category.CategoryTypeId equals categoryType.Id
                                         where tip.Id == Guid.Parse(SecurityTipId)
                                         select new GetSecurityTipResponse
                                         {
                                             Id = tip.Id.ToString(),
                                             Subject = tip.Subject,
                                             Description = tip.Body,
                                             Coordinates = tip.Coordinates,
                                             SecurityTipStatus = tip.Status.ToString(),
                                             BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                             AlertLevel = alertLevel.Name,
                                             IsBroadcasted = tip.IsBroadcasted,
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
                                             }
                                         }).FirstOrDefaultAsync();

                if (securityTip == null)
                {
                    _logger.LogWarning($"Security tip with ID {SecurityTipId} was not found");
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
                return securityTip;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving security tip with ID {SecurityTipId}", SecurityTipId);
                return null;
            }
        }

        public async Task<GetSecurityTipsListResponse> GetSecurityTipsPostedByUser(string Userid, int pageNumber, int pageSize)
        {
            GetSecurityTipsListResponse getSecurityTipsListResponse = new GetSecurityTipsListResponse();

            try
            {
                var userSecurityTips = await (from securityTip in _context.SecurityTips
                                              join broadcaster in _context.Users on securityTip.BroadcasterId equals broadcaster.Id
                                              join category in _context.SecurityTipCategories on securityTip.SecurityTipCategoryId equals category.Id
                                              join alertLevel in _context.AlertLevels on securityTip.AlertLevelId equals alertLevel.Id
                                              where broadcaster.Id == Userid
                                              select new GetSecurityTipResponse
                                              {
                                                  Id = securityTip.Id.ToString(),
                                                  Subject = securityTip.Subject,
                                                  Description = securityTip.Body,
                                                  Coordinates = securityTip.Coordinates,
                                                  SecurityTipStatus = securityTip.Status.ToString(),
                                                  BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                                  AlertLevel = alertLevel.Name,
                                                  IsBroadcasted = securityTip.IsBroadcasted,
                                                  AlertCategoryType = new AlertCategoryType
                                                  {
                                                      Id = category.CategoryType.Id.ToString(),
                                                      Name = category.CategoryType.Name,
                                                      Description = category.CategoryType.Description
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
                                                  }
                                              }).Skip((pageNumber - 1) * pageSize)
                                              .Take(pageSize)
                                              .AsNoTracking()
                                              .ToListAsync();

                if (userSecurityTips == null || !userSecurityTips.Any())
                {
                    _logger.LogWarning($"No security tips found for user with ID {Userid}");
                    return null;
                }

                getSecurityTipsListResponse.SecurityTipsList = userSecurityTips;

                foreach (var securityTip in getSecurityTipsListResponse.SecurityTipsList)
                {
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
                }

                return getSecurityTipsListResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving security tips for user {Userid}: {ex.StackTrace}");
                getSecurityTipsListResponse = null;
                return getSecurityTipsListResponse;
            }
        }


        public async Task<GetLiveLocationSecurityTipResponse> GetSecurityTipsForUserLiveLocation(string Userid, string BroadcastLevel, string coordinates, int pageNumber, int pageSize) // state, lga and town
        {
            GetLiveLocationSecurityTipResponse getSecurityTipsListResponse = new GetLiveLocationSecurityTipResponse();

            try
            { 
                var customerLiveLocation = await _geocodingService.GetCustomerLiveAddresses(coordinates);

                if (customerLiveLocation.status == APIResponseStatus.fail.ToString())
                {
                    getSecurityTipsListResponse.Success = false;
                    getSecurityTipsListResponse.Message = customerLiveLocation.Message;
                    return getSecurityTipsListResponse;
                }

                var broadcastLevelExists = Enum.GetNames(typeof(BroadcastLevelEnum)).Any(x => x.ToLower() ==BroadcastLevel);

                if (!broadcastLevelExists)
                {
                    _logger.LogWarning($"Request from: {Userid} Selected Broadast Level does not exist:");
                    getSecurityTipsListResponse.Success = false;
                    getSecurityTipsListResponse.Message = $"Request from: {Userid} Selected Broadast Level does not exist:";
                    return getSecurityTipsListResponse;
                }

                var broadcastlevelEnum = Enum.TryParse(BroadcastLevel, out BroadcastLevelEnum myStatus);

                if (myStatus == BroadcastLevelEnum.Town)
                {
                    // get live location town
                    string townName = customerLiveLocation.Data?.TownOrDistrict;

                    var town = await _context.Towns.Where(x => x.Name == townName).FirstOrDefaultAsync();
                    if (town == null)
                    {
                        _logger.LogWarning($"Request from: {Userid}: Operation: GetSecurityTipsForUserLiveLocation: Warning: Town does not exist.");

                        getSecurityTipsListResponse.Success = false;
                        getSecurityTipsListResponse.Message = $"Town does not exist.";
                        return getSecurityTipsListResponse;
                    }
                    getSecurityTipsListResponse = await _securityTipRepositoryAsync.GetSecurityTipDataForTown(town.Id.ToString(), pageNumber, pageSize);
                    return getSecurityTipsListResponse;
                }

                else if (myStatus == BroadcastLevelEnum.LGA)
                {
                    string lgaName = customerLiveLocation.Data?.CountryOrDistrictOrLGA;

                    var lga = await _context.LGAs.Where(x => x.Name == lgaName).FirstOrDefaultAsync();
                    if (lga == null)
                    {
                        _logger.LogWarning($"Request from: {Userid}: Operation: GetSecurityTipsForUserLiveLocation: Warning: Town does not exist.");

                        getSecurityTipsListResponse.Success = false;
                        getSecurityTipsListResponse.Message = $"LGA does not exist.";
                        return getSecurityTipsListResponse;
                    }
                    getSecurityTipsListResponse = await _securityTipRepositoryAsync.GetSecurityTipDataForLGA(lga.Id.ToString(), pageNumber, pageSize);

                    if (getSecurityTipsListResponse == null)
                    {
                        getSecurityTipsListResponse.Success = false;
                        getSecurityTipsListResponse.Message = $"At the moment,no security tips have been recorded for {lgaName} LGA.";
                        return getSecurityTipsListResponse;
                    }
                    return getSecurityTipsListResponse;
                }
                else if (myStatus == BroadcastLevelEnum.State)
                {

                    string stateName = customerLiveLocation.Data?.StateOrProvinceOrRegion;

                    var state = await _context.States.Where(x => x.Name == stateName).FirstOrDefaultAsync();
                    if (state == null)
                    {
                        _logger.LogWarning($"Request from: {Userid}: Operation: GetSecurityTipsForUserLiveLocation: Warning: Town does not exist.");

                        getSecurityTipsListResponse.Success = false;
                        getSecurityTipsListResponse.Message = $"State does not exist.";
                        return getSecurityTipsListResponse;
                    }
                    getSecurityTipsListResponse = await _securityTipRepositoryAsync.GetSecurityTipDataForState(state.Id.ToString(), pageNumber, pageSize);

                    if (!getSecurityTipsListResponse.SecurityTipsList.Any())
                    {
                        getSecurityTipsListResponse.Success = false;
                        getSecurityTipsListResponse.Message = $"At the moment,no security tips have been recorded for {stateName} State.";
                        return getSecurityTipsListResponse;
                    }
                    return getSecurityTipsListResponse;
                }
                
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

        public async Task<GetSecurityTipsForUserTownLGAandStateResponse> GetSecurityTipsForUserLocations(string Userid, int pageNumber, int pageSize) // state, lga and town
        {
            GetSecurityTipsForUserTownLGAandStateResponse getSecurityTipsForUserTownLGAandStateResponse = new GetSecurityTipsForUserTownLGAandStateResponse();

            try
            {
                var customerProfile = await _customerService.GetCustomerProfileAsync(Userid);

                var townTips = await _securityTipRepositoryAsync.GetSecurityTipDataForTown(customerProfile.userLocation.city.cityId, pageNumber, pageSize);
                getSecurityTipsForUserTownLGAandStateResponse.SecurityTipsListforUserTown = townTips.SecurityTipsList;

                var lgaTips = await _securityTipRepositoryAsync.GetSecurityTipDataForLGA(customerProfile.userLocation.city.cityId, pageNumber, pageSize);
                getSecurityTipsForUserTownLGAandStateResponse.SecurityTipsListforUserLGA = lgaTips.SecurityTipsList;

                var stateTips = await _securityTipRepositoryAsync.GetSecurityTipDataForState(customerProfile.userLocation.city.cityId, pageNumber, pageSize);
                getSecurityTipsForUserTownLGAandStateResponse.SecurityTipsListforUserState = stateTips.SecurityTipsList;

                return getSecurityTipsForUserTownLGAandStateResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving security tip: {ex.StackTrace}");

                getSecurityTipsForUserTownLGAandStateResponse.Success = false;
                getSecurityTipsForUserTownLGAandStateResponse.Message = $"{ex}";
                return getSecurityTipsForUserTownLGAandStateResponse;
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
    }
}
