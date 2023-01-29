using Application.DTOs.Email;
using Application.Services.Interfaces;
using RestSharp;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services
{
    public interface IEmailService
    {
        Task SendSMTPAsync(EmailRequest request);
        Task SendWelcomeEmailAsync(EmailRequest request);
        Task SendStaffVerificationEmailAsync(EmailRequest request);

        Task<RestResponse> SendAsync(EmailRequest request);
    }
}
