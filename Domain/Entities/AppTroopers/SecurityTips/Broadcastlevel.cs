using Domain.Common;
using Domain.Common.Enums;
using Domain.Entities.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.AppTroopers.SecurityTips
{
    public class BroadcastLevel : AuditableBaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }
        public BroadcastLevelEnum broadcastLevel { get; set; }

        public virtual ICollection<SecurityTip> VGNGAAdminApprovedSecurityTips { get; set; }
        public virtual ICollection<ApplicationUser> Customers { get; set; }
        public virtual ICollection<EscalatedTip> EscalatedTips { get; set; }
    }
}
