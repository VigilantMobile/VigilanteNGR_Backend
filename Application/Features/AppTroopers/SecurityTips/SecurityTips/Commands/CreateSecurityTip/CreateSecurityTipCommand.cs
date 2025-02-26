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
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTips.Commands.CreateSecurityTip
{
    public partial class CreateSecurityTipCommand : IRequest<Response<CreateSecurityTipResponse>>
    {
        public string Subject { get; set; }
        public string BroadcasterId { get; set; }
        public string CategoryId { get; set; }
        public int Casualties { get; set; }
        public string AlertLevelId { get; set; }
        public string BroadcastLevelId { get; set; } // 1,2,3 for state, lga and 
        public string LocationId { get; set; }
        public string SourceId { get; set; }
        public string Body { get; set; }
        public string TipStatus { get; set; }
        public string coordinates { get; set; }
    }
    public class CreateSecurityTipCommandHandler : IRequestHandler<CreateSecurityTipCommand, Response<CreateSecurityTipResponse>>
    {
        private readonly ISecurityTipService _securityTipService;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        public CreateSecurityTipCommandHandler(ISecurityTipService securityTipService, IMapper mapper, IUserAccessor userAccessor)
        {
            _securityTipService = securityTipService;
            _mapper = mapper;
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
        }

        public async Task<Response<CreateSecurityTipResponse>> Handle(CreateSecurityTipCommand request, CancellationToken cancellationToken)
        {
           
            var createdTip = await _securityTipService.CreateSecurityTipAsync(request);
      
            if (!createdTip.IsCreated)
            {
                return new Response<CreateSecurityTipResponse>(null, responsestatus: APIResponseStatus.fail.ToString(), createdTip.Message);
            }

            else
            {
                return new Response<CreateSecurityTipResponse>(createdTip, responsestatus: APIResponseStatus.success.ToString(), createdTip.Message);
            }
        }
    }
}
