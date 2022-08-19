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
    public class GetStateLevelSecurityTipsByIdQuery : IRequest<Response<GetSecurityTipsListResponse>>
    {
        public int StateId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public class GetStateLevelSecurityTipsByIdQueryHandler : IRequestHandler<GetStateLevelSecurityTipsByIdQuery, Response<GetSecurityTipsListResponse>>
        {
            private readonly ISecurityTipService _securityTipService;
            private readonly IMapper _mapper;

            public GetStateLevelSecurityTipsByIdQueryHandler(ISecurityTipService securityTipService, IMapper mapper)
            {
                _securityTipService = securityTipService;
                _mapper = mapper;
            }

            public async Task<Response<GetSecurityTipsListResponse>> Handle(GetStateLevelSecurityTipsByIdQuery query, CancellationToken cancellationToken)
            {
                var validFilter = _mapper.Map<GetSecurityTipsListQueryParameter>(query);
                var SecurityTipsForState = await _securityTipService.GetSecurityTipsForState(query.StateId, validFilter.PageNumber, validFilter.PageSize);
                if (SecurityTipsForState == null) throw new ApiException($"No security tips found for the specified state.");
                return new Response<GetSecurityTipsListResponse>(SecurityTipsForState, $"Security tip retrieval for {SecurityTipsForState.SecurityTipsList.First().BroadcasterLocation} state successful");
            }
        }
    }
}
