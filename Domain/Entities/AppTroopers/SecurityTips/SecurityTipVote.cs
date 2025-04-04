using Domain.Common.Enums;
using Domain.Common;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AppTroopers.SecurityTips
{
    public class SecurityTipVote : AuditableBaseEntity
    {
        [Required]
        [ForeignKey("ApplicationUser")]
        public string VoterId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("SecurityTip")]
        public Guid SecurityTipId { get; set; }
        public virtual SecurityTip SecurityTip { get; set; }

        public CommentVoteEnum VoteType { get; set; }
    }

}
