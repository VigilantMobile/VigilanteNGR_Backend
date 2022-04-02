using Domain.Common;
using Domain.Common.Enums;
using Infrastructure.Persistence.Models.Identity;
using Infrastructure.Persistence.Models.LocationEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.AppTroopers.SecurityTip
{
    public class SecurityTip2 : AuditableBaseEntity
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }


        //broadcaster

        public string BroadcasterUserId { get; set; } //No FK

        //Admin Authorization
        public bool isAdminAuthorized { get; set; } // 

        [ForeignKey("VGNGAStaffAuthorizer")]
        public string AdminAuthorizerID { get; set; }
        public virtual VGNGAStaff VGNGAAdminAuthorizer { get; set; }

        //Coverage Type - based on broadcaster => Nullable for each or Generic
        public string LocationId { get; set; } //No FK

        [ForeignKey("BroadcastLevel")]
        public int BroadcastLevelId { get; set; } //e.g. lga, state; tbd by broadcaster ID
        public BroadcastLevel BroadcastLevel { get; set; }

        [ForeignKey("BroadcasterType")]
        public int BroadcasterTypeId { get; set; }
        public BroadcasterType BroadcasterType { get; set; }


        [ForeignKey("SecurityTipCategory")]
        public int SecurityTipCategoryId { get; set; }
        public SecurityTipCategory SecurityTipCategory { get; set; }

        //
        [ForeignKey("AlertLevel")]
        public int AlertLevelId { get; set; }
        public AlertLevel AlertLevel { get; set; }


    }
}
