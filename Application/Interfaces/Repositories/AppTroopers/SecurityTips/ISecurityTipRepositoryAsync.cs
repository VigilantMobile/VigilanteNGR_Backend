using Application.DTOs.SecurityTips;
using Application.Features.AppTroopers.SecurityTips;
using Domain.Entities;
using Domain.Entities.AppTroopers.SecurityTips;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.AppTroopers.SecurityTips
{
    public interface ISecurityTipRepositoryAsync : IGenericRepositoryAsync<SecurityTip>
    {
        Task<GetLiveLocationSecurityTipResponse> GetSecurityTipDataForTown(string Townid, int pageNumber, int pageSize);
        Task<GetLiveLocationSecurityTipResponse> GetSecurityTipDataForLGA(string LGAId, int pageNumber, int pageSize);
        Task<GetLiveLocationSecurityTipResponse> GetSecurityTipDataForState(string StateId, int pageNumber, int pageSize);
    }
}
