using Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.AppTroopers.SecurityTips
{
    public class Source : AuditableBaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string SourceName { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }
        public bool IsVerified { get; set; }
        public string LogoUrl { get; set; }

        [ForeignKey("SourceCategory")]
        public int SourceCategoryId { get; set; }
        public virtual SourceCategory SourceCategory { get; set; }
        public virtual ICollection<SecurityTip> SecurityTips { get; set; }
    }
}
