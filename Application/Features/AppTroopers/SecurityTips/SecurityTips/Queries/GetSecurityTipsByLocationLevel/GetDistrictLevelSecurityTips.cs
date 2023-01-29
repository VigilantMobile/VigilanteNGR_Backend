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
    public class GetDistrictLevelSecurityTipsByIdQuery : IRequest<Response<GetSecurityTipsListResponse>>
    {
        public string DistrictId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public class GetDistrictLevelSecurityTipsByIdQueryHandler : IRequestHandler<GetDistrictLevelSecurityTipsByIdQuery, Response<GetSecurityTipsListResponse>>
        {
            private readonly ISecurityTipService _securityTipService;
            private readonly IMapper _mapper;

            public GetDistrictLevelSecurityTipsByIdQueryHandler(ISecurityTipService securityTipService, IMapper mapper)
            {
                _securityTipService = securityTipService;
                _mapper = mapper;
            }

            public async Task<Response<GetSecurityTipsListResponse>> Handle(GetDistrictLevelSecurityTipsByIdQuery query, CancellationToken cancellationToken)
            {
                var validFilter = _mapper.Map<GetSecurityTipsListQueryParameter>(query);
                var SecurityTipsForDistrict = await _securityTipService.GetSecurityTipsForDistrict(query.DistrictId, validFilter.PageNumber, validFilter.PageSize);
                if (SecurityTipsForDistrict == null) throw new ApiException($"No security tips found for the specified district.");
                return new Response<GetSecurityTipsListResponse>(SecurityTipsForDistrict, $"Security tip retrieval for {SecurityTipsForDistrict.SecurityTipsList.First().BroadcasterFullLocation} district successful");
            }
        }
    }
}
