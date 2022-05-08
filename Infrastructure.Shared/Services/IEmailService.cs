using Application.DTOs.Email;
using RestSharp;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
        Task SendWelcomeEmailAsync(EmailRequest request);
        Task SendStaffVerificationEmailAsync(EmailRequest request);
        //Task MandrillSendAsync(EmailRequest request);
    }

    public interface IMandrillEmailService
    {
        Task<RestResponse> SendAsync(EmailRequest request);
    }

    //public interface IEmailService
    //{
    //    Task SendAsync(EmailRequest request);
    //    Task SendWelcomeEmailAsync(EmailRequest request);
    //    Task SendStaffVerificationEmailAsync(EmailRequest request);
    //}
}
