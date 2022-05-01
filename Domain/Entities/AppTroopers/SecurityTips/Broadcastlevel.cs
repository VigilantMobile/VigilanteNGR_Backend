using Domain.Common;
using Domain.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.AppTroopers.SecurityTips
{
    public class BroadcastLevel : AuditableBaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public BroadcastLevelEnum broadcastLevel { get; set; }

        public virtual ICollection<SecurityTip> VGNGAAdminApprovedSecurityTips { get; set; }
    }
}
