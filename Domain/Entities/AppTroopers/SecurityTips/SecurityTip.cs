using Domain.Common;
using Domain.Common.Enums;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.AppTroopers.SecurityTips
{
    public class SecurityTip : AuditableBaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(100)]
        public string Body { get; set; }

        [Required]
        [ForeignKey("Source")]
        public Guid SourceId { get; set; }
        public virtual Source Source { get; set; }


        [Required]
        [ForeignKey("Town")]
        public Guid TownId { get; set; }
        public virtual Source Town { get; set; }

        public bool IsBroadcasted { get; set; }

        public int Casualties { get; set; }
        public SecurityTipStatusEnum Status { get; set; }

        // Broadcaster
        [Required]
        [ForeignKey("ApplicationUser")]
        public string BroadcasterId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        // Coverage Type - based on broadcaster => Nullable for each or Generic
        [Required]
        public string Coordinates { get; set; } // No FK

        //[Required]
        //[ForeignKey("BroadcastLevel")]
        //public Guid BroadcastLevelId { get; set; } // e.g. lga, state; tbd by broadcaster ID
        //public virtual BroadcastLevel BroadcastLevel { get; set; }

        // Broadcaster type
        public Guid BroadcasterTypeId { get; set; }
        public BroadcasterTypeEnum BroadcasterType { get; set; }

        [Required]
        [ForeignKey("SecurityTipCategory")]
        public Guid SecurityTipCategoryId { get; set; }
        public virtual SecurityTipCategory SecurityTipCategory { get; set; }

        // Replaced AlertLevelId and AlertLevel navigation property with direct enum
        public AlertLevelEnum AlertLevel { get; set; }

        // Time Information
        public DateTime IncidentDateTime { get; set; }
        public bool IsOngoing { get; set; }

        // Resolution
        //public string ResolutionNotes { get; set; }
        //public DateTime? ResolvedDate { get; set; }

        // Analytics
        public int ViewCount { get; set; }
        public int ShareCount { get; set; }
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }

        // Privacy
        //public bool IsAnonymous { get; set; }
        //public VisibilityLevel Visibility { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<SecurityTipVote> Votes { get; set; }

        public SecurityTip()
        {
            Comments = new HashSet<Comment>();
            Votes = new HashSet<SecurityTipVote>();
        }
    }
}
