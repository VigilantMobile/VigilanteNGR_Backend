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
using Application.Features.UserProfile.Customer.Queries.GetCustomerProfile;
using Domain.Common.Enums;

namespace Infrastructure.Persistence.Services
{
    public class CustomerProfileService : ICustomerProfileService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;


        public CustomerProfileService( ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger logger)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<CustomerProfileVM> GetCustomerProfileAsync(string CustomerId)
        {
            CustomerProfileVM customerProfile = new CustomerProfileVM();
            try
            {

                //var customer = await _userManager.FindByIdAsync(CustomerId);

                var customerandLocation = (from customer in _context.Users
                                           join loclevel in _context.BroadcastLevels on customer.LocationLevelId equals loclevel.Id
                                           select new
                                           {
                                             LocationLevel = loclevel.broadcastLevel.ToString()
                                           }).FirstOrDefault();

                //Get Location(s)
                if (customerandLocation.LocationLevel == BroadcastLevelEnum.Town.ToString())
                {
                    customerProfile = (from customer in _context.Users
                                           join town in _context.Towns on customer.LocationId equals town.Id
                                           join lga in _context.LGAs on town.LGAId equals lga.Id
                                           join state in _context.States on lga.StateId equals state.Id
                                           where customer.Id == CustomerId
                                           select new CustomerProfileVM()
                                           {
                                               CustomerId = customer.Id,
                                               CustomerName = customer.FirstName,
                                               CustomerPhone = customer.PhoneNumber,
                                               CustomerLocation = new CustomerLocationVM
                                               {
                                                   CustomerState = new CustomerStateVM
                                                   {
                                                       StateId = state.Id.ToString(),
                                                       StateName = state.Name
                                                   },
                                                   CustomerLGA = new CustomerLGAVM
                                                   {

                                                       LGAId = lga.Id.ToString(),
                                                       LGAName = lga.Name,
                                                   },
                                                   CustomerDistrict = new CustomerDistrictVM
                                                   {
                                                       DistrictId = lga.Id.ToString(),
                                                       DistrictName = lga.Name
                                                   }
                                               }
                                           }).FirstOrDefault();

                }
                else if (customerandLocation.LocationLevel == BroadcastLevelEnum.LGA.ToString())
                {
                    customerProfile = (from customer in _context.Users
                                       join lga in _context.LGAs on customer.LocationId equals lga.Id
                                       join state in _context.States on lga.StateId equals state.Id
                                       where customer.Id == CustomerId
                                       select new CustomerProfileVM()
                                       {
                                           CustomerId = customer.Id,
                                           CustomerName = customer.FirstName,
                                           CustomerPhone = customer.PhoneNumber,
                                           CustomerLocation = new CustomerLocationVM
                                           {
                                               CustomerState = new CustomerStateVM
                                               {
                                                   StateId = state.Id.ToString(),
                                                   StateName = state.Name
                                               },
                                               CustomerLGA = new CustomerLGAVM
                                               {

                                                   LGAId = lga.Id.ToString(),
                                                   LGAName = lga.Name,
                                               },
                                           }
                                       }).FirstOrDefault();
                }
                else if (customerandLocation.LocationLevel == BroadcastLevelEnum.State.ToString())
                {
                    customerProfile = (from customer in _context.Users
                                       join state in _context.States on customer.LocationId equals state.Id
                                       where customer.Id == CustomerId
                                       select new CustomerProfileVM()
                                       {
                                           CustomerId = customer.Id,
                                           CustomerName = customer.FirstName,
                                           CustomerPhone = customer.PhoneNumber,
                                           CustomerLocation = new CustomerLocationVM
                                           {
                                               CustomerState = new CustomerStateVM
                                               {
                                                   StateId = state.Id.ToString(),
                                                   StateName = state.Name
                                               },
                                           }
                                       }).FirstOrDefault();
                }


                //Get Trusted Persons:
                var trustedContacts = (from contact in _context.TrustedPeople
                                       join customer in _context.Users on contact.OwnerId equals customer.Id
                                       where customer.Id == CustomerId
                                       select new CustomerTrustedContactVM()
                                       {
                                           EmailAddress = contact.EmailAddress,
                                           FullName = contact.FullName,
                                           PhoneNumber = contact.PhoneNumber,
                                       }).ToList();

                customerProfile.CustomerTrustedContacts = trustedContacts;

                return customerProfile;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving customer profile: {ex.Message}");
                return null;
            }
        }
    }
}
