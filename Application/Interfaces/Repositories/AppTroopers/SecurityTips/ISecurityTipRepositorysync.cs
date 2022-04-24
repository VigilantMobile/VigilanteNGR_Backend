using Domain.Entities;
using Domain.Entities.AppTroopers.SecurityTips;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.AppTroopers.SecurityTips
{
    public interface ISecurityTipRepositorysync : IGenericRepositoryAsync<SecurityTip>
    {
        Task<bool> CustomerCanPostInSpecifiedLocation(string CustomerId, string LocationId);

    }
}
