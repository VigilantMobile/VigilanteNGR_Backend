using Application.Interfaces.Repositories.Location;
using Domain.Entities.LocationEntities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
