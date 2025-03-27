using Application.Features.AppTroopers.SecurityTips;
using Application.Features.AppTroopers.SecurityTips.Commands.CreateSecurityTip;
using Application.Features.Location;
using Application.Wrappers;
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
        //Task<BroadcasterandTipLocations> GetBroadcasterandTipLocations(string BroadcastLevelId, string BroadcastLocationId, string BroadcasterTownId);
        Task<GetSecurityTipResponse> GetSecurityTipByIdAsync(string securityTipId);
        Task<GetSecurityTipsListResponse> GetSecurityTipsPostedByUser(string Userid, int pageNumber, int pageSize);
        Task<GetSecurityTipsListResponse> GetSecurityTipsForUserLiveLocation(string Userid, string BroadcastLevel, int pageNumber, int pageSize);
        Task<AlertLevelEnum> DetermineAlertLevelAsync(Guid categoryId, int casualties);
        Task<GetSecurityTipsListResponse> GetSecurityTipsForUserRegisteredLocation(string Userid, int pageNumber, int pageSize);
        Task<CreateSecurityTipResponse> CreateSecurityTipForAdminAsync(CreateSecurityTipCommand securityTipRequest);
        //Comments and Votes
        Task<GetSecurityTipResponse> ToggleSecurityTipVoteAsync(string userId, string securityTipId, CommentVoteEnum voteType);
        Task<GetSecurityTipResponse> ToggleCommentVoteAsync(string userId, string commentId, CommentVoteEnum voteType);
        Task<GetSecurityTipResponse> CreateCommentAsync(string userId, string securityTipId, string comment);
        Task<GetSecurityTipResponse> UpdateCommentAsync(string userId, string commentId, string updatedComment);
        Task<GetSecurityTipResponse> DeleteCommentAsync(string userId, string securityTipId, string commentId);

        //LocationSpecific
        Task<GetSecurityTipsListResponse> GetSecurityTipsForState(string StateId, int pageNumber, int pageSize);
        Task<GetSecurityTipsListResponse> GetSecurityTipsForLGA(string LGAId, int pageNumber, int PageSize);
        Task<GetSecurityTipsListResponse> GetSecurityTipsForTown(string DistrictId, int pageNumber, int PageSize);
    }
}
