using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Domain.Entities.AppTroopers.SecurityTips;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.SecurityTips
{
    public class EscalatedTipsRepositoryAsync : GenericRepositoryAsync<SecurityTip>, ISecurityTipRepositoryAsync
    {
        private readonly DbSet<EscalatedTip> _escalatedTips;

        public EscalatedTipsRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _escalatedTips = dbContext.Set<EscalatedTip>();
        }
    }
}
