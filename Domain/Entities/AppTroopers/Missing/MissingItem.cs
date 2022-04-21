using Domain.Common;
using Domain.Common.Enums;
using Domain.Entities.AppTroopers.SecurityTip;
using Domain.Entities.Identity;
using Domain.Entities.LocationEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.AppTroopers.Missing
{
    public class MissingItem : AuditableBaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string ItemName { get; set; }

        [Required]
        [MaxLength(150)]
        public string ItemType { get; set; }

        [Required]
        [MaxLength(150)]
        public string Description { get; set; }
        public string IdNumber { get; set; }
        public DateTime DateLastSeen { get; set; }

        [ForeignKey("Town")]
        public int? TownId { get; set; }
        public virtual Town Town { get; set; }

        [ForeignKey("Settlement")]
        public int? SettlementId { get; set; }
        public virtual Settlement Settlement { get; set; }

        //Admin Authorization
        public bool isAdminAuthorized { get; set; }


        [ForeignKey("LoserId")]
        public string LoserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


        [ForeignKey("AdminAuthorizerID")]
        public string AdminAuthorizerID { get; set; }
        public virtual ApplicationUser VGNGAStaff { get; set; }
        


        [ForeignKey("BroadcastLevel")]
        public int BroadcastLevelId { get; set; }
        public virtual BroadcastLevel BroadcastLevel { get; set; }

    }
}
