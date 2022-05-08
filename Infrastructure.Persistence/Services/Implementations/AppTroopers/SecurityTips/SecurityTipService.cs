using Application.Features.AppTroopers.SecurityTips.Commands;
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
        private readonly ILGARepositoryAsync _lGARepositoryAsync;
        private readonly ITownRepositoryAsync _townRepositoryAsync;
        private readonly ISecurityTipEligibilityService _securityTipEligibilityService;
        private readonly ISecurityTipRepositoryAsync _securityTipRepositoryAsync;
        private readonly IEscalatedTipsRepositoryAsync _escalatedTipsRepositoryAsync;

        private readonly ILogger _logger;


        public SecurityTipService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, 
            ILGARepositoryAsync lGARepositoryAsync, ITownRepositoryAsync townRepositoryAsync, 
            ISecurityTipEligibilityService securityTipEligibilityService,  ILogger logger,
            ISecurityTipRepositoryAsync securityTipRepositoryAsync,
            IEscalatedTipsRepositoryAsync escalatedTipsRepositoryAsync
            )

        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _lGARepositoryAsync = lGARepositoryAsync;
            _townRepositoryAsync = townRepositoryAsync; 
            _securityTipEligibilityService = securityTipEligibilityService; 
            _securityTipRepositoryAsync = securityTipRepositoryAsync;
            _escalatedTipsRepositoryAsync = escalatedTipsRepositoryAsync;
        }

        public async Task<CreateSecurityTipResponse> CreateSecurityTipAsync(CreateSecurityTipCommand securityTipRequest)
        {
            CreateSecurityTipResponse createSecurityTipResponse   = new CreateSecurityTipResponse();
            bool isCreated = false;
            SecurityTip saveTipResult = new SecurityTip();
            try
            {
                var source = await _context.Source.Where(x => x.SourceName == SourcesEnum.VGNGAUser.ToString()).FirstOrDefaultAsync();
                //Get Post Eligibility
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
                createSecurityTipResponse.Message = "It's not you, it's us. An error occurred while creating the security tip: Please try again later.";
                return createSecurityTipResponse;
            }

        }
    }
}
