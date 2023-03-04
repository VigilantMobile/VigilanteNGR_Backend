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
        private readonly IAlertLevelRespositoryAsync _alertLevelRespositoryAsync;
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
            IAlertLevelRespositoryAsync alertLevelRespositoryAsync,
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
            {
                var source = await _context.Sources.Where(x => x.SourceName == SourcesEnum.VGNGAUser.ToString()).FirstOrDefaultAsync();
                //Get Post Eligibilitys
                var postEligibility = await _securityTipEligibilityService.GetSecurityTipPostEligibility(securityTipRequest.BroadcasterId,
                    securityTipRequest.BroadcastLevelId, securityTipRequest.AlertLevelId, securityTipRequest.coordinates, securityTipRequest.LocationId);

                if (postEligibility.CanPostTip)
                {
                    string SavedTipId = string.Empty;

                    SecurityTip securityTip = new SecurityTip
                    {
                        Subject = securityTipRequest.Subject, 
                        Body = securityTipRequest.Body, 
                        AlertLevelId = Guid.Parse(securityTipRequest.AlertLevelId),
                        BroadcastLevelId = Guid.Parse(securityTipRequest.BroadcastLevelId),
                        LocationId = securityTipRequest.LocationId,
                        BroadcasterId = securityTipRequest.BroadcasterId,
                        SecurityTipCategoryId = Guid.Parse(securityTipRequest.CategoryId),
                        Casualties = securityTipRequest.Casualties,
                        SourceId = source.Id,
                        CreatedBy = securityTipRequest.BroadcasterId,
                        Created = DateTime.UtcNow.AddHours(1)
                    };

                    if (postEligibility.CanBroadcastImmediately)
                    {
                        securityTip.IsBroadcasted = true;
                        securityTip.TipStatusString = SecurityTipStatusEnum.BroadcastedPendingVerification.ToString();

                        //Do BroadCast();
                        //Save Tip
                        saveTipResult = await _securityTipRepositoryAsync.AddAsync(securityTip);
                        SavedTipId = saveTipResult.Id.ToString();
                    }
                    else 
                    {
                        //Can't broadcast Tip immediately
                       
                        if (!postEligibility.EscalationRequested) //tip is district level escalation unnecessary:
                        {
                            securityTip.IsBroadcasted = false;
                            securityTip.TipStatusString = SecurityTipStatusEnum.PendingApproval.ToString();
                            securityTip.TipStatusString = SecurityTipStatusEnum.PendingApproval.ToString();
                            saveTipResult = await _securityTipRepositoryAsync.AddAsync(securityTip);
                            SavedTipId = saveTipResult.Id.ToString();
                        }
                        else
                        {
                            securityTip.EscalationRequested = true;
                            saveTipResult = await _securityTipRepositoryAsync.AddAsync(securityTip);
                            SavedTipId = saveTipResult.Id.ToString();

                            EscalatedTip escalatedTip = new EscalatedTip
                            {
                                EscalationBroadcastLevelId = Guid.Parse(securityTipRequest.BroadcastLevelId),
                                EscalationLocationId = securityTipRequest.LocationId,
                                CreatedBy = securityTipRequest.BroadcasterId,
                                Created = DateTime.UtcNow.AddHours(1),
                                SecurityTipId = Guid.Parse(SavedTipId)
                            };

                            await _escalatedTipsRepositoryAsync.AddAsync(escalatedTip);
                        }
                    }

                    createSecurityTipResponse.SecurityTipStatus = saveTipResult.TipStatusString;
                    createSecurityTipResponse.IsDispatched = saveTipResult.IsBroadcasted;
                    createSecurityTipResponse.IsCreated = true;
                    createSecurityTipResponse.Message = $"Security Tip Created";
                    return createSecurityTipResponse;
                }
                else
                {
                    //Sorry Cant post tip with {reason}
                    createSecurityTipResponse.SecurityTipStatus = saveTipResult.TipStatusString;
                    createSecurityTipResponse.IsDispatched = saveTipResult.IsBroadcasted;
                    createSecurityTipResponse.IsCreated = true;
                    createSecurityTipResponse.Message = postEligibility.FailureReason;
                    return createSecurityTipResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while saving a security tip: ");
                createSecurityTipResponse.IsCreated = false;
                createSecurityTipResponse.Message = "It's not you, it's us. An error occurred while creating the security tip: Please try again later.";
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
            GetSecurityTipResponse securityTipResponse = new GetSecurityTipResponse();

            try
            {
                var Securitytip = await (from securityTip in _context.SecurityTips
                                                join broadcaster in _context.Users on securityTip.BroadcasterId equals broadcaster.Id
                                                join category in _context.SecurityTipCategories on securityTip.SecurityTipCategoryId equals category.Id
                                                join alertLevel in _context.AlertLevels on securityTip.AlertLevelId equals alertLevel.Id


                                         select new GetSecurityTipResponse
                                         {
                                              Id =  securityTip.Id.ToString(),
                                              Subject = securityTip.Subject,
                                              Body = securityTip.Body,
                                              BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}" ,
                                              TipStatus = securityTip.TipStatusString,
                                              AlertLevel = alertLevel.Name,
                                              SecurityTipCategory = category.CategoryName,
                                              BroadcastLevelId = securityTip.BroadcastLevelId.ToString(),
                                              BroadcastLocationId = securityTip.LocationId,
                                              BroadcasterTownId = broadcaster.TownId.ToString(),
                                         }).FirstOrDefaultAsync();

                //Get Tip Broadcast Location

                var broadcasterAndTipLocations = await GetBroadcasterandTipLocations(Securitytip.BroadcastLevelId, Securitytip.BroadcastLocationId, Securitytip.BroadcasterTownId);

                if (broadcasterAndTipLocations == null)
                {
                    securityTipResponse = null;
                    return securityTipResponse;
                }

                securityTipResponse.BroadcastLevel = broadcasterAndTipLocations.BroadcastLocationLevel;
                securityTipResponse.BroadcastLocation = broadcasterAndTipLocations.BroadcastLocation;
                securityTipResponse.BroadcasterFullLocation = broadcasterAndTipLocations.BroadcasterFullLocation;

                return securityTipResponse;
            }
            catch (Exception ex)
            {
               _logger.LogError($"An error occurred while retrieving security tip: {ex.StackTrace}");
                securityTipResponse = null;
               return securityTipResponse;
            }
        }

        public async Task<GetSecurityTipsListResponse> GetSecurityTipsPostedByUser(string Userid, int pageNumber, int pageSize)
        {
            GetSecurityTipsListResponse getSecurityTipsListResponse = new GetSecurityTipsListResponse();
           
            try
            {
                var UserSecuritytips = await (from securityTip in _context.SecurityTips
                                         join broadcaster in _context.Users on securityTip.BroadcasterId equals broadcaster.Id
                                         join category in _context.SecurityTipCategories on securityTip.SecurityTipCategoryId equals category.Id
                                         join alertLevel in _context.AlertLevels on securityTip.AlertLevelId equals alertLevel.Id
                                         where broadcaster.Id == Userid

                                         select new GetSecurityTipResponse
                                         {
                                             Id = securityTip.Id.ToString(),
                                             Subject = securityTip.Subject,
                                             Body = securityTip.Body,
                                             BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                             TipStatus = securityTip.TipStatusString,
                                             AlertLevel = alertLevel.Name,
                                             SecurityTipCategory = category.CategoryName,
                                             BroadcastLevelId = securityTip.BroadcastLevelId.ToString(),
                                             BroadcastLocationId = securityTip.LocationId,
                                             BroadcasterTownId = broadcaster.TownId.ToString(),
                                         }).Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .AsNoTracking()
                                         .ToListAsync();

                // Get UserandTipLocations
                var broadcasterAndTipLocations = await GetBroadcasterandTipLocations(UserSecuritytips.First().BroadcastLevelId, UserSecuritytips.First().BroadcastLocationId, UserSecuritytips.First().BroadcasterTownId);

                if (broadcasterAndTipLocations == null)
                {
                    getSecurityTipsListResponse = null;
                    return getSecurityTipsListResponse;
                }   

                getSecurityTipsListResponse.SecurityTipsList = UserSecuritytips;

                foreach (var securityTip in getSecurityTipsListResponse.SecurityTipsList)
                {
                    securityTip.BroadcastLevel = broadcasterAndTipLocations.BroadcastLocationLevel;
                    securityTip.BroadcastLocation = broadcasterAndTipLocations.BroadcastLocation;
                    securityTip.BroadcasterFullLocation = broadcasterAndTipLocations.BroadcasterFullLocation;
                }

                return getSecurityTipsListResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving security tip: {ex.StackTrace}");
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

                var broadcastLevelExists = Enum.GetNames(typeof(BroadcastLevelEnum)).Any(x => x.ToLower() ==BroadcastLevel);

                if (!broadcastLevelExists)
                {
                    _logger.LogWarning($"Request from: {Userid} Selected Broadast Level does not exist:");
                    return null;
                }

                // get live location town
                string townName = customerLiveLocation.Data?.DistrictName;

                var town = await _context.Towns.Where(x => x.Name == townName).FirstOrDefaultAsync();
                if (town == null)
                {
                    _logger.LogWarning($"Request from: {Userid}: Operation: GetSecurityTipsForUserLiveLocation: Warning: Town does not exist.");

                    return null;
                }

                var broadcastlevelEnum = Enum.TryParse(BroadcastLevel, out BroadcastLevelEnum myStatus);

                if (myStatus == BroadcastLevelEnum.Town)
                {
                    // Redo this by calling existing methods.
                    getSecurityTipsListResponse = await _securityTipRepositoryAsync.GetSecurityTipDataForTown(town.Id.ToString(), pageNumber, pageSize);
                    return getSecurityTipsListResponse;
                }
                else
                {
                    var tipTownlGAState = await _townRepositoryAsync.GetTownStateAndLGAAsync(town.Id.ToString());

                    if (myStatus == BroadcastLevelEnum.LGA)
                    {
                        getSecurityTipsListResponse = await _securityTipRepositoryAsync.GetSecurityTipDataForLGA(tipTownlGAState.LGAId, pageNumber, pageSize);
                        return getSecurityTipsListResponse;
                    }
                    else if (myStatus == BroadcastLevelEnum.State)
                    {
                        getSecurityTipsListResponse = await _securityTipRepositoryAsync.GetSecurityTipDataForState(tipTownlGAState.StateId.ToString(), pageNumber, pageSize);
                        return getSecurityTipsListResponse;
                    }
                } 

                return getSecurityTipsListResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving security tip: {ex.StackTrace}");
                getSecurityTipsListResponse = null;
                return getSecurityTipsListResponse;
            }
        }

        public async Task<GetSecurityTipsForUserTownLGAandStateResponse> GetSecurityTipsForUserLocations(string Userid, int pageNumber, int pageSize) // state, lga and town
        {
            GetSecurityTipsForUserTownLGAandStateResponse getSecurityTipsForUserTownLGAandStateResponse = new GetSecurityTipsForUserTownLGAandStateResponse();

            try
            {
                var customerLocations = await _customerService.GetCustomerProfileAsync(Userid);

                var townTips = await _securityTipRepositoryAsync.GetSecurityTipDataForTown(customerLocations.CustomerLocation.CustomerDistrict.DistrictId, pageNumber, pageSize);
                getSecurityTipsForUserTownLGAandStateResponse.SecurityTipsListforUserTown = townTips.SecurityTipsList;

                var lgaTips = await _securityTipRepositoryAsync.GetSecurityTipDataForLGA(customerLocations.CustomerLocation.CustomerDistrict.DistrictId, pageNumber, pageSize);
                getSecurityTipsForUserTownLGAandStateResponse.SecurityTipsListforUserLGA = lgaTips.SecurityTipsList;

                var stateTips = await _securityTipRepositoryAsync.GetSecurityTipDataForState(customerLocations.CustomerLocation.CustomerDistrict.DistrictId, pageNumber, pageSize);
                getSecurityTipsForUserTownLGAandStateResponse.SecurityTipsListforUserState = stateTips.SecurityTipsList;

                return getSecurityTipsForUserTownLGAandStateResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving security tip: {ex.StackTrace}");
                getSecurityTipsForUserTownLGAandStateResponse = null;
                return getSecurityTipsForUserTownLGAandStateResponse;
            }
        }

        public async Task<GetSecurityTipsListResponse> GetSecurityTipsForState(string StateId, int pageNumber, int pageSize)
        {
            GetSecurityTipsListResponse getSecurityTipsListResponse = new GetSecurityTipsListResponse();

            try
            {
                var UserSecuritytips = await (from securityTip in _context.SecurityTips
                                              join broadcaster in _context.Users on securityTip.BroadcasterId equals broadcaster.Id
                                              join category in _context.SecurityTipCategories on securityTip.SecurityTipCategoryId equals category.Id
                                              join alertLevel in _context.AlertLevels on securityTip.AlertLevelId equals alertLevel.Id
                                              join broadcastLevel in _context.BroadcastLevels on securityTip.BroadcastLevelId equals broadcastLevel.Id
                                              join state in _context.States on securityTip.LocationId equals state.Id.ToString()
                                              where broadcastLevel.Name == BroadcastLevelEnum.State.ToString() && state.Id.ToString() == StateId
                                              && securityTip.IsBroadcasted == true

                                              select new GetSecurityTipResponse
                                              {
                                                  Id = securityTip.Id.ToString(),
                                                  Subject = securityTip.Subject,
                                                  Body = securityTip.Body,
                                                  BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                                  TipStatus = securityTip.TipStatusString,
                                                  AlertLevel = alertLevel.Name,
                                                  SecurityTipCategory = category.CategoryName,
                                                  BroadcastLevelId = securityTip.BroadcastLevelId.ToString(),
                                                  BroadcastLocationId = securityTip.LocationId,
                                                  BroadcasterTownId = broadcaster.TownId.ToString(),
                                              }).Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .AsNoTracking()
                                         .ToListAsync();

                // Get UserandTipLocations
                var broadcasterAndTipLocations = await GetBroadcasterandTipLocations(UserSecuritytips.First().BroadcastLevelId, UserSecuritytips.First().BroadcastLocationId, UserSecuritytips.First().BroadcasterTownId);

                if (broadcasterAndTipLocations == null)
                {
                    getSecurityTipsListResponse = null;
                    return getSecurityTipsListResponse;
                }

                getSecurityTipsListResponse.SecurityTipsList = UserSecuritytips;

                foreach (var securityTip in getSecurityTipsListResponse.SecurityTipsList)
                {
                    securityTip.BroadcastLevel = broadcasterAndTipLocations.BroadcastLocationLevel;
                    securityTip.BroadcastLocation = broadcasterAndTipLocations.BroadcastLocation;
                    securityTip.BroadcasterFullLocation = broadcasterAndTipLocations.BroadcasterFullLocation;
                }

                return getSecurityTipsListResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving security tip: {ex.StackTrace}");
                getSecurityTipsListResponse = null;
                return getSecurityTipsListResponse;
            }
        }

        public async Task<GetSecurityTipsListResponse> GetSecurityTipsForLGA(string LGAId, int pageNumber, int pageSize)
        {
            GetSecurityTipsListResponse getSecurityTipsListResponse = new GetSecurityTipsListResponse();

            try
            {
                var UserSecuritytips = await (from securityTip in _context.SecurityTips
                                              join broadcaster in _context.Users on securityTip.BroadcasterId equals broadcaster.Id.ToString()
                                              join category in _context.SecurityTipCategories on securityTip.SecurityTipCategoryId equals category.Id
                                              join alertLevel in _context.AlertLevels on securityTip.AlertLevelId equals alertLevel.Id
                                              join broadcastLevel in _context.BroadcastLevels on securityTip.BroadcastLevelId equals broadcastLevel.Id
                                              join lga in _context.LGAs on securityTip.LocationId equals lga.Id.ToString()
                                              where broadcastLevel.Name == BroadcastLevelEnum.LGA.ToString() && lga.Id.ToString() == LGAId
                                              && securityTip.IsBroadcasted == true

                                              select new GetSecurityTipResponse
                                              {
                                                  Id = securityTip.Id.ToString(),
                                                  Subject = securityTip.Subject,
                                                  Body = securityTip.Body,
                                                  BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                                  TipStatus = securityTip.TipStatusString,
                                                  AlertLevel = alertLevel.Name,
                                                  SecurityTipCategory = category.CategoryName,
                                                  BroadcastLevelId = securityTip.BroadcastLevelId.ToString(),
                                                  BroadcastLocationId = securityTip.LocationId,
                                                  BroadcasterTownId = broadcaster.TownId.ToString(),
                                                   
                                              }).Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .AsNoTracking()
                                         .ToListAsync();

                // Get UserandTipLocations
                var broadcasterAndTipLocations = await GetBroadcasterandTipLocations(UserSecuritytips.First().BroadcastLevelId, UserSecuritytips.First().BroadcastLocationId, UserSecuritytips.First().BroadcasterTownId);

                if (broadcasterAndTipLocations == null)
                {
                    getSecurityTipsListResponse = null;
                    return getSecurityTipsListResponse;
                }

                getSecurityTipsListResponse.SecurityTipsList = UserSecuritytips;

                foreach (var securityTip in getSecurityTipsListResponse.SecurityTipsList)
                {
                    securityTip.BroadcastLevel = broadcasterAndTipLocations.BroadcastLocationLevel;
                    securityTip.BroadcastLocation = broadcasterAndTipLocations.BroadcastLocation;
                    securityTip.BroadcasterFullLocation = broadcasterAndTipLocations.BroadcasterFullLocation;
                }

                return getSecurityTipsListResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving security tip: {ex.StackTrace}");
                getSecurityTipsListResponse = null;
                return getSecurityTipsListResponse;
            }
        }

        public async Task<GetSecurityTipsListResponse> GetSecurityTipsForDistrict(string DistrictId, int pageNumber, int pageSize)
        {
            GetSecurityTipsListResponse getSecurityTipsListResponse = new GetSecurityTipsListResponse();

            try
            {
                var UserSecuritytips = await _securityTipRepositoryAsync.GetSecurityTipDataForTown(DistrictId, pageNumber, pageSize);

                //var dsd = await (from securityTip in _context.SecurityTips
                //       join broadcaster in _context.Users on securityTip.BroadcasterId equals broadcaster.Id.ToString()
                //       join category in _context.SecurityTipCategories on securityTip.SecurityTipCategoryId equals category.Id
                //       join alertLevel in _context.AlertLevels on securityTip.AlertLevelId equals alertLevel.Id
                //       join broadcastLevel in _context.BroadcastLevels on securityTip.BroadcastLevelId equals broadcastLevel.Id
                //       join district in _context.Towns on securityTip.LocationId equals district.Id.ToString()
                //       where broadcastLevel.Name == BroadcastLevelEnum.Town.ToString() && district.Id == Guid.Parse(DistrictId.ToString())
                //       && securityTip.IsBroadcasted == true

                //       select new GetSecurityTipResponse
                //       {
                //           Id = securityTip.Id.ToString(),
                //           Subject = securityTip.Subject,
                //           Body = securityTip.Body,
                //           BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                //           TipStatus = securityTip.TipStatusString,
                //           AlertLevel = alertLevel.Name,
                //           SecurityTipCategory = category.CategoryName,
                //           BroadcastLevelId = securityTip.BroadcastLevelId.ToString(),
                //           BroadcastLocationId = securityTip.LocationId,
                //           BroadcasterTownId = broadcaster.TownId.ToString(),
                //       }).Skip((pageNumber - 1) * pageSize)
                //                         .Take(pageSize)
                //                         .AsNoTracking()
                //                         .ToListAsync();

                // Get UserandTipLocations
                var broadcasterAndTipLocations = await GetBroadcasterandTipLocations(UserSecuritytips.SecurityTipsList.First().BroadcastLevelId, UserSecuritytips.SecurityTipsList.First().BroadcastLocationId, UserSecuritytips.SecurityTipsList.First().BroadcasterTownId);

                if (broadcasterAndTipLocations == null)
                {
                    getSecurityTipsListResponse = null;
                    return getSecurityTipsListResponse;
                }

                getSecurityTipsListResponse.SecurityTipsList = UserSecuritytips.SecurityTipsList;

                foreach (var securityTip in getSecurityTipsListResponse.SecurityTipsList)
                {
                    securityTip.BroadcastLevel = broadcasterAndTipLocations.BroadcastLocationLevel;
                    securityTip.BroadcastLocation = broadcasterAndTipLocations.BroadcastLocation;
                    securityTip.BroadcasterFullLocation = broadcasterAndTipLocations.BroadcasterFullLocation;
                }

                return getSecurityTipsListResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving security tip: {ex.StackTrace}");
                getSecurityTipsListResponse = null;
                return getSecurityTipsListResponse;
            }
        }

        //public async Task<GetSecurityTipsListResponse> GetSecurityTipsForUserLiveLocation(string UserId, string coordinates, int pageNumber, int PageSize)
        //{
        //    GetSecurityTipsListResponse getSecurityTipsListResponse = new GetSecurityTipsListResponse();

        //    try
        //    {
        //        // Get User Location Level



        //        var UserSecuritytips = await (from securityTip in _context.SecurityTips
        //                                      join broadcaster in _context.Users on securityTip.BroadcasterId equals broadcaster.Id
        //                                      join category in _context.SecurityTipCategories on securityTip.SecurityTipCategoryId equals category.Id
        //                                      join alertLevel in _context.AlertLevels on securityTip.AlertLevelId equals alertLevel.Id
        //                                      join broadcastLevel in _context.BroadcastLevels on securityTip.BroadcastLevelId equals broadcastLevel.Id
        //                                      join district in _context.Towns on securityTip.LocationId equals district.Id
        //                                      where broadcastLevel.Name == BroadcastLevelEnum.Town.ToString() && district.Id == DistrictId

        //                                      select new GetSecurityTipResponse
        //                                      {
        //                                          Id = securityTip.Id,
        //                                          Subject = securityTip.Subject,
        //                                          Body = securityTip.Body,
        //                                          BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
        //                                          TipStatus = securityTip.TipStatusString,
        //                                          AlertLevel = alertLevel.Name,
        //                                          SecurityTipCategory = category.CategoryName,
        //                                          BroadcastLevelId = securityTip.BroadcastLevelId,
        //                                          BroadcastLocationId = securityTip.LocationId,
        //                                          BroadcasterLocationLevelId = broadcaster.LocationLevelId,
        //                                          BroadcasterLocationId = broadcaster.LocationId,
        //                                      }).Skip((pageNumber - 1) * pageSize)
        //                                 .Take(pageSize)
        //                                 .AsNoTracking()
        //                                 .ToListAsync();

        //        // Get UserandTipLocations
        //        var broadcasterAndTipLocations = await GetBroadcasterandTipLocations(UserSecuritytips.First().BroadcastLevelId, UserSecuritytips.First().BroadcastLocationId, UserSecuritytips.First().BroadcasterLocationLevelId, UserSecuritytips.First().BroadcasterLocationId);

        //        if (broadcasterAndTipLocations == null)
        //        {
        //            getSecurityTipsListResponse = null;
        //            return getSecurityTipsListResponse;
        //        }

        //        getSecurityTipsListResponse.SecurityTipsList = UserSecuritytips;

        //        foreach (var securityTip in getSecurityTipsListResponse.SecurityTipsList)
        //        {
        //            securityTip.BroadcastLevel = broadcasterAndTipLocations.BroadcastLocationLevel;
        //            securityTip.BroadcastLocation = broadcasterAndTipLocations.BroadcastLocation;
        //            securityTip.BroadcasterLocationLevel = broadcasterAndTipLocations.BroadcasterLocationLevel;
        //            securityTip.BroadcasterLocation = broadcasterAndTipLocations.BroadcasterLocation;
        //        }

        //        return getSecurityTipsListResponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"An error occurred while retrieving security tip: {ex.StackTrace}");
        //        getSecurityTipsListResponse = null;
        //        return getSecurityTipsListResponse;
        //    }
        //}
    }
}
