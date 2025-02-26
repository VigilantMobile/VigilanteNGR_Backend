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
    public interface IStateRepositoryAsync : IGenericRepositoryAsync<State>
    {
        //Task<bool> IsUniqueLGAinState(int StateId, string LGAName);
        Task<State> GetStateWithCountryAsync(int StateId);
        Task<List<LGA>> GetLGAsinStateAsync(string StateId);
    }
}
