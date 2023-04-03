using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Application.Services.Interfaces.AppTroopers.SecurityTips;
using Application.Wrappers;
using AutoMapper;
using Domain.Common.Enums;
using Domain.Entities;
using Domain.Entities.AppTroopers.SecurityTips;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTips.Commands
{
    public class GetUserLiveLocationSecurityTipsQuery : IRequest<Response<GetLiveLocationSecurityTipResponse>>
    {
        public string UserId { get; set; }
        public string DesiredBroadcastLevel { get; set; }
        [Required]
        public string Coordinates { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public class GetUserLiveLocationSecurityTipsByIdQueryHandler : IRequestHandler<GetUserLiveLocationSecurityTipsQuery, Response<GetLiveLocationSecurityTipResponse>>
        {
            private readonly ISecurityTipService _securityTipService;
            private readonly IMapper _mapper;

            public GetUserLiveLocationSecurityTipsByIdQueryHandler(ISecurityTipService securityTipService, IMapper mapper)
            {
                _securityTipService = securityTipService;
                _mapper = mapper;
            }

            public async Task<Response<GetLiveLocationSecurityTipResponse>> Handle(GetUserLiveLocationSecurityTipsQuery query, CancellationToken cancellationToken)
            {
                //var validFilter = _mapper.Map<GetSecurityTipsListQueryParameter>(query);
                var SecurityTipsForUserLiveLocation = await _securityTipService.GetSecurityTipsForUserLiveLocation(query.UserId, query.DesiredBroadcastLevel, query.Coordinates, query.PageNumber, query.PageSize);
                if (!SecurityTipsForUserLiveLocation.Success) 
                    throw new ApiException($"{SecurityTipsForUserLiveLocation.Message}");

                return new Response<GetLiveLocationSecurityTipResponse>(SecurityTipsForUserLiveLocation, $"Security tip retrieval for {SecurityTipsForUserLiveLocation.SecurityTipsList.First().BroadcasterFullLocation} user successful");
            }
        }
    }
}
