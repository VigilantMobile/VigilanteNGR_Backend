using Domain.Common;
using Domain.Entities.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.AppTroopers.SecurityTips
{
    public class Comment : AuditableBaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string UserComment { get; set; }

        //Tip Status
        [ForeignKey("SecurityTip")]
        public int SecurityTipId { get; set; }
        public virtual SecurityTip SecurityTip { get; set; }

        //broadcaster
        [Required]
        [ForeignKey("ApplicationUser")]
        public string CommenterId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<CommentFlags> CommentFlags { get; set; }
    }

}
