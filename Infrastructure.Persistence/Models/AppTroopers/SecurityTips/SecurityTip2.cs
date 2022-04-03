using Domain.Common;
using Domain.Common.Enums;
using Infrastructure.Persistence.Models.Identity;
using Infrastructure.Persistence.Models.LocationEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.AppTroopers.SecurityTip
{
    public class SecurityTip : AuditableBaseEntity
    {
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        public string Description { get; set; }
        [Required]
        public string Source { get; set; }


        //broadcaster
        [Required]
        [ForeignKey("ApplicationUser")]
        public string BroadcasterId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


        //Admin Authorization
        public bool isAdminAuthorized { get; set; } // 

        [ForeignKey("VGNGAStaffAuthorizer")]
        public string AdminAuthorizerID { get; set; }
        public virtual VGNGAStaff VGNGAAdminAuthorizer { get; set; }

        //Coverage Type - based on broadcaster => Nullable for each or Generic
        [Required]
        public int LocationId { get; set; } //No FK

        [Required]
        [ForeignKey("BroadcastLevel")]
        public int BroadcastLevelId { get; set; } //e.g. lga, state; tbd by broadcaster ID
        public virtual BroadcastLevel BroadcastLevel { get; set; }

        [Required]
        [ForeignKey("BroadcasterType")]
        public int BroadcasterTypeId { get; set; }
        public virtual BroadcasterType BroadcasterType { get; set; }

        [Required]
        [ForeignKey("SecurityTipCategory")]
        public int SecurityTipCategoryId { get; set; }
        public virtual SecurityTipCategory SecurityTipCategory { get; set; }

        //
        [Required]
        [ForeignKey("AlertLevel")]
        public int AlertLevelId { get; set; }
        public virtual AlertLevel AlertLevel { get; set; }
    }
}
