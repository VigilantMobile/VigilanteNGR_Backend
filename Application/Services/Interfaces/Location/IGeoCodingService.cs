using Application.Features.AppTroopers.SecurityTips.Commands;
using Application.Wrappers;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.Location
{
    public interface IGeoCodingService : IAutoDependencyService
    {
        Task<Response<CustomerPreciseLocation>> GetCustomerLiveAddresses(string Coordinates);
    }
}
