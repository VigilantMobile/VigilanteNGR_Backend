using Domain.Common;
using Domain.Common.Enums;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AppTroopers.SecurityTips
{
    public class CommentFlags : AuditableBaseEntity
    {
        [Required]
        [ForeignKey("ApplicationUser")]
        public string VoterId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Comment")]
        public Guid CommentId { get; set; }
        public virtual Comment Comment { get; set; }

        public CommentVoteEnum CommentVote { get; set; }
    }

}
