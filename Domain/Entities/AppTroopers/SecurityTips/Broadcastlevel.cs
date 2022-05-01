using Domain.Common;
using Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
