using Domain.Entities.LocationEntities;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Location
{
    public interface ITownRepositoryAsync : IGenericRepositoryAsync<Town>
    {
        Task<bool> IsUniqueTowninLGA(int LGAId, string townName);
        Task<Town> GetTownWithLGAAsync(int Townid);
        Task<State> GetTownStateAsync(int Townid);
    }
}
