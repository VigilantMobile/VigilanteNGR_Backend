using Application.Features.AppTroopers.SecurityTips.Commands;
using Application.Features.AppTroopers.SecurityTips.Commands.CreateSecurityTip;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.AppTroopers.SecurityTips
{
    public interface ISecurityTipService : IAutoDependencyService
    {
        Task<CreateSecurityTipResponse> CreateSecurityTipAsync(CreateSecurityTipCommand securityTipRequest);
    }
}
