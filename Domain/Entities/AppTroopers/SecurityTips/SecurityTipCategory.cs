using Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.AppTroopers.SecurityTips
{
    public class SecurityTipCategory : AuditableBaseEntity
    {
        [Required]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<SecurityTip> SecurityTips { get; set; }
    }
}
