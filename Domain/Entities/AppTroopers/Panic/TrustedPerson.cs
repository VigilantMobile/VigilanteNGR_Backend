using Domain.Common;
using Domain.Common.Enums;
using Domain.Entities.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.AppTroopers.Panic
{
    public class TrustedPerson : AuditableBaseEntity
    {

        [ForeignKey("Contact")]
        public string InviterId { get; set; }
        public virtual ApplicationUser Owner { get; set; }

        // The user who is invited as a trusted contact.
        // If the invited user isn’t registered yet, you could allow this to be null and store the email separately.
        public string TrustedUserId { get; set; }
        [ForeignKey(nameof(TrustedUserId))]
        public virtual ApplicationUser TrustedUser { get; set; }
        public string FullName { get; set; }
        public string FullAddress { get; set; }
        public string EmailAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public TrustedContactStatus Status { get; set; }
    }
}
