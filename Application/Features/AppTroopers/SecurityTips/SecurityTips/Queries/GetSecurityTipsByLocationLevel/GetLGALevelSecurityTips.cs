using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Application.Services.Interfaces.AppTroopers.SecurityTips;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.AppTroopers.SecurityTips;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTips.Commands
{
    public class GetLGALevelSecurityTipsByIdQuery : IRequest<Response<GetSecurityTipsListResponse>>
    {
        public int LGAId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public class GetLGALevelSecurityTipsByIdQueryHandler : IRequestHandler<GetLGALevelSecurityTipsByIdQuery, Response<GetSecurityTipsListResponse>>
        {
            private readonly ISecurityTipService _securityTipService;
            private readonly IMapper _mapper;

            public GetLGALevelSecurityTipsByIdQueryHandler(ISecurityTipService securityTipService, IMapper mapper)
            {
                _securityTipService = securityTipService;
                _mapper = mapper;
            }

            public async Task<Response<GetSecurityTipsListResponse>> Handle(GetLGALevelSecurityTipsByIdQuery query, CancellationToken cancellationToken)
            {
                var validFilter = _mapper.Map<GetSecurityTipsListQueryParameter>(query);
                var SecurityTipsForLGA = await _securityTipService.GetSecurityTipsForLGA(query.LGAId, validFilter.PageNumber, validFilter.PageSize);
                if (SecurityTipsForLGA == null) throw new ApiException($"No security tips found for the specified LGA.");
                return new Response<GetSecurityTipsListResponse>(SecurityTipsForLGA, $"Security tip retrieval for {SecurityTipsForLGA.SecurityTipsList.First().BroadcasterLocation} LGA successful");
            }
        }
    }
}
