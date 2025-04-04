using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.AppTroopers.SecurityTips
{
    public class SecurityTipCategory : AuditableBaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }
        public Guid CategoryTypeId { get; set; }
        public virtual SecurityTipCategoryType CategoryType { get; set; }
        public virtual ICollection<SecurityTip> SecurityTips { get; set; }
    }
}
