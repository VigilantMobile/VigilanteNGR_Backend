//using Domain.Common;
//using Domain.Common.Enums;
//using Infrastructure.Persistence.Models.Identity;
//using Infrastructure.Persistence.Models.LocationEntities;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text;

//namespace Domain.Entities.AppTroopers.SecurityTip
//{
//    public class SecurityTip : AuditableBaseEntity
//    {
//        public string Subject { get; set; }
//        public string Body { get; set; }
//        public string Description { get; set; }
//        public string Source { get; set; }

//        //Admin Authorizer
//        public bool isAdminAuthorized { get; set; } // 


//        [ForeignKey("VGNGAStaffAuthorizer")]
//        public string AdminAuthorizerID { get; set; }
//        public virtual VGNGAStaff VGNGAStaffAuthorizer { get; set; }

//        //Coverage Type - based on broadcaster => Nullable for each or Generic
//        [ForeignKey("Settlement")]
//        public int? SettlementId { get; set; }

//        [ForeignKey("Town")]
//        public int? TownId { get; set; }

//        [ForeignKey("LGA")]
//        public int?  LGAId { get; set; }

//        [ForeignKey("State")]
//        public int? StateId { get; set; }
        

//        [ForeignKey("BroadcasterType")]
//        public int BroadcasterTypeId { get; set; }
//        public BroadcasterType BroadcasterType { get; set; } //e.g. verified user, official NPF

//        //broadcaster

//        [ForeignKey("ApplicationUser")]
//        public string BroadcasterUserId { get; set; }
//        public virtual ApplicationUser ApplicationUser { get; set; }

//        [ForeignKey("SecurityTipCategory")]
//        public int SecurityTipCategoryId { get; set; }
//        public SecurityTipCategory SecurityTipCategory { get; set; }

//        //
//        [ForeignKey("AlertLevel")]
//        public int AlertLevelId { get; set; }
//        public AlertLevel AlertLevel { get; set; }


//        [ForeignKey("BroadcastLevel")]
//        public int BroadcastLevelId { get; set; } //e.g. lga, state; tbd by broadcaster ID
//        public BroadcastLevel BroadcastLevel { get; set; }

//    }
//}
