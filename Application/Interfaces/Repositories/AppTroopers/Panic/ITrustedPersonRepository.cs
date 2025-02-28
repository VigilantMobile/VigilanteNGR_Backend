using Application.Features.UserProfile;
using Domain.Entities.AppTroopers.Panic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.AppTroopers.Panic
{
    public interface ITrustedPersonRepositoryAsync : IGenericRepositoryAsync<TrustedPerson>
    {
        Task<TrustedPerson> IsOwnedByOwner(string Id, string ContactOwnerId);
        Task<List<CustomerTrustedContactViewModel>> GetCustomerTrustedContactsAsync(string customerId);
        Task<CustomerProfileViewModel> GetCustomerProfileAsync(string customerId);

    }
}
