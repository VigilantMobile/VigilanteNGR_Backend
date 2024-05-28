using Application.DTOs.Account;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Cache;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Enums;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Application.DTOs.Email;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Shared.Services;
using Microsoft.AspNetCore.Http;
using Infrastructure.Persistence.Helpers;
using Infrastructure.Persistence.Models;
using RestSharp;
using RestSharp.Authenticators;
using Domain.Entities.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Application.Services.Interfaces.UserProfile;
using Domain.Common.Enums;
using Infrastructure.Persistence.Models.ViewModels.CustomerProfile;
using Application.Interfaces.Repositories.AppTroopers.Panic;
using Domain.Entities.AppTroopers.Panic;
using Application.Features.UserProfile;
using Infrastructure.Persistence.Migrations;

namespace Infrastructure.Persistence.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        ITrustedPersonRepositoryAsync _trustedPersonRepositoryAsync;
        private readonly IUtilities _utilities;

        public CustomerService( ApplicationDbContext context, UserManager<ApplicationUser> userManager, ITrustedPersonRepositoryAsync trustedPersonRepositoryAsync,
        ILogger logger, IUtilities utilities)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _trustedPersonRepositoryAsync = trustedPersonRepositoryAsync;
            _utilities = utilities;
        }

        public async Task<CustomerProfileViewModel> GetCustomerProfileAsync(string CustomerId)
        {
            CustomerProfileViewModel customerProfile = new CustomerProfileViewModel();
            try
            {

                //var customer = await _userManager.FindByIdAsync(CustomerId);

                //var customerandLocation = (from customer in _context.Users
                //                           join loclevel in _context.BroadcastLevels on customer.TownId equals loclevel.Id.
                //                           select new
                //                           {
                //                             LocationLevel = loclevel.broadcastLevel.ToString()
                //                           }).FirstOrDefault();

                //Get Location(s)

                customerProfile = await (from customer in _context.Users
                                   join town in _context.Towns on customer.TownId equals town.Id
                                   join lga in _context.LGAs on town.LGAId equals lga.Id
                                   join state in _context.States on lga.StateId equals state.Id
                                   join country in _context.Countries on state.CountryId equals country.Id
                                         join subscriptionPlan in _context.Subscriptions on customer.SubscriptionId equals subscriptionPlan.Id

                                         where customer.Id == CustomerId
                                   select new CustomerProfileViewModel()
                                   {
                                       CustomerId = customer.Id,
                                       CustomerName = customer.FirstName,
                                       CustomerPhone = customer.PhoneNumber,
                                       CustomerLocation = new CustomerLocationViewModel
                                       {
                                           Address = customer.AddressLine1,
                                           City = new CustomerCityViewModel
                                           {
                                               CityId = town.Id.ToString(),
                                               CityName = town.Name
                                           },
                                           District = new CustomerDistrictViewModel
                                           {

                                               DistrictId = lga.Id.ToString(),
                                               DistrictName = lga.Name,
                                           },
                                           State = new CustomerStateViewModel
                                           {
                                               StateId = state.Id.ToString(),
                                               StateName = state.Name
                                           },
                                           Country = new CustomerCountryViewModel
                                           {
                                               CountryId = country.Id.ToString(),
                                               CountryName = country.Name
                                           },
                                       },
                                       SubscriptionPlan = new CustomerSubscriptionPlan
                                       {
                                           SubscriptionPlanId = subscriptionPlan.Id.ToString(),
                                           SubscriptionPlanName = subscriptionPlan.SubscriptionName
                                       }
                                   }).FirstOrDefaultAsync();

                //Get Customer Trusted Contacts:
                var trustedContacts = await GetCustomerTrustedContactsAsync(CustomerId);
                customerProfile.CustomerTrustedContacts = trustedContacts;

                return customerProfile;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving customer profile: {ex.Message}, {ex}");
                return null;
            }
        }

        public async Task<List<CustomerTrustedContactViewModel>> CreateCustomerTrustedContactsAsync(CreateCustomerTrustedContactViewModel createCustomerTrustedContactsRequest)
        {
            try
            {
                foreach (var contact in createCustomerTrustedContactsRequest.customerTrustedContacts)
                {
                    TrustedPerson trustedPerson = new TrustedPerson
                    {
                        Id = Guid.NewGuid(),
                        OwnerId = createCustomerTrustedContactsRequest.CustomerId,
                        EmailAddress = contact.EmailAddress,
                        FullName = contact.FullName,
                        FullAddress = contact.FullAddress,
                        Gender = contact.Gender,
                        PhoneNumber = contact.PhoneNumber,
                        TownId = Guid.Parse(contact.TownId)
                    };
                    await _context.TrustedPeople.AddAsync(trustedPerson);
                }

                await _context.SaveChangesAsync();

                return createCustomerTrustedContactsRequest.customerTrustedContacts;

            }
            catch(Exception ex)
            {
                _logger.LogError($"An error occurred while creating customer tusted contacts: {ex.Message}, {ex}");
                throw ex;
            }
        }

        public async Task<List<CustomerTrustedContactViewModel>> GetCustomerTrustedContactsAsync(string CustomerId)
        {
            try
            {
                var trustedContacts = await (from contact in _context.TrustedPeople
                                       join customer in _context.Users on contact.OwnerId equals customer.Id
                                       where customer.Id == CustomerId
                                       select new CustomerTrustedContactViewModel()
                                       {
                                           EmailAddress = contact.EmailAddress,
                                           FullName = contact.FullName,
                                           PhoneNumber = contact.PhoneNumber,
                                       }).ToListAsync();

                return trustedContacts;
            }
            catch(Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving customer tusted contacts: {ex.Message}, {ex}");
                return null;
            }
        }
    }
}
