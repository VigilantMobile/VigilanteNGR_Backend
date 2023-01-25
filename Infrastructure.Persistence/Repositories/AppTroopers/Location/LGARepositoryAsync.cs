using Application.Interfaces.Repositories.Location;
using Domain.Entities.LocationEntities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<LGA> GetLGAWithStateAsync(string LGAid)
        {
            return await
            _lga.Where(x => x.Id == Guid.Parse(LGAid)).Include(l => l.State).FirstOrDefaultAsync();
        }

        public async Task<List<Town>> GetDistrictsinLGAAsync(string LGAid)
        {
            return await
            _context.Towns.Where(x => x.LGAId == Guid.Parse(LGAid)).ToListAsync();
        }
    }
}
