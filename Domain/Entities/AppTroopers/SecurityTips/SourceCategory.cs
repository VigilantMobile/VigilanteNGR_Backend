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
    public class SourceCategory : AuditableBaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }
        public virtual ICollection<Source> Sources { get; set; }
    }

}
