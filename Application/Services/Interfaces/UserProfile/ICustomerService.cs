using Application.Features.UserProfile;
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
        Task<List<CircleMemberViewModel>> GetCustomerTrustedContactsAsync(string CustomerId);
        Task<List<CircleMemberViewModel>> CreateCustomerTrustedContactsAsync(CreateCircleMemberViewModel customerTrustedContacts);
        Task<bool> UpdateCustomerProfilePicUrl(string customerProfilePicUrl, string CustomerId);
        Task<bool> RejectCircleInvitation(RejectCircleInvitationViewModel model);
        Task<bool> AcceptCircleInvitation(AcceptCircleMemberInvitationViewModel model);
        Task<bool> DeactivateFriendship(DeactivateCircleMembershipViewModel model);
        Task<bool> ReactivateFriendship(ReactivateCircleMembershipViewModel model);
        Task<bool> UpdateCustomerProfileAsync(UpdateUserProfileViewModel model);
        Task<bool> ToggleCustomerProfileVisibility(ToggleCircleMembershipVisibilityViewModel model);
        Task<bool> ToggleEmergencyContactStatus(ToggleEmergencyContactViewModel model);
        Task<List<EmergencyContactViewModel>> GetUserEmergencyContacts(string userId);
    }
}
