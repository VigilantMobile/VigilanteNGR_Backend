using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Application.Services.Interfaces.AppTroopers.SecurityTips;
using Application.Wrappers;
using AutoMapper;
using Domain.Common.Enums;
using Domain.Entities;
using Domain.Entities.AppTroopers.SecurityTips;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTips.Commands.CreateSecurityTip
{
    public partial class CreateSecurityTipCommand : IRequest<Response<CreateSecurityTipResponse>>
    {
        public string Subject { get; set; }
        public string BroadcasterUserId { get; set; }
        public string CategoryId { get; set; }
        public int Casualties { get; set; }
        public string IncidentCoordinates { get; set; }
        public string UserCurrentCoordinates { get; set; }
        public string SourceId { get; set; }
        public DateTime? IncidentDateTime { get; set; }
        public bool IsOngoing { get; set; }
        public string Description { get; set; }
        public string coordinates { get; set; }
    }
    public class CreateSecurityTipCommandHandler : IRequestHandler<CreateSecurityTipCommand, Response<CreateSecurityTipResponse>>
    {
        private readonly ISecurityTipService _securityTipService;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly ILogger<CreateSecurityTipCommandHandler> _logger;

        public CreateSecurityTipCommandHandler(ISecurityTipService securityTipService, IMapper mapper, IUserAccessor userAccessor, ILogger<CreateSecurityTipCommandHandler> logger)
        {
            _securityTipService = securityTipService;
            _mapper = mapper;
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Response<CreateSecurityTipResponse>> Handle(CreateSecurityTipCommand request, CancellationToken cancellationToken)
        {
            // Log the request
            var requestJson = System.Text.Json.JsonSerializer.Serialize(request, new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
            });

            _logger.LogInformation("New Security Tip Request: {RequestBody}", requestJson);

            var securityalert = await _securityTipService.CreateSecurityTipAsync(request);
      
            if (!securityalert.IsCreated)
            {
                return new Response<CreateSecurityTipResponse>(null, responsestatus: APIResponseStatus.fail.ToString(), securityalert.Message);
            }

            else
            {
                return new Response<CreateSecurityTipResponse>(securityalert, responsestatus: APIResponseStatus.success.ToString(), securityalert.Message);
            }
        }
    }
}
