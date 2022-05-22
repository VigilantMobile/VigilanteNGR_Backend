using Domain.Common;
using Domain.Common.Enums;
using Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.AppTroopers.SecurityTips
{
    public class CommentFlags : AuditableBaseEntity
    {
        [Required]
        [ForeignKey("ApplicationUser")]
        public string VoterId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }

        public CommentVoteEnum CommentVote { get; set; }
    }

}
