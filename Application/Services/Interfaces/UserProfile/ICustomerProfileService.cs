using Application.Features.UserProfile.Customer.Queries.GetCustomerProfile;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.UserProfile
{
    public interface ICustomerProfileService : IAutoDependencyService
    {
        Task<CustomerProfileVM> GetCustomerProfileAsync(string CustomerId);
    }
}
