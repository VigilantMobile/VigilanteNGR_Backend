using Application.DTOs.Email;
using Infrastructure.Shared.Services.Notification.EmailHelperClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

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
