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
using Domain.Entities.AppTroopers.Panic;
using System.Linq;
using Domain.Entities.LocationEntities;
using Application.Interfaces.Repositories.AppTroopers.Panic;
using Application.Interfaces.Repositories.Location;

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
