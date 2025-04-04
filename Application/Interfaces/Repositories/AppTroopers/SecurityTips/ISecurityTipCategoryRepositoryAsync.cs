using Application.DTOs.AlertCategories;
using Domain.Entities.AppTroopers.SecurityTips;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.AppTroopers.SecurityTips
{
    public interface ISecurityTipCategoryRepositoryAsync : IGenericRepositoryAsync<SecurityTipCategory>
    {
        Task<bool> IsUniqueCategoryNameAsync(string CategoryName);
        Task<List<CategoryTypeWithCategoriesDto>> GetCategoryTypesWithCategoriesAsync();

    }
}
