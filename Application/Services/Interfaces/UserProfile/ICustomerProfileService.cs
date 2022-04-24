using Application.Features.UserProfile.Customer.Queries.GetCustomerProfile;
using Application.Wrappers;
using Domain.Entities;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTip;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.UserProfile
{
    public interface ICustomerProfileService : IAutoDependencyService
    {
        Task<CustomerProfileVM> GetCustomerProfileAsync(string CustomerId);
    }
}
