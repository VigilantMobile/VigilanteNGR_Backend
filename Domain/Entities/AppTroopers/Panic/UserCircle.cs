using Domain.Common;
using Domain.Common.Enums;
using Domain.Entities.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.AppTroopers.Panic
{
    public class UserCircle : AuditableBaseEntity
    {
        [ForeignKey("Inviter")]
        public string InviterId { get; set; }
        public virtual ApplicationUser Inviter { get; set; }

        // The user who is invited as a trusted contact.
        // If the invited user isn't registered yet, you could allow this to be null and store the email separately.
        public string InviteeId { get; set; }
        [ForeignKey(nameof(InviteeId))]
        public virtual ApplicationUser Invitee { get; set; }

        public string FullName { get; set; }
        public string FullAddress { get; set; }

        // Keep the specific relationship descriptor (brother, cousin, etc.)
        public string Relationship { get; set; }

        // Add the relationship type category (Family, Friend)
        public CircleMemberRelationshipType RelationshipType { get; set; }

        public string EmailAddress { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public CircleMemberInvitationStatus Status { get; set; }

        // Connection status
        public bool IsActive { get; set; } = true;
        public bool InviterProfileVisible { get; set; } = true;
        public bool InviteeProfileVisible { get; set; } = true;

        // Emergency contact flags
        public bool IsInviterEmergencyContact { get; set; } = false;
        public bool IsInviteeEmergencyContact { get; set; } = false;
    }
    




























}
