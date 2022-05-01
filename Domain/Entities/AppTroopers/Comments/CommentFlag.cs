using Domain.Common;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AppTroopers.Comments
{
    public class CommentFlag : AuditableBaseEntity
    {
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public bool isFlagUp { get; set; }
    }
}
