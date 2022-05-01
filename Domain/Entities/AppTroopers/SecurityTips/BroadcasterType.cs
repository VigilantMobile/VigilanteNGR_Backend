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
    public class BroadcasterType : AuditableBaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public BroadcasterTypeEnum Broadcaster { get; set; }
        public virtual ICollection<SecurityTip> SecurityTips { get; set; }
    }
}
