using Application.DTOs.AlertCategories;
using Application.Features.AppTroopers.SecurityTips;
using Domain.Entities.AppTroopers.SecurityTips;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.AppTroopers.SecurityTipCategories
{
    public interface ISecurityTipCategoryService : IAutoDependencyService
    {
        Task<SecurityTipCategory> GetByIdAsync(string id);
        Task<IReadOnlyList<SecurityTipCategory>> GetAllAsync();
        Task<IReadOnlyList<SecurityTipCategory>> GetPagedResponseAsync(int pageNumber, int pageSize);
        Task<SecurityTipCategory> AddAsync(SecurityTipCategory entity, string userId = null);
        Task UpdateAsync(SecurityTipCategory entity, string userId = null);
        Task DeleteAsync(SecurityTipCategory entity, string userId = null);
        Task<bool> IsUniqueCategoryNameAsync(string categoryName);
        Task<List<CategoryTypeWithCategoriesDto>> GetCategoryTypesWithCategoriesAsync();
    }
}