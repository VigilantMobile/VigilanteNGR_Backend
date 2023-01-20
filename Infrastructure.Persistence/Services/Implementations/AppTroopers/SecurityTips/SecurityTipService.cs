using Application.Features.AppTroopers.SecurityTips;
using Application.Features.AppTroopers.SecurityTips.Commands.CreateSecurityTip;
using Application.Features.Location;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Application.Interfaces.Repositories.Location;
using Application.Services.Interfaces.AppTroopers.SecurityTips;
using Domain.Common.Enums;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.Identity;
using Domain.Entities.LocationEntities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories.Location;
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
        private readonly ILogger _logger;


        public SecurityTipService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, 
            IStateRepositoryAsync stateRepositoryAsync, 
            ILGARepositoryAsync lGARepositoryAsync,
            ITownRepositoryAsync townRepositoryAsync, 
            ISecurityTipEligibilityService securityTipEligibilityService,  ILogger logger,
            ISecurityTipRepositoryAsync securityTipRepositoryAsync,
            IEscalatedTipsRepositoryAsync escalatedTipsRepositoryAsync,
             IAlertLevelRespositoryAsync alertLevelRespositoryAsync,
             ISecurityTipCategoryRepositoryAsync securityTipCategoryRepositoryAsync
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
        }

        public async Task<CreateSecurityTipResponse> CreateSecurityTipAsync(CreateSecurityTipCommand securityTipRequest)
        {
            CreateSecurityTipResponse createSecurityTipResponse   = new CreateSecurityTipResponse();
            bool isCreated = false;
            SecurityTip saveTipResult = new SecurityTip();
            try
            {
                var source = await _context.Source.Where(x => x.SourceName == SourcesEnum.VGNGAUser.ToString()).FirstOrDefaultAsync();
                //Get Post Eligibilitys
                var postEligibility = await _securityTipEligibilityService.GetSecurityTipPostEligibility(securityTipRequest.BroadcasterId, securityTipRequest.LocationId,
                    securityTipRequest.BroadcastLevelId, securityTipRequest.AlertLevelId, securityTipRequest.coordinates);

                if (postEligibility.CanPostTip)
                {
                    int SavedTipId = 0;

                    SecurityTip securityTip = new SecurityTip
                    {
                        Subject = securityTipRequest.Subject, 
                        Body = securityTipRequest.Body, 
                        AlertLevelId = securityTipRequest.AlertLevelId,
                        BroadcastLevelId = securityTipRequest.BroadcastLevelId,
                        LocationId = securityTipRequest.LocationId,
                        BroadcasterId = securityTipRequest.BroadcasterId,
                        SecurityTipCategoryId = securityTipRequest.CategoryId,
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
                        SavedTipId = saveTipResult.Id;
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
                            SavedTipId = saveTipResult.Id;
                        }
                        else
                        {
                            securityTip.EscalationRequested = true;
                            saveTipResult = await _securityTipRepositoryAsync.AddAsync(securityTip);
                            SavedTipId = saveTipResult.Id;

                            EscalatedTip escalatedTip = new EscalatedTip
                            {
                                EscalationBroadcastLevelId = securityTipRequest.BroadcastLevelId,
                                EscalationLocationId = securityTipRequest.LocationId,
                                CreatedBy = securityTipRequest.BroadcasterId,
                                Created = DateTime.UtcNow.AddHours(1),
                                SecurityTipId = SavedTipId
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

        

        public async Task<BroadcasterandTipLocations> GetBroadcasterandTipLocations(int BroadcastLevelId, int BroadcastLocationId, int BroadcasterLocationLevelId, int BroadcasterLocationId)
        {
            BroadcasterandTipLocations broadcasterandTipLocations = new BroadcasterandTipLocations();
            try
            {
                //Get Tip Broadcast Location
                if (BroadcastLevelId == ((int)BroadcastLevelEnum.State))
                {
                    var state = await _stateRepositoryAsync.GetByIdAsync(BroadcastLocationId);
                    broadcasterandTipLocations.BroadcastLocationLevel = BroadcastLevelEnum.State.ToString();
                    broadcasterandTipLocations.BroadcastLocation = state.Name;
                }
                else if (BroadcastLevelId == ((int)BroadcastLevelEnum.LGA))
                {
                    var lga = await _lGARepositoryAsync.GetByIdAsync(BroadcastLocationId);
                    broadcasterandTipLocations.BroadcastLocationLevel = BroadcastLevelEnum.LGA.ToString();
                    broadcasterandTipLocations.BroadcastLocation = lga.Name;
                }
                else if (BroadcastLevelId == ((int)BroadcastLevelEnum.Town))
                {
                    var town = await _townRepositoryAsync.GetByIdAsync(BroadcastLocationId);
                    broadcasterandTipLocations.BroadcastLocationLevel = BroadcastLevelEnum.Town.ToString();
                    broadcasterandTipLocations.BroadcastLocation = town.Name;
                }

                //Get Broadcaster Details 
                if (BroadcasterLocationLevelId == ((int)BroadcastLevelEnum.State))
                {
                    var state = await _stateRepositoryAsync.GetByIdAsync(BroadcasterLocationId);
                    broadcasterandTipLocations.BroadcasterLocationLevel = BroadcastLevelEnum.State.ToString();
                    broadcasterandTipLocations.BroadcasterLocation = state.Name;
                }
                else if (BroadcasterLocationLevelId == ((int)BroadcastLevelEnum.LGA))
                {
                    var lga = await _lGARepositoryAsync.GetByIdAsync(BroadcasterLocationId);
                    broadcasterandTipLocations.BroadcasterLocationLevel = BroadcastLevelEnum.LGA.ToString();
                    broadcasterandTipLocations.BroadcasterLocation = lga.Name;
                }
                else if (BroadcasterLocationLevelId == ((int)BroadcastLevelEnum.Town))
                {
                    var town = await _townRepositoryAsync.GetByIdAsync(BroadcasterLocationId);
                    broadcasterandTipLocations.BroadcasterLocationLevel = BroadcastLevelEnum.Town.ToString();
                    broadcasterandTipLocations.BroadcasterLocation = town.Name;
                }

                return broadcasterandTipLocations;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving user and tip locations: {ex.StackTrace}:");
                broadcasterandTipLocations = null;
                return broadcasterandTipLocations;
            }
        }

        public async Task<GetSecurityTipResponse> GetSecurityTip(int SecurityTipId)
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
                                              Id =  securityTip.Id,
                                              Subject = securityTip.Subject,
                                              Body = securityTip.Body,
                                              BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}" ,
                                              TipStatus = securityTip.TipStatusString,
                                              AlertLevel = alertLevel.Name,
                                              SecurityTipCategory = category.CategoryName,
                                              BroadcastLevelId = securityTip.BroadcastLevelId,
                                              BroadcastLocationId = securityTip.LocationId,
                                              BroadcasterLocationLevelId = (int)broadcaster.TownId,
                                              BroadcasterLocationId = (int)broadcaster.TownId,
                                         }).FirstOrDefaultAsync();

                //Get Tip Broadcast Location

                var broadcasterAndTipLocations = await GetBroadcasterandTipLocations(Securitytip.BroadcastLevelId, Securitytip.BroadcastLocationId, Securitytip.BroadcasterLocationLevelId, Securitytip.BroadcasterLocationId);

                if (broadcasterAndTipLocations == null)
                {
                    securityTipResponse = null;
                    return securityTipResponse;
                }

                securityTipResponse.BroadcastLevel = broadcasterAndTipLocations.BroadcastLocationLevel;
                securityTipResponse.BroadcastLocation = broadcasterAndTipLocations.BroadcastLocation;
                securityTipResponse.BroadcasterLocationLevel = broadcasterAndTipLocations.BroadcasterLocationLevel;
                securityTipResponse.BroadcasterLocation = broadcasterAndTipLocations.BroadcasterLocation;

                return securityTipResponse;
            }
            catch (Exception ex)
            {
               _logger.LogError($"An error occurred while retrieving security tip: {ex.StackTrace}");
                securityTipResponse = null;
               return securityTipResponse;
            }
        }

        public async Task<GetSecurityTipsListResponse> GetSecurityTipsForUser(string Userid, int pageNumber, int pageSize)
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
                                             Id = securityTip.Id,
                                             Subject = securityTip.Subject,
                                             Body = securityTip.Body,
                                             BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                             TipStatus = securityTip.TipStatusString,
                                             AlertLevel = alertLevel.Name,
                                             SecurityTipCategory = category.CategoryName,
                                             BroadcastLevelId = securityTip.BroadcastLevelId,
                                             BroadcastLocationId = securityTip.LocationId,
                                             BroadcasterLocationLevelId = (int)broadcaster.TownId,
                                             BroadcasterLocationId = (int)broadcaster.TownId,
                                         }).Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .AsNoTracking()
                                         .ToListAsync();

                // Get UserandTipLocations
                var broadcasterAndTipLocations = await GetBroadcasterandTipLocations(UserSecuritytips.First().BroadcastLevelId, UserSecuritytips.First().BroadcastLocationId, UserSecuritytips.First().BroadcasterLocationLevelId, UserSecuritytips.First().BroadcasterLocationId);

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
                    securityTip.BroadcasterLocationLevel = broadcasterAndTipLocations.BroadcasterLocationLevel;
                    securityTip.BroadcasterLocation = broadcasterAndTipLocations.BroadcasterLocation;
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

        public async Task<GetSecurityTipsListResponse> GetSecurityTipsForState(int StateId, int pageNumber, int pageSize)
        {
            GetSecurityTipsListResponse getSecurityTipsListResponse = new GetSecurityTipsListResponse();

            try
            {
                var UserSecuritytips = await (from securityTip in _context.SecurityTips
                                              join broadcaster in _context.Users on securityTip.BroadcasterId equals broadcaster.Id
                                              join category in _context.SecurityTipCategories on securityTip.SecurityTipCategoryId equals category.Id
                                              join alertLevel in _context.AlertLevels on securityTip.AlertLevelId equals alertLevel.Id
                                              join broadcastLevel in _context.BroadcastLevels on securityTip.BroadcastLevelId equals broadcastLevel.Id
                                              join state in _context.States on securityTip.LocationId equals state.Id
                                              where broadcastLevel.Name == BroadcastLevelEnum.State.ToString() && state.Id == StateId
                                              && securityTip.IsBroadcasted == true

                                              select new GetSecurityTipResponse
                                              {
                                                  Id = securityTip.Id,
                                                  Subject = securityTip.Subject,
                                                  Body = securityTip.Body,
                                                  BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                                  TipStatus = securityTip.TipStatusString,
                                                  AlertLevel = alertLevel.Name,
                                                  SecurityTipCategory = category.CategoryName,
                                                  BroadcastLevelId = securityTip.BroadcastLevelId,
                                                  BroadcastLocationId = securityTip.LocationId,
                                                  BroadcasterLocationLevelId = (int)broadcaster.TownId,
                                                  BroadcasterLocationId = (int)broadcaster.TownId,
                                              }).Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .AsNoTracking()
                                         .ToListAsync();

                // Get UserandTipLocations
                var broadcasterAndTipLocations = await GetBroadcasterandTipLocations(UserSecuritytips.First().BroadcastLevelId, UserSecuritytips.First().BroadcastLocationId, UserSecuritytips.First().BroadcasterLocationLevelId, UserSecuritytips.First().BroadcasterLocationId);

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
                    securityTip.BroadcasterLocationLevel = broadcasterAndTipLocations.BroadcasterLocationLevel;
                    securityTip.BroadcasterLocation = broadcasterAndTipLocations.BroadcasterLocation;
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

        public async Task<GetSecurityTipsListResponse> GetSecurityTipsForLGA(int LGAId, int pageNumber, int pageSize)
        {
            GetSecurityTipsListResponse getSecurityTipsListResponse = new GetSecurityTipsListResponse();

            try
            {
                var UserSecuritytips = await (from securityTip in _context.SecurityTips
                                              join broadcaster in _context.Users on securityTip.BroadcasterId equals broadcaster.Id
                                              join category in _context.SecurityTipCategories on securityTip.SecurityTipCategoryId equals category.Id
                                              join alertLevel in _context.AlertLevels on securityTip.AlertLevelId equals alertLevel.Id
                                              join broadcastLevel in _context.BroadcastLevels on securityTip.BroadcastLevelId equals broadcastLevel.Id
                                              join lga in _context.LGAs on securityTip.LocationId equals lga.Id
                                              where broadcastLevel.Name == BroadcastLevelEnum.LGA.ToString() && lga.Id == LGAId
                                              && securityTip.IsBroadcasted == true

                                              select new GetSecurityTipResponse
                                              {
                                                  Id = securityTip.Id,
                                                  Subject = securityTip.Subject,
                                                  Body = securityTip.Body,
                                                  BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                                  TipStatus = securityTip.TipStatusString,
                                                  AlertLevel = alertLevel.Name,
                                                  SecurityTipCategory = category.CategoryName,
                                                  BroadcastLevelId = securityTip.BroadcastLevelId,
                                                  BroadcastLocationId = securityTip.LocationId,
                                                  BroadcasterLocationLevelId = (int)broadcaster.TownId,
                                                  BroadcasterLocationId = (int)broadcaster.TownId,
                                              }).Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .AsNoTracking()
                                         .ToListAsync();

                // Get UserandTipLocations
                var broadcasterAndTipLocations = await GetBroadcasterandTipLocations(UserSecuritytips.First().BroadcastLevelId, UserSecuritytips.First().BroadcastLocationId, UserSecuritytips.First().BroadcasterLocationLevelId, UserSecuritytips.First().BroadcasterLocationId);

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
                    securityTip.BroadcasterLocationLevel = broadcasterAndTipLocations.BroadcasterLocationLevel;
                    securityTip.BroadcasterLocation = broadcasterAndTipLocations.BroadcasterLocation;
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

        public async Task<GetSecurityTipsListResponse> GetSecurityTipsForDistrict(int DistrictId, int pageNumber, int pageSize)
        {
            GetSecurityTipsListResponse getSecurityTipsListResponse = new GetSecurityTipsListResponse();

            try
            {
                var UserSecuritytips = await (from securityTip in _context.SecurityTips
                                              join broadcaster in _context.Users on securityTip.BroadcasterId equals broadcaster.Id
                                              join category in _context.SecurityTipCategories on securityTip.SecurityTipCategoryId equals category.Id
                                              join alertLevel in _context.AlertLevels on securityTip.AlertLevelId equals alertLevel.Id
                                              join broadcastLevel in _context.BroadcastLevels on securityTip.BroadcastLevelId equals broadcastLevel.Id
                                              join district in _context.Towns on securityTip.LocationId equals district.Id
                                              where broadcastLevel.Name == BroadcastLevelEnum.Town.ToString() && district.Id == DistrictId
                                              && securityTip.IsBroadcasted == true

                                              select new GetSecurityTipResponse
                                              {
                                                  Id = securityTip.Id,
                                                  Subject = securityTip.Subject,
                                                  Body = securityTip.Body,
                                                  BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                                  TipStatus = securityTip.TipStatusString,
                                                  AlertLevel = alertLevel.Name,
                                                  SecurityTipCategory = category.CategoryName,
                                                  BroadcastLevelId = securityTip.BroadcastLevelId,
                                                  BroadcastLocationId = securityTip.LocationId,
                                                  BroadcasterLocationLevelId = (int)broadcaster.TownId,
                                                  BroadcasterLocationId = (int)broadcaster.TownId,
                                              }).Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .AsNoTracking()
                                         .ToListAsync();

                // Get UserandTipLocations
                var broadcasterAndTipLocations = await GetBroadcasterandTipLocations(UserSecuritytips.First().BroadcastLevelId, UserSecuritytips.First().BroadcastLocationId, UserSecuritytips.First().BroadcasterLocationLevelId, UserSecuritytips.First().BroadcasterLocationId);

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
                    securityTip.BroadcasterLocationLevel = broadcasterAndTipLocations.BroadcasterLocationLevel;
                    securityTip.BroadcasterLocation = broadcasterAndTipLocations.BroadcasterLocation;
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
