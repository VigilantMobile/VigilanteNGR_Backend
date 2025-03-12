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

        [ForeignKey("Contact")]
        public string InviterId { get; set; }
        public virtual ApplicationUser Inviter { get; set; }

        // The user who is invited as a trusted contact.
        // If the invited user isn’t registered yet, you could allow this to be null and store the email separately.
        public string InviterteeId { get; set; }
        [ForeignKey(nameof(InviterteeId))]
        public virtual ApplicationUser Invitee { get; set; }
        public string FullName { get; set; }
        public string FullAddress { get; set; }
        public string EmailAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public TrustedContactStatus Status { get; set; }
        // New property: indicates whether the connection is active (true) or turned off/inactive (false)
        public bool IsActive { get; set; } = true;
        public bool isProfileVisible { get; set; } = true;
    }
}
