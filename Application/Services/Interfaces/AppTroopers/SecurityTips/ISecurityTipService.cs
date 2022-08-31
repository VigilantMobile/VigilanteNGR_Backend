using Application.Features.AppTroopers.SecurityTips;
using Application.Features.AppTroopers.SecurityTips.Commands.CreateSecurityTip;
using Application.Features.Location;
using Domain.Common.Enums;
using Domain.Entities.AppTroopers.SecurityTips;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.AppTroopers.SecurityTips
{
    public interface ISecurityTipService : IAutoDependencyService
    {
        Task<CreateSecurityTipResponse> CreateSecurityTipAsync(CreateSecurityTipCommand securityTipRequest);
        Task<GetSecurityTipResponse> GetSecurityTip(int SecurityTipId);
        Task<BroadcasterandTipLocations> GetBroadcasterandTipLocations(int BroadcastLevelId, int BroadcastLocationId, int BroadcasterLocationLevelId, int BroadcasterLocationId);
        Task<GetSecurityTipsListResponse> GetSecurityTipsForUser(string Userid, int pageNumber, int pageSize);
        Task<GetSecurityTipsListResponse> GetSecurityTipsForState(int StateId, int pageNumber, int pageSize);
        Task<GetSecurityTipsListResponse> GetSecurityTipsForLGA(int LGAId, int pageNumber, int PageSize);
        Task<GetSecurityTipsListResponse> GetSecurityTipsForDistrict(int DistrictId, int pageNumber, int PageSize);

        //Task<GetSecurityTipsListResponse> GetSecurityTipsForUserLiveLocation(string UserId, string coordinates, int pageNumber, int PageSize);

        //Task<GetSecurityTipResponse> GetSecurityTips(string userId, int PageNumber, int PageSize);

        //Task BroadcastTip Actually
    }
}
