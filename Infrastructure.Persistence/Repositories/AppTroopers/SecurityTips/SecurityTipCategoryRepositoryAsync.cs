using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.AppTroopers.SecurityTips;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;

namespace Infrastructure.Persistence.Repositories.SecurityTips
{
    public class SecurityTipCategoryRepositoryAsync : GenericRepositoryAsync<SecurityTipCategory>, ISecurityTipCategoryRepositorysync
    {
        private readonly DbSet<SecurityTipCategory> _securityTipCategory;

        public SecurityTipCategoryRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _securityTipCategory = dbContext.Set<SecurityTipCategory>();
        }

        public Task<bool> IsUniqueCategoryName(string CategoryName)
        {
            return _securityTipCategory
                .AllAsync(p => p.CategoryName != CategoryName);
        }
    }
}
