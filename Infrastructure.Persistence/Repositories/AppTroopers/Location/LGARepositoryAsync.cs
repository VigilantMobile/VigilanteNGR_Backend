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
    public class LGARepositoryAsync : GenericRepositoryAsync<LGA>, ILGARepositoryAsync
    {
        private readonly DbSet<LGA> _lga;
        private readonly ApplicationDbContext _context;

        public LGARepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _lga = dbContext.Set<LGA>();
        }

        public async Task<LGA> GetLGAWithStateAsync(int id)
        {
            return await
            _lga.Where(x => x.Id == id).Include(l => l.State).FirstOrDefaultAsync();
        }

        public async Task<List<Town>> GetDistrictsinLGAAsync(int LGAid)
        {
            return await
            _context.Towns.Where(x => x.LGAId == LGAid).ToListAsync();
        }
    }
}
