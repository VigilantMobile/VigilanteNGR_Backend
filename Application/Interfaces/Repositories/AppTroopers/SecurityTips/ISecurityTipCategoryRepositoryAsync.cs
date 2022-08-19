using Domain.Entities.AppTroopers.SecurityTips;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.AppTroopers.SecurityTips
{
    public interface ISecurityTipCategoryRepositoryAsync : IGenericRepositoryAsync<SecurityTipCategory>
    {
        Task<bool> IsUniqueCategoryName(string CategoryName);
    }
}
