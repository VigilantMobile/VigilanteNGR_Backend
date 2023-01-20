using Application.DTOs.Email;
using Application.Services.Interfaces;
using Infrastructure.Shared.Services.Notification.SMSHelperClasses;
using RestSharp;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services
{
    public interface ISMSService
    {
        Task<RestResponse> SendSMSAsync(SMSRequest request);
        Task<RestResponse> SendOTPRequest(GenerateOTPrequest request);

    }
}
