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
    public class CountryRepositoryAsync : GenericRepositoryAsync<Country>, ICountryRepositoryAsync
    {
        private readonly DbSet<State> _state;

        public CountryRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _state = dbContext.Set<State>();
        }
       
        //public async Task<LGA> GetStateWithCountryAsync(string id)
        //{
        //    return await
        //    _state.Where(x => x.Id == Guid.Parse(id)).Include(l => l.State).FirstOrDefaultAsync();
        //}

        public async Task<List<State>> GetStatesinCountryAsync(string CountryId)
        {
            return await
            _state.Where(x => x.CountryId == Guid.Parse(CountryId)).ToListAsync();
        }
    }
}
