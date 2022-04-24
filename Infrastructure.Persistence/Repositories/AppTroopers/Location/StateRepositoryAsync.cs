using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.AppTroopers.SecurityTip;
using Domain.Entities.AppTroopers.Panic;
using System.Linq;
using Domain.Entities.LocationEntities;
using Application.Interfaces.Repositories.AppTroopers.Panic;
using Application.Interfaces.Repositories.Location;

namespace Infrastructure.Persistence.Repositories.Location
{
    public class StateRepositoryAsync : GenericRepositoryAsync<State>, IStateRepositoryAsync
    {
        private readonly DbSet<LGA> _lga;

        public StateRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _lga = dbContext.Set<LGA>();
        }
       
        public async Task<LGA> GetLGAWithStateAsync(int id)
        {
            return await
            _lga.Where(x => x.Id == id).Include(l => l.State).FirstOrDefaultAsync();
        }

        public async Task<List<LGA>> GetLGAsinStateAsync(int StateId)
        {
            return await
            _lga.Where(x => x.StateId == StateId).ToListAsync();
        }
    }
}
