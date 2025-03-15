using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AppTroopers.SecurityTips
{
    public class SecurityTipCategoryType : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation property for the related categories
        public virtual ICollection<SecurityTipCategory> Categories { get; set; }
    }
}
