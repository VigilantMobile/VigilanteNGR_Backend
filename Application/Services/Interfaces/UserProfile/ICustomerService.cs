using Application.Features.UserProfile.Customer.Queries.GetCustomerProfile;
using Application.Wrappers;
using Domain.Entities;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTips;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.UserProfile
{
    public interface ICustomerService : IAutoDependencyService
    {
        Task<CustomerProfileViewModel> GetCustomerProfileAsync(string CustomerId);
        Task<List<CustomerTrustedContactViewModel>> GetCustomerTrustedContactsAsync(string CustomerId);
        Task<List<CustomerTrustedContactViewModel>> CreateCustomerTrustedContactsAsync(CreateCustomerTrustedContactViewModel customerTrustedContacts);
    }
}
