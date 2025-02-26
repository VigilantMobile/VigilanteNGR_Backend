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
    public class BroadcastLevelRepositoryAsync : GenericRepositoryAsync<BroadcastLevel>, IBroadcastLevelRespositoryAsync
    {
        private readonly DbSet<BroadcastLevel> _broadcastLevel;

        public BroadcastLevelRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _broadcastLevel = dbContext.Set<BroadcastLevel>();
        }

        public Task<bool> IsUniqueBroadcastLevel(string BroadcastLevelName)
        {
            return _broadcastLevel
                .AllAsync(p => p.Name != BroadcastLevelName);
        }
    }
}
