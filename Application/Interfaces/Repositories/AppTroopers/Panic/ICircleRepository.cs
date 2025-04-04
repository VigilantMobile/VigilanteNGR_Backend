using Application.Features.UserProfile;
using Domain.Entities.AppTroopers.Panic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.AppTroopers.Panic
{
    public interface ICircleRepositoryAsync : IGenericRepositoryAsync<UserCircle>
    {
        Task<UserCircle> IsOwnedByOwner(string Id, string ContactOwnerId);
        Task<List<CircleMemberViewModel>> GetCustomerCircleMembersAsync(string customerId);
        Task<CustomerProfileViewModel> GetCustomerProfileAsync(string customerId);

    }
}
