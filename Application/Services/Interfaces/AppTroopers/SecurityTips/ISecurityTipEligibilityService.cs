using Application.Features.AppTroopers.SecurityTips.Commands;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.AppTroopers.SecurityTips
{
    public interface ISecurityTipEligibilityService : IAutoDependencyService
    {
        Task<CreateSecurityTipEligibilityResponse> GetSecurityTipPostEligibility(string CustomerId, int PostLocationId, int PostLocationLevel, int alertLevelId, string currentLocationCoordinates);
    }
}
