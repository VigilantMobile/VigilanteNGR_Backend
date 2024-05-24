using Domain.Entities;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.LocationEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Location
{
    public interface ICountryRepositoryAsync : IGenericRepositoryAsync<Country>
    {
        //Task<bool> IsUniqueLGAinState(int StateId, string LGAName);
        //Task<LGA> GetStateWithCountryAsync(int LGAid);
        Task<List<State>> GetStatesinCountryAsync(string CountryId);
    }
}
