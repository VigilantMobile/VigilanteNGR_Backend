using Domain.Entities.LocationEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Location
{
    public interface ILGARepositoryAsync : IGenericRepositoryAsync<LGA>
    {
        //Task<bool> IsUniqueLGAinState(int StateId, string LGAName);
        Task<LGA> GetLGAWithStateAsync(string LGAid);
        Task<List<Town>> GetDistrictsinLGAAsync(string LGAid);
    }
}
