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
    public class AlertLevelRepositoryAsync : GenericRepositoryAsync<AlertLevel>, IAlertLevelRepositoryAsync
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
