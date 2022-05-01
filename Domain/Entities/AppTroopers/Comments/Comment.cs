using Domain.Common;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.Identity;
using System.Collections.Generic;

namespace Domain.Entities.AppTroopers.Comments
{
    public class Comment : AuditableBaseEntity
    {
        public string Text { get; set; }
        public int OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public int TipId { get; set; }
        public virtual SecurityTip SecurityTip { get; set; }
        public ICollection<CommentFlag> CommentFlags { get; set; }
    }
}
