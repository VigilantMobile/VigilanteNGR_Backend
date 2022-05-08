using Application.Interfaces.Repositories.AppTroopers.SecurityTip;
using Domain.Entities.AppTroopers.SecurityTips;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
