using Domain.Entities.AppTroopers.Panic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.AppTroopers.Panic
{
    public interface ITrustedPersonRepositoryAsync : IGenericRepositoryAsync<TrustedPerson>
    {
        Task<TrustedPerson> IsOwnedByOwner(string Id, string ContactOwnerId);

    }
}
