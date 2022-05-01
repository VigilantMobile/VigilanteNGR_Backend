using Application.Interfaces.Repositories.Location;
using Domain.Entities.LocationEntities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.Location
{
    public class TownRepositoryAsync : GenericRepositoryAsync<Town>, ITownRepositoryAsync
    {
        private readonly DbSet<Town> _town;
        private readonly ApplicationDbContext _context;

        public TownRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _town = dbContext.Set<Town>();
        }

        public async Task<bool> IsUniqueTowninLGA(int LGAId, string townName)
        {
            return await _town.Where(x => x.LGAId == LGAId && x.Name == townName).FirstOrDefaultAsync() == null ? true : false;

            //return await _town
            //    .AllAsync(p => p.LGAId != LGAId && p.Name != townName);
        }

        public async Task<Town> GetTownWithLGAAsync(int id)
        {
            return await
            _context.Towns.Where(x => x.Id == id).Include(l => l.LGA).FirstOrDefaultAsync();
        }
    }
}
