using Domain.Common;
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

        public bool IsBroadcasted { get; set; }

        public int Casualties { get; set; }

        // Tip Status
        [ForeignKey("SecurityTipStatus")]
        public Guid SecurityTipStatusId { get; set; }
        public virtual SecurityTipStatus SecurityTipStatus { get; set; }

        [MaxLength(100)]
        public string TipStatusString { get; set; }

        // Broadcaster
        [Required]
        [ForeignKey("ApplicationUser")]
        public string BroadcasterId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        // Admin Authorization
        public bool IsAdminAuthorized { get; set; } // Fixed casing to match C# conventions

        [ForeignKey("VGNGAAdminAuthorizer")]
        public string AdminAuthorizerID { get; set; }
        public virtual ApplicationUser VGNGAAdminAuthorizer { get; set; }

        // External - not presently in use
        [ForeignKey("ExternalInitiator")] // Fixed typo in attribute name - was "ExternalIniator"
        public string ExternalInitiatorId { get; set; }
        public virtual ApplicationUser ExternalInitiator { get; set; }

        [ForeignKey("ExternalAuthorizer")]
        public string ExternalAuthorizerId { get; set; }
        public virtual ApplicationUser ExternalAuthorizer { get; set; }

        // Coverage Type - based on broadcaster => Nullable for each or Generic
        [Required]
        public string LocationId { get; set; } // No FK

        [Required]
        [ForeignKey("BroadcastLevel")]
        public Guid BroadcastLevelId { get; set; } // e.g. lga, state; tbd by broadcaster ID
        public virtual BroadcastLevel BroadcastLevel { get; set; }

        // Escalation
        public bool EscalationRequested { get; set; }

        // Broadcaster type
        [Required]
        [ForeignKey("BroadcasterType")]
        public Guid BroadcasterTypeId { get; set; }
        public virtual BroadcasterType BroadcasterType { get; set; }

        [MaxLength(100)]
        public string BroadcasterTypeString { get; set; }

        [Required]
        [ForeignKey("SecurityTipCategory")]
        public Guid SecurityTipCategoryId { get; set; }
        public virtual SecurityTipCategory SecurityTipCategory { get; set; }

        [Required]
        [ForeignKey("AlertLevel")]
        public Guid AlertLevelId { get; set; }
        public virtual AlertLevel AlertLevel { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<EscalatedTip> EscalatedTips { get; set; }
    }
}
