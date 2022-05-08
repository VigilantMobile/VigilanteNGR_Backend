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

        public async Task<CreateSecurityTipEligibilityResponse> GetSecurityTipPostEligibility(string CustomerId, int PostLocationId, int PostLocationLevel, int alertLevelId, string currentLocationCoordinates = null)
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
                var customerLocationandLevel = (from customer in _context.Users
                                             join loclevel in _context.BroadcastLevels on customer.LocationLevelId equals loclevel.Id
                                             select new
                                             {
                                                 LocationId = customer.LocationId,
                                                 LocationLevelId = customer.LocationLevelId,
                                                 LocationLevel = loclevel.broadcastLevel.ToString()
                                             }).FirstOrDefault();



                if (PostLocationLevel == 1) //proposed location is state
                {
                    CanImmediatelyBroadcast = false; //categorically


                    if ((customerLocationandLevel.LocationLevel == BroadcastLevelEnum.State.ToString()))
                    {
                        var customerState = await _stateRepositoryAsync.GetByIdAsync(customerLocationandLevel.LocationId);
                        //check eligibility for specific state
                        if (customerLocationandLevel.LocationId == PostLocationId) // check if customer is registered with state
                        {
                            canPostTip = true;
                        }
                        else if (customerLiveLocation.Data?.StateName == customerState.Name) //customer live location is in state
                        {
                            canPostTip = true;
                        }
                        else
                        {
                            canPostTip = false;
                            createSecurityTipEligibilityResponse.FailureReason = $"Oops, tip could not be created: To post a tip for {customerState.Name} state, it must either be your registered state or your live location.";
                        }
                    }
                    else if (customerLocationandLevel.LocationLevel == BroadcastLevelEnum.LGA.ToString())
                    {
                        CanImmediatelyBroadcast = false;
                        var customerLGAWithState = await _lGARepositoryAsync.GetLGAWithStateAsync(customerLocationandLevel.LocationId);
                        if (customerLGAWithState.State.Id == PostLocationId) //check if customer lga is in state
                        {
                            canPostTip = true;
                        }
                        else if (customerLiveLocation.Data?.LGAName == customerLGAWithState.Name) //customer is in state
                        {
                            canPostTip = true;
                        }
                        else
                        {
                            canPostTip = false;
                            createSecurityTipEligibilityResponse.FailureReason = $"Oops, tip could not be created: To post this tip, the LGA must be in your registered state or your live location.";

                        }

                        if (canPostTip)
                        {
                            EscalationRequested = true;
                        }
                    }

                    else if (customerLocationandLevel.LocationLevel == BroadcastLevelEnum.Town.ToString())
                    {

                        var customerTownWithState = await _townRepositoryAsync.GetTownStateAsync(customerLocationandLevel.LocationId); //check if town is in state
                        if (customerTownWithState.Id == PostLocationId)
                        {
                            canPostTip = true;
                        }
                        else if (customerTownWithState.Name.Contains(customerLiveLocation.Data.DistrictName)) //customer is in state
                        {
                            canPostTip = true;
                        }
                        else
                        {
                            canPostTip = false;
                            canPostTip = false;
                            createSecurityTipEligibilityResponse.FailureReason = $"Oops, tip could not be created: To post this tip, the district must be in your registered state or your live location.";

                        }

                        if (canPostTip)
                        {
                            EscalationRequested = true;
                        }
                    }
                }
                else if (PostLocationLevel == 2) //Post location is LGA
                {
                    CanImmediatelyBroadcast = false; //categorically

                    if ((customerLocationandLevel.LocationLevel == BroadcastLevelEnum.State.ToString())) //Customer Location level is state
                    {

                        //check eligibility for specific lga
                        if (currentLocationCoordinates != null)
                        {
                            canPostTip = true;
                        }
                        else
                        {
                            canPostTip = false;
                            createSecurityTipEligibilityResponse.FailureReason = $"Oops, tip could not be created: To post a tip for this district it must be your live location.";

                        }
                    }
                    else if (customerLocationandLevel.LocationLevel == BroadcastLevelEnum.LGA.ToString())
                    {
                        if (customerLocationandLevel.LocationId == PostLocationId) // customer post location lga is same as customer registered lga.
                        {
                            canPostTip = true;
                        }

                        else if (currentLocationCoordinates != null) //customer is in state
                        {
                            canPostTip = true;
                        }
                        else
                        {
                            canPostTip = false;
                            createSecurityTipEligibilityResponse.FailureReason = $"Oops, tip could not be created: To post this tip, the lga must be your registered lga or your live location.";

                        }
                    }

                    else if (customerLocationandLevel.LocationLevel == BroadcastLevelEnum.Town.ToString())
                    {
                        var customerTownWithLGA = await _townRepositoryAsync.GetTownWithLGAAsync(customerLocationandLevel.LocationId); //check if town is in state
                        if (customerTownWithLGA.Id == PostLocationId)
                        {
                            canPostTip = true;
                        }
                        else if (currentLocationCoordinates != null) //customer town is not in lga
                        {
                            canPostTip = true;
                        }
                        else
                        {
                            canPostTip = false;
                            createSecurityTipEligibilityResponse.FailureReason = $"Oops, tip could not be created: To post this tip, the district must be in your registered lga or your live location.";
                        }

                        if (canPostTip)
                        {
                            EscalationRequested = true;
                        }
                    }
                }

                else if (PostLocationLevel == 3) //Post location is Town
                {
                    if ((customerLocationandLevel.LocationLevel == BroadcastLevelEnum.State.ToString()) || (customerLocationandLevel.LocationLevel == BroadcastLevelEnum.LGA.ToString()))
                    {
                        if (currentLocationCoordinates != null) //customer not in specified town
                        {
                            canPostTip = true;
                        }
                        else
                        {
                            canPostTip = false;
                            createSecurityTipEligibilityResponse.FailureReason = $"Oops, tip could not be created: To post this tip, the district must be your live location.";

                        }
                    }

                    else if (customerLocationandLevel.LocationLevel == BroadcastLevelEnum.Town.ToString())
                    {

                        if (customerLocationandLevel.LocationId == PostLocationId) // customer post location lga is same as customer registered lga.
                        {
                            canPostTip = true;
                        }
                        else if (currentLocationCoordinates != null) //customer town is not in lga
                        {
                            canPostTip = true;
                        }
                        else
                        {
                            canPostTip = false;
                            createSecurityTipEligibilityResponse.FailureReason = $"Oops, tip could not be created: To post this tip, the district must be your registered district or your live location.";

                        }

                        if (canPostTip)
                        {
                            if (alertLevel.alertLevel == AlertLevelEnum.Critical)
                            {
                                CanImmediatelyBroadcast = true;
                            }

                        }
                    }
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
