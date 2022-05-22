using Domain.Entities.LocationEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Location
{
    public interface IStateRepositoryAsync : IGenericRepositoryAsync<State>
    {
        //Task<bool> IsUniqueLGAinState(int StateId, string LGAName);
        //Task<LGA> GetLGAWithStateAsync(int LGAid);
        Task<List<LGA>> GetLGAsinStateAsync(int StateId);
    }
}
