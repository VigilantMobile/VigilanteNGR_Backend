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
    public class SecurityTip : AuditableBaseEntity
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }

        //Admin Authorizer
        public bool isAdminAuthorized { get; set; } // 
        public string AdminAuthorizerID { get; set; }
        [ForeignKey("AdminAuthorizerID")]
        public virtual ApplicationUser AdminAuthorizer { get; set; }

        //VGNGA Authorizer
        public bool isVGNGAAuthorized { get; set; }
        public string VGNGAAuthorizerID { get; set; }

        [ForeignKey("VGNGAAuthorizerID")]
        public virtual ApplicationUser VGNGAAuthorizer { get; set; }

        //Coverage Type - based on broadcaster
        [ForeignKey("Settlement")]
        public int? SettlementId { get; set; }

        [ForeignKey("Town")]
        public int? TownId { get; set; }

        [ForeignKey("LGA")]
        public int?  LGAId { get; set; }

        [ForeignKey("State")]
        public int? StateId { get; set; }

        //Coverage collections
        public virtual ICollection<State> StatesBroadcastedTo { get; set; }
        public virtual ICollection<LGA> LGAsBroadcastedTo { get; set; }
        public virtual ICollection<Town> TownsBroadcastedTo { get; set; }
        public virtual ICollection<Settlement> SettlementsBroadcastedTo { get; set; }
        //

        [ForeignKey("BroadcasterType")]
        public int BroadcasterTypeId { get; set; }  //e.g. verified user, official NPF

        //broadcaster
        public string BroadcasterUserId { get; set; }
        [ForeignKey("BroadcasterUserId")]
        public virtual ApplicationUser Broadcaster { get; set; }

        [ForeignKey("SecurityTipCategory")]
        public int SecurityTipCategoryId { get; set; }

        //
        [ForeignKey("AlertLevel")]
        public int AlertLevelId { get; set; }


        [ForeignKey("BroadcastLevel")]
        public int BroadcastLevelId { get; set; } //e.g. lga, state; tbd by broadcaster ID


    }
}
