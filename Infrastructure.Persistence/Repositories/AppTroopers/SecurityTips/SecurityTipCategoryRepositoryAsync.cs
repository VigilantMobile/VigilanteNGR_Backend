using Application.DTOs.AlertCategories;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Domain.Entities.AppTroopers.SecurityTips;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.SecurityTips
{
    public class SecurityTipCategoryRepositoryAsync : GenericRepositoryAsync<SecurityTipCategory>, ISecurityTipCategoryRepositoryAsync
    {
        private readonly DbSet<SecurityTipCategory> _securityTipCategory;
        private readonly ApplicationDbContext _context;  // Keep this field


        public SecurityTipCategoryRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _securityTipCategory = dbContext.Set<SecurityTipCategory>();
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        }

        public Task<bool> IsUniqueCategoryNameAsync(string categoryName)
        {
            return _securityTipCategory
                .AllAsync(p => p.Name != categoryName);
        }

        public async Task<List<CategoryTypeWithCategoriesDto>> GetCategoryTypesWithCategoriesAsync()
        {
            return await _context.SecurityTipCategoryTypes
                .Include(ct => ct.Categories)
                .Select(ct => new CategoryTypeWithCategoriesDto
                {
                    Id = ct.Id.ToString(),
                    Name = ct.Name,
                    Description = ct.Description,
                    Categories = ct.Categories.Select(c => new CategoryDto
                    {
                        Id = c.Id.ToString(),
                        Name = c.Name,
                        Description = c.Description,
                        IsActive = true
                    }).ToList()
                }).ToListAsync();
        }
    }
}
