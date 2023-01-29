using Application.DTOs.SecurityTips;
using Domain.Entities.LocationEntities;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Location
{
    public interface ITownRepositoryAsync : IGenericRepositoryAsync<Town>
    {
        Task<bool> IsUniqueTowninLGA(string LGAId, string townName);
        Task<Town> GetTownWithLGAAsync(string Townid);
        Task<TownLGAState> GetTownStateAndLGAAsync(string Townid);
    }
}
