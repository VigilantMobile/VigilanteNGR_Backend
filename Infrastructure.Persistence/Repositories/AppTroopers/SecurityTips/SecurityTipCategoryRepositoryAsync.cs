using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Domain.Entities.AppTroopers.SecurityTips;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.SecurityTips
{
    public class SecurityTipCategoryRepositoryAsync : GenericRepositoryAsync<SecurityTipCategory>, ISecurityTipCategoryRepositoryAsync
    {
        private readonly DbSet<SecurityTipCategory> _securityTipCategory;

        public SecurityTipCategoryRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _securityTipCategory = dbContext.Set<SecurityTipCategory>();
        }

        public Task<bool> IsUniqueCategoryName(string CategoryName)
        {
            return _securityTipCategory
                .AllAsync(p => p.Name != CategoryName);
        }
    }
}
