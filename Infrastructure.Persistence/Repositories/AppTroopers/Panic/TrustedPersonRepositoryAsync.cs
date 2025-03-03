using Application.Features.UserProfile;
using Application.Interfaces.Repositories.AppTroopers.Panic;
using Domain.Common.Enums;
using Domain.Entities.AppTroopers.Panic;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.Panic
{
    public class TrustedPersonRepositoryAsync : GenericRepositoryAsync<TrustedPerson>, ITrustedPersonRepositoryAsync
    {
        private readonly DbSet<TrustedPerson> _trustedPerson;
        private readonly ApplicationDbContext _context;

        public TrustedPersonRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _trustedPerson = dbContext.Set<TrustedPerson>();
        }

        public async Task<TrustedPerson> IsOwnedByOwner(string Id, string ContactOwnerId)
        {
            return await _trustedPerson.Where(x => x.Id == Guid.Parse(Id) && x.InviterId == ContactOwnerId).FirstOrDefaultAsync();
        }

        public async Task<List<CustomerTrustedContactViewModel>> GetCustomerTrustedContactsAsync(string customerId)
        {
            var trustedContacts = await (from contact in _context.TrustedPeople
                                         join customer in _context.Users on contact.InviterId equals customer.Id
                                         where customer.Id == customerId
                                         join contactProfile in _context.Users on contact.PhoneNumber equals contactProfile.PhoneNumber
                                         select new CustomerTrustedContactViewModel()
                                         {
                                             EmailAddress = contact.EmailAddress,
                                             FullName = contact.FullName,
                                             PhoneNumber = contact.PhoneNumber,
                                             InvitationStatus = contact.Status.ToString(),
                                             ProfilePicUrl = contact.Status != TrustedContactStatus.Accepted ? null : contactProfile.CustomerProfileUrl
                                         }).ToListAsync();

            return trustedContacts;
        }

        public async Task<CustomerProfileViewModel> GetCustomerProfileAsync(string customerId)
        {
            var customerProfile = await (from customer in _context.Users
                                         join town in _context.Towns on customer.TownId equals town.Id
                                         join lga in _context.LGAs on town.LGAId equals lga.Id
                                         join state in _context.States on lga.StateId equals state.Id
                                         join country in _context.Countries on state.CountryId equals country.Id
                                         join subscriptionPlan in _context.Subscriptions on customer.SubscriptionId equals subscriptionPlan.Id
                                         where customer.Id == customerId
                                         select new CustomerProfileViewModel()
                                         {
                                             CustomerId = customer.Id,
                                             CustomerName = $"{customer.FirstName} {(customer.MiddleName == null ? "" : customer.MiddleName + " ")}{customer.LastName}",
                                             CustomerPhone = customer.PhoneNumber,
                                             CustomerLocation = new CustomerLocationViewModel
                                             {
                                                 Address = customer.AddressLine1,
                                                 City = new CustomerCityViewModel
                                                 {
                                                     CityId = town.Id.ToString(),
                                                     CityName = town.Name,
                                                     GoogleMapsPlaceId = town.GoogleMapsPlaceId
                                                 },
                                                 District = new CustomerDistrictViewModel
                                                 {
                                                     DistrictId = lga.Id.ToString(),
                                                     DistrictName = lga.Name,
                                                     GoogleMapsPlaceId = lga.GoogleMapsPlaceId
                                                 },
                                                 State = new CustomerStateViewModel
                                                 {
                                                     StateId = state.Id.ToString(),
                                                     StateName = state.Name,
                                                     GoogleMapsPlaceId = state.GoogleMapsPlaceId
                                                 },
                                                 Country = new CustomerCountryViewModel
                                                 {
                                                     CountryId = country.Id.ToString(),
                                                     CountryName = country.Name,
                                                     GoogleMapsPlaceId = country.GoogleMapsPlaceId
                                                 },
                                             },
                                             SubscriptionPlan = new CustomerSubscriptionPlan
                                             {
                                                 SubscriptionPlanId = subscriptionPlan.Id.ToString(),
                                                 SubscriptionPlanName = subscriptionPlan.SubscriptionName
                                             }
                                         }).FirstOrDefaultAsync();

            return customerProfile;
        }
    }
}
