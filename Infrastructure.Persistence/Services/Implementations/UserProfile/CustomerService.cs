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
        ICircleRepositoryAsync _trustedPersonRepositoryAsync;
        private readonly IUtilities _utilities;
        private readonly IEmailService _emailService;
        private readonly ILocationService _locationService;
        public CustomerService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICircleRepositoryAsync trustedPersonRepositoryAsync,
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
                        Status = CircleMemberInvitationStatus.Pending,
                        Created = DateTime.UtcNow.AddHours(1),
                        CreatedBy = "Admin",
                        Inviter = inviter,
                        // If the invitee is already registered, update TrustedUserId
                        InviteeId = existingUser != null ? existingUser.Id : null
                    };

                    await _context.UserCircle.AddAsync(trustedPerson);

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

                return await _trustedPersonRepositoryAsync.GetCustomerCircleMembersAsync(CustomerId);
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
                var circle = await _context.UserCircle
                    .Where(x => x.InviterId == customerId || x.InviteeId == customerId)
                    .ToListAsync();

                // Map accepted contacts to members.
                var members = circle
                    .Where(x => x.Status == CircleMemberInvitationStatus.Accepted)
                    .Select(x => new CircleMembersViewModel
                    {
                        membershipId = x.Id.ToString(),
                        userId = customerId,
                        memberId = x.InviterId == customerId ? x.InviteeId : x.InviterId,
                        status = x.Status.ToString(),
                        // If current customer is the inviter, use InviterProfileVisible for their own setting and InviteeProfileVisible for the member.
                        // Otherwise, if the customer is the invitee, use InviteeProfileVisible for themselves and InviterProfileVisible for the member.
                        userVisible = x.InviterId == customerId ? x.InviterProfileVisible : x.InviteeProfileVisible,
                        memberVisible = x.InviterId == customerId ? x.InviteeProfileVisible : x.InviterProfileVisible
                    })
                    .ToList();

                // Map pending invitations.
                var invitations = circle
                    .Where(x => x.Status == CircleMemberInvitationStatus.Pending)
                    .Select(x => new CircleMemberInvitationViewModel
                    {
                        invitationId = x.Id.ToString(),
                        userId = customerId,
                        // For pending invitations, the inviteeId remains the same.
                        inviteeId = x.InviterId == customerId ? x.InviteeId : x.InviterId,
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
                var invitation = await _context.UserCircle
                    .FirstOrDefaultAsync(x => x.InviteeId == model.userId &&
                                              x.InviterId == model.inviterId &&
                                              x.Status == CircleMemberInvitationStatus.Pending);

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
                invitation.Status = CircleMemberInvitationStatus.Accepted;
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
                var invitation = await _context.UserCircle
                    .FirstOrDefaultAsync(x => x.InviteeId == model.userId &&
                                              x.InviterId == model.inviterId &&
                                              x.Status == CircleMemberInvitationStatus.Pending);

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
                invitation.Status = CircleMemberInvitationStatus.Rejected;
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
                var connection = await _context.UserCircle.FirstOrDefaultAsync(x =>
                    x.Status == CircleMemberInvitationStatus.Accepted &&
                    x.IsActive &&
                    ((x.InviterId == model.userId && x.InviteeId == model.memberId) ||
                     (x.InviterId == model.memberId && x.InviteeId == model.userId))
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
                var connection = await _context.UserCircle.FirstOrDefaultAsync(x =>
                    x.Status == CircleMemberInvitationStatus.Accepted &&
                    !x.IsActive &&
                    ((x.InviterId == model.userId && x.InviteeId == model.memberId) ||
                     (x.InviterId == model.userId && x.InviteeId == model.memberId))
                );

                if (connection == null)
                {
                    throw new ApiException("No inactive friendship record found.");
                }

                // Update the record to indicate that a reactivation request is in progress.
                // Setting Status to Pending will require the other party to approve reactivation.
                connection.Status = CircleMemberInvitationStatus.Pending;
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
                // Retrieve the accepted connection between the two users.
                var connection = await _context.UserCircle.FirstOrDefaultAsync(x =>
                    x.Status == CircleMemberInvitationStatus.Accepted &&
                    ((x.InviterId == model.userId && x.InviteeId == model.memberId) ||
                     (x.InviterId == model.memberId && x.InviteeId == model.userId))
                );
                if (connection == null)
                {
                    throw new ApiException("Active friendship not found.");
                }

                // Determine which side of the connection is the current user and update that visibility flag.
                if (connection.InviterId == model.userId)
                {
                    connection.InviterProfileVisible = model.isProfileVisible;
                }
                else if (connection.InviteeId == model.userId)
                {
                    connection.InviteeProfileVisible = model.isProfileVisible;
                }
                else
                {
                    throw new ApiException("User is not part of the connection.");
                }

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

        //Emergency Contacts: 
        /// <summary>
        /// Toggles the emergency contact status for a circle member
        /// </summary>
        /// <param name="model">Model containing user ID, member ID, and emergency contact setting</param>
        /// <returns>True if the operation was successful, otherwise false</returns>
        public async Task<bool> ToggleEmergencyContactStatus(ToggleEmergencyContactViewModel model)
        {
            try
            {
                // Retrieve the active connection between the two users.
                var connection = await _context.UserCircle.FirstOrDefaultAsync(x =>
                    x.Status == CircleMemberInvitationStatus.Accepted &&
                    x.IsActive &&
                    ((x.InviterId == model.userId && x.InviteeId == model.memberId) ||
                     (x.InviterId == model.memberId && x.InviteeId == model.userId))
                );

                if (connection == null)
                {
                    throw new ApiException("Active circle membership not found.");
                }

                // Determine which emergency contact flag to update based on who is making the request
                if (connection.InviterId == model.userId)
                {
                    // If the user is the inviter, they're toggling whether the invitee is their emergency contact
                    connection.IsInviteeEmergencyContact = model.isEmergencyContact;
                }
                else if (connection.InviteeId == model.userId)
                {
                    // If the user is the invitee, they're toggling whether the inviter is their emergency contact
                    connection.IsInviterEmergencyContact = model.isEmergencyContact;
                }
                else
                {
                    throw new ApiException("User is not part of the connection.");
                }

                await _context.SaveChangesAsync();

                // Log the emergency contact change
                _logger.LogInformation(
                    "Emergency contact status updated: User {UserId} set {MemberId} as {Status}",
                    model.userId,
                    model.memberId,
                    model.isEmergencyContact ? "emergency contact" : "regular contact");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating emergency contact status: {ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// Gets all emergency contacts for a user
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>List of emergency contacts</returns>
        public async Task<List<EmergencyContactViewModel>> GetUserEmergencyContacts(string userId)
        {
            try
            {
                // Get all active accepted connections where the user is either inviter or invitee
                // and either the invitee or inviter is marked as emergency contact
                var emergencyContacts = await _context.UserCircle
                    .Where(x =>
                        x.Status == CircleMemberInvitationStatus.Accepted &&
                        x.IsActive &&
                        ((x.InviterId == userId && x.IsInviteeEmergencyContact) ||
                         (x.InviteeId == userId && x.IsInviterEmergencyContact)))
                    .Select(x => new EmergencyContactViewModel
                    {
                        membershipId = x.Id.ToString(),
                        userId = userId,
                        contactId = x.InviterId == userId ? x.InviteeId : x.InviterId,
                        fullName = x.FullName,
                        phoneNumber = x.PhoneNumber,
                        emailAddress = x.EmailAddress,
                        relationship = x.Relationship,
                        relationshipType = x.RelationshipType.ToString()
                    })
                    .ToListAsync();

                return emergencyContacts;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving emergency contacts: {ex.Message}", ex);
                return new List<EmergencyContactViewModel>();
            }
        }

    }
}
