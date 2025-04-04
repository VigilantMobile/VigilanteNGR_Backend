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
    public class GetUserLocationSecurityTipsByIdQuery : IRequest<Response<GetSecurityTipsListResponse>>
    {
        public string UserId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public class GetUserLocationSecurityTipsByIdQueryHandler : IRequestHandler<GetUserLocationSecurityTipsByIdQuery, Response<GetSecurityTipsListResponse>>
        {
            private readonly ISecurityTipService _securityTipService;
            private readonly IMapper _mapper;

            public GetUserLocationSecurityTipsByIdQueryHandler(ISecurityTipService securityTipService, IMapper mapper)
            {
                _securityTipService = securityTipService;
                _mapper = mapper;
            }

            public async Task<Response<GetSecurityTipsListResponse>> Handle(GetUserLocationSecurityTipsByIdQuery query, CancellationToken cancellationToken)
            {
                var validFilter = _mapper.Map<GetSecurityTipsListQueryParameter>(query);
                var SecurityTipsForUser = await _securityTipService.GetSecurityTipsForUserRegisteredLocation(query.UserId,  validFilter.PageNumber, validFilter.PageSize);
                if (SecurityTipsForUser == null) throw new ApiException($"No security tips found for the specified User location.");
                return new Response<GetSecurityTipsListResponse>(SecurityTipsForUser, $"Security tip retrieval for user {query.UserId} successful");
            }
        }
    }
}
