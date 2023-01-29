using Domain.Common;
using Domain.Entities.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.AppTroopers.Panic
{
    public class TrustedPerson : AuditableBaseEntity
    {

        [ForeignKey("Contact")]
        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }

        public string FullName { get; set; }
        public Guid TownId { get; set; }
        public string FullAddress { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
    }
}
