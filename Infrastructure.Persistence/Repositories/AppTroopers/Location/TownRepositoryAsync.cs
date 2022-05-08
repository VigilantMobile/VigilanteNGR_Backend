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

        public async Task<State> GetTownStateAsync(int id)
        {
            try
            {
                var townState = (from town in _context.Towns
                                 join lga in _context.LGAs on town.LGAId equals lga.Id
                                 join state in _context.States on lga.StateId equals state.Id
                                 select new State
                                 {
                                     Id = state.Id,
                                     Name = state.Name,
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
