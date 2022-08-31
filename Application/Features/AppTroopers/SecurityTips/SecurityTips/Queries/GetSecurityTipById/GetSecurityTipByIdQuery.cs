using Application.Exceptions;
using Application.Services.Interfaces.AppTroopers.SecurityTips;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTips.Queries
{
    public class GetSecurityTipByIdQuery : IRequest<Response<GetSecurityTipResponse>>
    {
        public int Id { get; set; }
        public class GetSecurityTipByIdQueryHandler : IRequestHandler<GetSecurityTipByIdQuery, Response<GetSecurityTipResponse>>
        {
            private readonly ISecurityTipService _securityTipService;
            public GetSecurityTipByIdQueryHandler(ISecurityTipService securityTipService)
            {
                _securityTipService = securityTipService;
            }
            public async Task<Response<GetSecurityTipResponse>> Handle(GetSecurityTipByIdQuery query, CancellationToken cancellationToken)
            {
                var securityTip = await _securityTipService.GetSecurityTip(query.Id);
                if (securityTip == null) throw new ApiException($"Security tip not found.");

                return new Response<GetSecurityTipResponse>(securityTip, $"Security tip retrieval successful");
            }
        }
    }
}
