using Application.Features.AppTroopers.SecurityTips;
using Application.Features.AppTroopers.SecurityTips.Commands;
using Application.Features.Location;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Application.Interfaces.Repositories.Location;
using Application.Services.Interfaces.AppTroopers.SecurityTips;
using Application.Services.Interfaces.Location;
using Domain.Common.Enums;
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
    public class SecurityTipEligibilityService : ISecurityTipEligibilityService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILGARepositoryAsync _lGARepositoryAsync;
        private readonly IStateRepositoryAsync _stateRepositoryAsync;
        private readonly ITownRepositoryAsync _townRepositoryAsync;
        private readonly IAlertLevelRespositoryAsync _alertLevelRespositoryAsync;
        private readonly IGeoCodingService _geocodingService;

        private readonly ILogger _logger;

        public SecurityTipEligibilityService(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        IStateRepositoryAsync stateRepositoryAsync,
        ILGARepositoryAsync lGARepositoryAsync,
        IAlertLevelRespositoryAsync alertLevelRespositoryAsync,
        ITownRepositoryAsync townRepositoryAsync,
        IGeoCodingService geocodingService,ILogger logger)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _lGARepositoryAsync = lGARepositoryAsync;
            _stateRepositoryAsync = stateRepositoryAsync;
            _townRepositoryAsync = townRepositoryAsync; 
            _alertLevelRespositoryAsync = alertLevelRespositoryAsync;
            _geocodingService = geocodingService;
        }

        public async Task<CreateSecurityTipEligibilityResponse> GetSecurityTipPostEligibility(string CustomerId, int IncidentLocationId, int BroadcastLevel, int alertLevelId, string currentLocationCoordinates)
        {

            CreateSecurityTipEligibilityResponse createSecurityTipEligibilityResponse = new CreateSecurityTipEligibilityResponse();

           try
           {
                // Customer Live location 
                
                var customerLiveLocation = await _geocodingService.GetCustomerLiveAddresses(currentLocationCoordinates);

                var alertLevel = await _alertLevelRespositoryAsync.GetByIdAsync(alertLevelId);

                bool CanImmediatelyBroadcast = false;
                bool canPostTip = false;
                bool EscalationRequested = false;

                //1. Determine if that location is customer's location

                //If customer is not in specified location live: 
                var customerRegisteredLocation = (from customer in _context.Users
                                             join town in _context.Towns on customer.TownId equals town.Id
                                             join lga in _context.LGAs on town.LGAId equals lga.Id
                                             join state in _context.States on lga.StateId equals state.Id

                                             select new
                                             {
                                                 TownId = customer.TownId,
                                                 TownName = town.Name,
                                                 LGAId = lga.Id,
                                                 LGAName = lga.Name,
                                                 StateId = state.Id,
                                                 StateName = state.Name,
                                             }).FirstOrDefault();

                var IncidentLocation = (from incidentTown in _context.Towns
                                                  join incidentLga in _context.LGAs on incidentTown.LGAId equals incidentLga.Id
                                                  join incidentState in _context.States on incidentLga.StateId equals incidentState.Id

                                                  select new
                                                  {
                                                      TownId = incidentTown.Id,
                                                      TownName = incidentTown.Name,
                                                      LGAId = incidentLga.Id,
                                                      LGAName = incidentLga.Name,
                                                      StateId = incidentState.Id,
                                                      StateName = incidentState.Name,
                                                  }).FirstOrDefault();

                if (customerRegisteredLocation.TownId == IncidentLocation.TownId) // 
                {
                    canPostTip = true;
                }

                else if (customerLiveLocation.Data?.DistrictName == IncidentLocation.TownName) //customer live location is in state
                {
                    canPostTip = true;
                }

                else
                {
                    canPostTip = false;
                    createSecurityTipEligibilityResponse.FailureReason = $"Oops, tip could not be created: To post a tip for {customerRegisteredLocation.StateName} state, it must either be your registered state or your live location.";
                }

                if (canPostTip)
                {
                    if (BroadcastLevel == 1) //proposed post location level is state /
                    {
                        EscalationRequested = true;
                    }
                    else if (BroadcastLevel == 2) //Post location is LGA
                    {
                        EscalationRequested = true;
                    }

                    ////Alert Levels
                    //if (alertLevel.alertLevel == AlertLevelEnum.Critical)
                    //{
                    //    CanImmediatelyBroadcast = true;
                    //}

                }
                else
                {
                    canPostTip = false;
                    createSecurityTipEligibilityResponse.FailureReason = $"Oops, tip could not be created: security tips can only be posted for your registered location or live location.";
                }

                createSecurityTipEligibilityResponse.CanPostTip = canPostTip;
                createSecurityTipEligibilityResponse.CanBroadcastImmediately = CanImmediatelyBroadcast;
                createSecurityTipEligibilityResponse.EscalationRequested = EscalationRequested;

                return createSecurityTipEligibilityResponse;
           }
           catch (Exception ex)
           {
                _logger.LogError($"An error occurred while getting security tip eligibility: {ex.Message}");
                return createSecurityTipEligibilityResponse;
           }
        }
    }
}
