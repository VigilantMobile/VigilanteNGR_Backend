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
    public class TrustedPersonRepositoryAsync : GenericRepositoryAsync<UserCircle>, ITrustedPersonRepositoryAsync
    {
        private readonly DbSet<UserCircle> _trustedPerson;
        private readonly ApplicationDbContext _context;

        public TrustedPersonRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _trustedPerson = dbContext.Set<UserCircle>();
        }

        public async Task<UserCircle> IsOwnedByOwner(string id, string contactOwnerId)
        {
            return await _trustedPerson.Where(x => x.Id == Guid.Parse(id) && x.InviterId == contactOwnerId).FirstOrDefaultAsync();
        }

        public async Task<List<CircleMemberViewModel>> GetCustomerTrustedContactsAsync(string customerId)
        {
            var trustedContacts = await (from contact in _context.UserCircle
                                         join customer in _context.Users on contact.InviterId equals customer.Id
                                         where customer.Id == customerId
                                         join contactProfile in _context.Users on contact.PhoneNumber equals contactProfile.PhoneNumber
                                         select new CircleMemberViewModel()
                                         {
                                             emailAddress = contact.EmailAddress,
                                             fullName = contact.FullName,
                                             phoneNumber = contact.PhoneNumber,
                                             invitationStatus = contact.Status.ToString(),
                                             profilePicUrl = contact.Status != TrustedContactStatus.Accepted ? null : contactProfile.CustomerProfileUrl
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
                                             userId = customer.Id,
                                             fullName = $"{customer.FirstName} {(customer.MiddleName == null ? "" : customer.MiddleName + " ")}{customer.LastName}",
                                             phoneNumber = customer.PhoneNumber,
                                             userLocation = new UserLocationViewModel
                                             {
                                                 address = customer.AddressLine1,
                                                 city = new CustomerCityViewModel
                                                 {
                                                     cityId = town.Id.ToString(),
                                                     cityName = town.Name,
                                                     googleMapsPlaceId = town.GoogleMapsPlaceId
                                                 },
                                                 district = new CustomerDistrictViewModel
                                                 {
                                                     districtId = lga.Id.ToString(),
                                                     districtName = lga.Name,
                                                     googleMapsPlaceId = lga.GoogleMapsPlaceId
                                                 },
                                                 state = new CustomerStateViewModel
                                                 {
                                                     stateId = state.Id.ToString(),
                                                     stateName = state.Name,
                                                     googleMapsPlaceId = state.GoogleMapsPlaceId
                                                 },
                                                 country = new CustomerCountryViewModel
                                                 {
                                                     countryId = country.Id.ToString(),
                                                     countryName = country.Name,
                                                     googleMapsPlaceId = country.GoogleMapsPlaceId
                                                 },
                                             },
                                             subscriptionPlan = new UserSubscriptionPlan
                                             {
                                                 subscriptionPlanId = subscriptionPlan.Id.ToString(),
                                                 subscriptionPlanName = subscriptionPlan.SubscriptionName
                                             }
                                         }).FirstOrDefaultAsync();

            return customerProfile;
        }
    }
}
