using Domain.Common;
using Domain.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.AppTroopers.SecurityTips
{
    public class AlertLevel : AuditableBaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }
        public AlertLevelEnum alertLevel { get; set; }
        public virtual ICollection<SecurityTip> SecurityTips { get; set; }
    }
}
