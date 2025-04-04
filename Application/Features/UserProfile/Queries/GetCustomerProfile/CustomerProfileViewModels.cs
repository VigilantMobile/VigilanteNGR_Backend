using Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserProfile
{
    public class CustomerProfileViewModel
    {
        public string userId { get; set; }
        public string fullName { get; set; }
        public string phoneNumber { get; set; }
        public string profilePhotoUrl { get; set; }

        [Required, MinLength(1)]
        //public List<CustomerTrustedContactViewModel> CustomerCircle { get; set; }

        public TrustedContactsResponseViewModel userCircle { get; set; }
        public UserLocationViewModel userLocation { get; set; }
        public UserSubscriptionPlan subscriptionPlan { get; set; }
    }

    public class TrustedContactsResponseViewModel
    {
        public List<CircleMembersViewModel> members { get; set; }
        public List<CircleMemberInvitationViewModel> invitations { get; set; }
    }



    public class CircleMemberInvitationViewModel
    {
        public string invitationId { get; set; }
        public string userId { get; set; }
        public string memberId { get; set; }
        public string inviteeId { get; set; }
        public string status { get; set; }
        /// <summary>
        /// Indicates if the invitation was sent by the customer.
        /// If true, the invitation is outgoing; if false, it's an incoming invitation.
        /// </summary>
        public bool sentByUser { get; set; }
        // Add relationship information
        public string relationship { get; set; }
        public string relationshipType { get; set; }
        // Add emergency contact flags
        public bool isUserEmergencyContact { get; set; }
        public bool isMemberEmergencyContact { get; set; }
    }

    public class CreateCircleMemberViewModel
    {
        public string userId { get; set; }

        [Required, MinLength(1)]
        public List<CircleMemberViewModel> circleMembers { get; set; }
    }

    //public class AcceptCustomerTrustedContactInvitationViewModel
    //{
    //    public string CustomerId { get; set; }

    //    [Required, MinLength(1)]
    //    public List<string> InviterIds { get; set; }
    //}

    public class AcceptCircleMemberInvitationViewModel
    {
        [Required]
        public string userId { get; set; }

        [Required]
        public string inviterId { get; set; }
    }

    public class RejectCircleInvitationViewModel
    {
        [Required]
        public string userId { get; set; }

        [Required]
        public string inviterId { get; set; }
    }

    public class DeactivateCircleMembershipViewModel
    {
        [Required]
        public string userId { get; set; }

        [Required]
        public string memberId { get; set; }
    }

    public class ReactivateCircleMembershipViewModel
    {
        [Required]
        public string userId { get; set; }

        [Required]
        public string memberId { get; set; }
    }

    public class ToggleCircleMembershipVisibilityViewModel
    {
        [Required]
        public string userId { get; set; }

        [Required]
        public string memberId { get; set; }

        public bool isProfileVisible { get; set; }
    }

    public class UpdateUserProfilePhotoUrlViewModel
    {
        public string userId { get; set; }

        [Required]
        public string userProfilePhotoUrl { get; set; }
    }

    public class UpdateUserProfileViewModel
    {
        [Required]
        public string userId { get; set; }
        public string fullName { get; set; }
        public string coordinates { get; set; }
    }
    public class CircleMembersViewModel
    {
        public string membershipId { get; set; }
        public string userId { get; set; }
        public string memberId { get; set; }
        public string status { get; set; }
        public bool userVisible { get; set; }
        public bool memberVisible { get; set; }
        // Add relationship information
        public string relationship { get; set; }
        public string relationshipType { get; set; }
        // Add emergency contact status
        public bool isUserEmergencyContact { get; set; }
        public bool isMemberEmergencyContact { get; set; }
    }

    public class CircleMemberViewModel
    {
        [Required]
        public string fullName { get; set; }

        [Required]
        public string fullAddress { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string emailAddress { get; set; }
        public string gender { get; set; }
        public string profilePicUrl { get; set; }
        public string invitationStatus { get; set; }
        public string relationship { get; set; }
        public CircleMemberRelationshipType relationshipType { get; set; }
        // Add emergency contact flags
        public bool? isInviterEmergencyContact { get; set; }
        public bool? isInviteeEmergencyContact { get; set; }
    }

    public class UserLocationViewModel
    {
        public string address { get; set; }
        public CustomerCityViewModel city { get; set; }
        public CustomerDistrictViewModel district { get; set; }
        public CustomerStateViewModel state { get; set; }
        public CustomerCountryViewModel country { get; set; }
    }

    public class CustomerCountryViewModel
    {
        public string countryId { get; set; }
        public string countryName { get; set; }
        public string googleMapsPlaceId { get; set; }
    }
    public class CustomerStateViewModel
    {
        public string stateId { get; set; }
        public string stateName { get; set; }
        public string googleMapsPlaceId { get; set; }

    }
    public class CustomerDistrictViewModel
    {
        public string districtId { get; set; }
        public string districtName { get; set; }
        public string googleMapsPlaceId { get; set; }

    }
    public class CustomerCityViewModel
    {
        public string cityId { get; set; }
        public string cityName { get; set; }
        public string googleMapsPlaceId { get; set; }
    }

    public class UserSubscriptionPlan
    {
        public string subscriptionPlanId { get; set; }
        public string subscriptionPlanName { get; set; }
    }

    //Emergency Contact:
    public class ToggleEmergencyContactViewModel
    {
        [Required]
        public string userId { get; set; }

        [Required]
        public string memberId { get; set; }

        [Required]
        public bool isEmergencyContact { get; set; }
    }

    public class EmergencyContactViewModel
    {
        public string membershipId { get; set; }
        public string userId { get; set; }
        public string contactId { get; set; }
        public string fullName { get; set; }
        public string phoneNumber { get; set; }
        public string emailAddress { get; set; }
        public string relationship { get; set; }
        public string relationshipType { get; set; }
    }
}
