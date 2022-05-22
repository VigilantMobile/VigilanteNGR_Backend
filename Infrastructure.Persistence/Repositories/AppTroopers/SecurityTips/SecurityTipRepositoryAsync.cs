using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Domain.Entities.AppTroopers.SecurityTips;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.SecurityTips
{
    public class SecurityTipRepositoryAsync : GenericRepositoryAsync<SecurityTip>, ISecurityTipRepositoryAsync
    {
        private readonly DbSet<SecurityTip> _securityTip;

        public SecurityTipRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _securityTip = dbContext.Set<SecurityTip>();
        }
    }
}
