using Application.DTOs.Email;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services
{

    public interface IMailgunEmailService
    {
        Task<RestResponse> SendAsync(EmailRequest request);
    }
}
