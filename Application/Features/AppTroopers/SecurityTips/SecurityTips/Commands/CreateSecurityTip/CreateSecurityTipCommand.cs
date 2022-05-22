using Application.Interfaces;
using Application.Services.Interfaces.AppTroopers.SecurityTips;
using Application.Wrappers;
using AutoMapper;
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
        public int CategoryId { get; set; }
        public int Casualties { get; set; }
        public int AlertLevelId { get; set; }
        public int BroadcastLevelId { get; set; } // 1,2,3 for state, lga and 
        public int LocationId { get; set; }
        public int SourceId { get; set; }
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
                return new Response<CreateSecurityTipResponse>(null, createdTip.Message, successStatus: false);
            }
            else
            {
                return new Response<CreateSecurityTipResponse>(createdTip, createdTip.Message, successStatus: true);
            }
        }
    }
}
