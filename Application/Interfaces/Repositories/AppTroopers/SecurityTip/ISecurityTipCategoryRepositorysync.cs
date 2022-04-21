using Domain.Entities;
using Domain.Entities.AppTroopers.SecurityTip;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.AppTroopers.SecurityTip
{
    public interface ISecurityTipCategoryRepositorysync : IGenericRepositoryAsync<SecurityTipCategory>
    {
        Task<bool> IsUniqueCategoryName(string CategoryName);

    }
}
