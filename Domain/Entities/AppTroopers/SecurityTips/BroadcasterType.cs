using Domain.Common;
using Domain.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.AppTroopers.SecurityTips
{
    public class BroadcasterType : AuditableBaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        public BroadcasterTypeEnum Broadcaster { get; set; }
        public virtual ICollection<SecurityTip> SecurityTips { get; set; }
    }
}
