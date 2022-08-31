using Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.AppTroopers.SecurityTips
{
    public class SecurityTipCategory : AuditableBaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }
        public virtual ICollection<SecurityTip> SecurityTips { get; set; }
    }
}
