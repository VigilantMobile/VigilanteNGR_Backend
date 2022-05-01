using Domain.Entities.AppTroopers.SecurityTips;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.AppTroopers.SecurityTip
{
    public interface ISecurityTipCategoryRepositorysync : IGenericRepositoryAsync<SecurityTipCategory>
    {
        Task<bool> IsUniqueCategoryName(string CategoryName);

    }
}
