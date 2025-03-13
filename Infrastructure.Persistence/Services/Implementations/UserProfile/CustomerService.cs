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
using Microsoft.AspNetCore.Routing;
using System.ComponentModel.DataAnnotations;
using Application.Services.Interfaces.Location;

namespace Infrastructure.Persistence.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly IGeoCodingService _geoCodingService;
        ITrustedPersonRepositoryAsync _trustedPersonRepositoryAsync;
        private readonly IUtilities _utilities;
        private readonly IEmailService _emailService;
        private readonly ILocationService _locationService;
        public CustomerService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ITrustedPersonRepositoryAsync trustedPersonRepositoryAsync,
        ILogger logger, IEmailService emailService, IUtilities utilities, IGeoCodingService geoCodingService, ILocationService locationService)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _trustedPersonRepositoryAsync = trustedPersonRepositoryAsync;
            _utilities = utilities;
            _emailService = emailService;
            _geoCodingService = geoCodingService;
            _locationService = locationService;
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

                customerProfile = await _trustedPersonRepositoryAsync.GetCustomerProfileAsync(CustomerId);
                if (customerProfile != null)
                {
                    //Get Customer Trusted Contacts:
                    var trustedContacts = await GetCircleMembersAsync(CustomerId);
                    customerProfile.userCircle = trustedContacts;
                }

                return customerProfile;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving customer profile: {ex.Message}, {ex}");
                return null;
            }
        }

        //public async Task<List<CustomerTrustedContactViewModel>> CreateCustomerTrustedContactsAsync(CreateCustomerTrustedContactViewModel createCustomerTrustedContactsRequest)
        //{
        //    try
        //    {
        //        var inviter = await _userManager.FindByIdAsync(createCustomerTrustedContactsRequest.CustomerId);

        //        foreach (var contact in createCustomerTrustedContactsRequest.customerTrustedContacts)
        //        {
        //            UserCircle trustedPerson = new UserCircle
        //            {
        //                Id = Guid.NewGuid(),
        //                InviterId = createCustomerTrustedContactsRequest.CustomerId,
        //                EmailAddress = contact.EmailAddress,
        //                FullName = contact.FullName,
        //                FullAddress = contact.FullAddress,
        //                PhoneNumber = contact.PhoneNumber,
        //                Status = TrustedContactStatus.Pending,
        //                Created = DateTime.UtcNow.AddHours(1),
        //                CreatedBy = "Admin",
        //                Inviter = inviter
        //            };
        //            await _context.TrustedPeople.AddAsync(trustedPerson);

        //            //send invitation email
        //            var vglntAboutUrl = new Uri("http://vigilantng-001-site1.itempurl.com");
        //            await _emailService.SendAsync(new Application.DTOs.Email.EmailRequest()
        //            {
        //                From = "info@vigilanteng.com",
        //                To = contact.EmailAddress,
        //                Username = contact.FullName,
        //                BodyParagraph1 = $"Welcome to the Vigilant NG community. User {inviter.FirstName} {inviter.LastName} invited you to their circle. To learn more about Vigilant, visit the link below.",
        //                ButtonLabel = "Learn more!",
        //                ButtonUrl = $"{vglntAboutUrl.ToString}",
        //                BodyParagraph2 = $"Need further assistance? Email us at info@vigilant.com",
        //                Subject = $"{inviter.FirstName} invited you to join the Vigilant community. "
        //            });
        //        }

        //        await _context.SaveChangesAsync();

        //        return createCustomerTrustedContactsRequest.customerTrustedContacts;

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"An error occurred while creating customer tusted contacts: {ex.Message}, {ex}");
        //        throw ex;
        //    }
        //}

        public async Task<List<CircleMemberViewModel>> CreateCustomerTrustedContactsAsync(CreateCircleMemberViewModel createCustomerTrustedContactsRequest)
        {
            try
            {
                var inviter = await _userManager.FindByIdAsync(createCustomerTrustedContactsRequest.userId);

                foreach (var contact in createCustomerTrustedContactsRequest.circleMembers)
                {
                    // Check if the invitee already exists based on their phone number.
                    var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == contact.phoneNumber);

                    UserCircle trustedPerson = new UserCircle
                    {
                        Id = Guid.NewGuid(),
                        InviterId = createCustomerTrustedContactsRequest.userId,
                        EmailAddress = contact.emailAddress,
                        FullName = contact.fullName,
                        FullAddress = contact.fullAddress,
                        PhoneNumber = contact.phoneNumber,
                        Status = TrustedContactStatus.Pending,
                        Created = DateTime.UtcNow.AddHours(1),
                        CreatedBy = "Admin",
                        Inviter = inviter,
                        // If the invitee is already registered, update TrustedUserId
                        InviterteeId = existingUser != null ? existingUser.Id : null
                    };

                    await _context.TrustedPeople.AddAsync(trustedPerson);

                    // Only send invitation email if the invitee is not registered.
                    if (existingUser == null)
                    {
                        var vglntAboutUrl = new Uri("http://vigilantng-001-site1.itempurl.com");
                        await _emailService.SendAsync(new Application.DTOs.Email.EmailRequest()
                        {
                            From = "info@vigilanteng.com",
                            To = contact.emailAddress,
                            Username = contact.fullName,
                            BodyParagraph1 = $"Welcome to the Vigilant NG community. User {inviter.FirstName} {inviter.LastName} invited you to their circle. To learn more about Vigilant, visit the link below.",
                            ButtonLabel = "Learn more!",
                            ButtonUrl = $"{vglntAboutUrl.ToString}",
                            BodyParagraph2 = $"Need further assistance? Email us at info@vigilant.com",
                            Subject = $"{inviter.FirstName} invited you to join the Vigilant community. "
                        });
                    }
                    else
                    {
                        _logger.LogInformation($"No invitation email sent. User with phone number {contact.phoneNumber} is already registered.");
                    }
                }

                await _context.SaveChangesAsync();

                return createCustomerTrustedContactsRequest.circleMembers;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while creating customer trusted contacts: {ex.Message}, {ex}");
                throw;
            }
        }

        public async Task<List<CircleMemberViewModel>> GetCustomerTrustedContactsAsync(string CustomerId)
        {
            try
            {

                return await _trustedPersonRepositoryAsync.GetCustomerTrustedContactsAsync(CustomerId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving customer tusted contacts: {ex.Message}, {ex}");
                return null;
            }
        }

        public async Task<TrustedContactsResponseViewModel> GetCircleMembersAsync(string customerId)
        {
            try
            {
                // Retrieve all trusted contacts where the customer is involved.
                var circle = await _context.TrustedPeople
                    .Where(x => x.InviterId == customerId || x.InviterteeId == customerId)
                    .ToListAsync();

                // Map accepted contacts to members.
                var members = circle
                .Where(x => x.Status == TrustedContactStatus.Accepted)
                .Select(x => new CircleMembersViewModel
                {
                    membershipId = x.Id.ToString(),
                    userId = customerId,
                    memberId = x.InviterId == customerId ? x.InviterteeId : x.InviterId,
                    status = x.Status.ToString(),
                    userVisible = x.isProfileVisible,
                    memberVisible = x.isProfileVisible
                })
                .ToList();

                //// Map pending invitations.
                //var invitations = circle
                //    .Where(x => x.Status == TrustedContactStatus.Pending)
                //    .Select(x => new CircleMemberInvitationViewModel
                //    {
                //        invitationId = x.Id.ToString(),
                //        userId = customerId,
                //        // Determine the friend based on who is the inviter.
                //        FriendId = x.InviterId == customerId ? x.InviterteeId : x.InviterId,
                //        Status = x.Status.ToString(),
                //        // If the current customer initiated the invitation, mark as sent.
                //        SentByUser = (x.InviterId == customerId)
                //    })
                //    .ToList();
                var invitations = circle
                .Where(x => x.Status == TrustedContactStatus.Pending)
                .Select(x => new CircleMemberInvitationViewModel
                {
                    invitationId = x.Id.ToString(),
                    userId = customerId,
                    inviteeId = x.InviterId == customerId ? x.InviterteeId : x.InviterId,
                    status = x.Status.ToString(),
                    sentByUser = (x.InviterId == customerId)
                })
                .ToList();

                return new TrustedContactsResponseViewModel
                {
                    members = members,
                    invitations = invitations
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving circle members: {ex.Message}", ex);
                throw;
            }
        }


        // For multiple inviter IDs.
        //public async Task<bool> AcceptCircleInvitation(AcceptCustomerTrustedContactInvitationViewModel model)
        //{
        //    try
        //    {
        //        // Retrieve all pending invitations for the customer from the specified inviter IDs.
        //        var invitations = await _context.TrustedPeople
        //            .Where(x => x.TrustedUserId == model.CustomerId &&
        //                        model.InviterIds.Contains(x.InviterId) &&
        //                        x.Status == TrustedContactStatus.Pending)
        //            .ToListAsync();

        //        if (invitations == null || !invitations.Any())
        //        {
        //            throw new ApiException("Trusted contact invitation(s) not found.");
        //        }

        //        // Process each invitation.
        //        foreach (var invitation in invitations)
        //        {
        //            // Optionally verify the inviter exists.
        //            var inviter = await _userManager.FindByIdAsync(invitation.InviterId);
        //            if (inviter == null)
        //            {
        //                throw new ApiException($"Inviter with ID {invitation.InviterId} not found.");
        //            }

        //            // Update the invitation status.
        //            invitation.Status = TrustedContactStatus.Accepted;
        //            // Optionally update a timestamp field if available.
        //            // invitation.Updated = DateTime.UtcNow;
        //        }

        //        // Save all changes at once.
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"An error occurred while accepting trusted contacts: {ex.Message}", ex);
        //        throw;
        //    }
        //}

        public async Task<bool> AcceptCircleInvitation(AcceptCircleMemberInvitationViewModel model)
        {
            try
            {
                // Retrieve the pending invitation for this customer from the specific inviter.
                var invitation = await _context.TrustedPeople
                    .FirstOrDefaultAsync(x => x.InviterteeId == model.userId &&
                                              x.InviterId == model.inviterId &&
                                              x.Status == TrustedContactStatus.Pending);

                if (invitation == null)
                {
                    throw new ApiException("Trusted contact invitation not found.");
                }

                // Optionally verify that the inviter exists.
                var inviter = await _userManager.FindByIdAsync(model.inviterId);
                if (inviter == null)
                {
                    throw new ApiException($"Inviter with ID {model.inviterId} not found.");
                }

                // Update the invitation status to Accepted.
                invitation.Status = TrustedContactStatus.Accepted;
                // Optionally, update a timestamp if you have one (e.g., invitation.Updated = DateTime.UtcNow;)
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while accepting trusted contact invitation: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> RejectCircleInvitation(RejectCircleInvitationViewModel model)
        {
            try
            {
                // Retrieve the pending invitation for this customer from the specific inviter.
                var invitation = await _context.TrustedPeople
                    .FirstOrDefaultAsync(x => x.InviterteeId == model.userId &&
                                              x.InviterId == model.inviterId &&
                                              x.Status == TrustedContactStatus.Pending);

                if (invitation == null)
                {
                    throw new ApiException("Trusted contact invitation not found.");
                }

                // Optionally verify that the inviter exists.
                var inviter = await _userManager.FindByIdAsync(model.inviterId);
                if (inviter == null)
                {
                    throw new ApiException($"Inviter with ID {model.inviterId} not found.");
                }

                // Update the invitation status to Rejected.
                invitation.Status = TrustedContactStatus.Rejected;
                // Optionally, update a timestamp if you have one (e.g., invitation.Updated = DateTime.UtcNow;)
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while rejecting trusted contact invitation: {ex.Message}", ex);
                return false;
            }
        }

        // Circle Activation Deactivation
        public async Task<bool> DeactivateFriendship(DeactivateCircleMembershipViewModel model)
        {
            try
            {
                // Retrieve the accepted friendship (active connection) where the customer is involved.
                var connection = await _context.TrustedPeople.FirstOrDefaultAsync(x =>
                    x.Status == TrustedContactStatus.Accepted &&
                    x.IsActive &&
                    ((x.InviterId == model.userId && x.InviterteeId == model.memberId) ||
                     (x.InviterId == model.memberId && x.InviterteeId == model.userId))
                );

                if (connection == null)
                {
                    throw new ApiException("Active friendship not found.");
                }

                // Set the connection as inactive.
                connection.IsActive = false;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deactivating friendship: {ex.Message}", ex);
                return false;
            }
        }
        public async Task<bool> ReactivateFriendship(ReactivateCircleMembershipViewModel model)
        {
            try
            {
                // Retrieve the inactive friendship record where the connection is currently accepted but deactivated.
                var connection = await _context.TrustedPeople.FirstOrDefaultAsync(x =>
                    x.Status == TrustedContactStatus.Accepted &&
                    !x.IsActive &&
                    ((x.InviterId == model.userId && x.InviterteeId == model.memberId) ||
                     (x.InviterId == model.userId && x.InviterteeId == model.memberId))
                );

                if (connection == null)
                {
                    throw new ApiException("No inactive friendship record found.");
                }

                // Update the record to indicate that a reactivation request is in progress.
                // Setting Status to Pending will require the other party to approve reactivation.
                connection.Status = TrustedContactStatus.Pending;
                // Note: IsActive remains false until the other user accepts the reactivation.

                await _context.SaveChangesAsync();
                // Optionally, notify the other user that a reactivation request has been initiated.
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error reactivating friendship: {ex.Message}", ex);
                return false;
            }
        }

        // End Circle Activation/Deactivation

        public async Task<bool> UpdateCustomerProfilePicUrl(string customerProfilePicUrl, string CustomerId)
        {
            try
            {
                var customer = await _userManager.FindByIdAsync(CustomerId);
                if (customer == null)
                {
                    throw new ApiException($"Customer with ID {CustomerId} not found.");
                }

                customer.CustomerProfileUrl = customerProfilePicUrl;
                await _userManager.UpdateAsync(customer);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating customer profile picture URL: {ex.Message}, {ex}");
                throw;
            }
        }

        //Circle Visibility 
        public async Task<bool> ToggleCustomerProfileVisibility(ToggleCircleMembershipVisibilityViewModel model)
        {
            try
            {
                // Retrieve the friendship record where the connection is currently accepted.
                var connection = await _context.TrustedPeople.FirstOrDefaultAsync(x =>
                    x.Status == TrustedContactStatus.Accepted &&
                    ((x.InviterId == model.userId && x.InviterteeId == model.memberId) ||
                     (x.InviterId == model.memberId && x.InviterteeId == model.userId))
                );
                if (connection == null)
                {
                    throw new ApiException("Active friendship not found.");
                }
                // Update the record to indicate that the profile visibility has been toggled.
                connection.isProfileVisible = model.isProfileVisible;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating profile visibility: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool>  UpdateCustomerProfileAsync(UpdateUserProfileViewModel model)
        {
            try
            {
                var customer = await _userManager.FindByIdAsync(model.userId);
                if (customer == null)
                {
                    throw new ApiException($"Customer with ID {model.userId} not found.");
                }

                if (!string.IsNullOrEmpty(model.fullName))
                {
                    var nameParts = model.fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    if (nameParts.Length > 0)
                    {
                        customer.FirstName = nameParts[0];
                        customer.LastName = nameParts.Length > 1 ? nameParts[nameParts.Length - 1] : string.Empty;
                        customer.MiddleName = nameParts.Length > 2
                            ? string.Join(" ", nameParts.Skip(1).Take(nameParts.Length - 2))
                            : string.Empty;
                    }
                    else
                    {
                        // Handle the case where the full name is empty or whitespace
                        customer.FirstName = string.Empty;
                        customer.LastName = string.Empty;
                        customer.MiddleName = string.Empty;
                    }
                }

                // Get detailed location information from coordinates
                if (!string.IsNullOrEmpty(model.coordinates))
                {
                    var customerDetailedLocation = await _geoCodingService.GetCustomerLiveAddresses(model.coordinates);
                    if (customerDetailedLocation == null)
                    {
                        throw new ApiException("Invalid coordinates provided.");
                    }

                    // Update customer address based on the location information
                    customer.AddressLine1 = customerDetailedLocation.Data.FormattedAddress;
                    customer.TownId = await _geoCodingService.GetOrCreateTownIdAsync(customerDetailedLocation.Data.TownOrDistrict, customerDetailedLocation.Data.CountryOrDistrictOrLGA, customerDetailedLocation.Data.StateOrProvinceOrRegion, customerDetailedLocation.Data.Country);
                }

                var result = await _userManager.UpdateAsync(customer);
                if (!result.Succeeded)
                {
                    throw new ApiException("Failed to update customer profile.");
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating customer profile: {ex.Message}, {ex}");
                return false;
            }
        }
    }
}
