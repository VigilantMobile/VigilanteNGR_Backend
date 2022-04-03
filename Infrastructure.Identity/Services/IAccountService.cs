using Application.DTOs.Account;
using Application.Wrappers;
using Infrastructure.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress); 
        Task<Response<CustomerRegistrationResponse>> RegisterCustomerAsync(CustomerRegisterRequest request, string origin); 
        Task<Response<StaffRegistrationResponse>> RegisterStaffAsync(StaffRegisterRequest request, string origin); 
        Task<Response<string>> ConfirmEmailAsync(string userId, string code);                 
        Task ForgotPassword(ForgotPasswordRequest model, string origin); 

        //Task<string> AddRoleAsync(AddRoleModel model); 
        
        Task<Response<AuthenticationResponse>> RefreshTokenAsync(string jwtToken); 

        Task<Response<string>> ResetPassword(ResetPasswordRequest model); 
        
        bool RevokeToken(string token); 
        ApplicationUser GetById(string id);
    }
}
