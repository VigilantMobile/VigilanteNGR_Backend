using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Domain.Entities.AppTroopers.SecurityTips;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.SecurityTips
{
    public class AlertLevelRepositoryAsync : GenericRepositoryAsync<AlertLevel>, IAlertLevelRespositoryAsync
    {
        private readonly DbSet<AlertLevel> _alertLevel;

        public AlertLevelRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _alertLevel = dbContext.Set<AlertLevel>();
        }

        public Task<bool> IsUniqueAlertLevel(string AlertLevelName)
        {
            return _alertLevel
                .AllAsync(p => p.Name != AlertLevelName);
        }
    }
}
