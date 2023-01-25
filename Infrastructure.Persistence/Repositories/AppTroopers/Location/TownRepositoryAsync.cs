using Application.DTOs.SecurityTips;
using Application.Interfaces.Repositories.Location;
using Domain.Entities.LocationEntities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<bool> IsUniqueTowninLGA(string LGAId, string townName)
        {
            return await _town.Where(x => x.LGAId == Guid.Parse(LGAId) && x.Name == townName).FirstOrDefaultAsync() == null ? true : false;

            //return await _town
            //    .AllAsync(p => p.LGAId != LGAId && p.Name != townName);
        }

        public async Task<Town> GetTownWithLGAAsync(string Townid)
        {
            return await
            _context.Towns.Where(x => x.Id == Guid.Parse(Townid)).Include(l => l.LGA).FirstOrDefaultAsync();
        }

        public async Task<TownLGAState> GetTownStateAndLGAAsync(string Townid)
        {
            try
            {
                var townState = (from town in _context.Towns
                                 join lga in _context.LGAs on town.LGAId equals lga.Id
                                 join state in _context.States on lga.StateId equals state.Id
                                 select new TownLGAState
                                 {
                                     TownId = town.Id.ToString(),
                                     TownName = town.Name,
                                     LGAId= lga.Id.ToString(),
                                     LGAName = lga.Name,
                                     StateId= state.Id.ToString(),
                                     StateName= state.Name
                                 }).FirstOrDefault();

                return townState;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
