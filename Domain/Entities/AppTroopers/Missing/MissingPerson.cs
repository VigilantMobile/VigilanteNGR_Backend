﻿using Domain.Common;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.Identity;
using Domain.Entities.LocationEntities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.AppTroopers.Missing
{
    public class MissingPerson : AuditableBaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string FirstName { get; set; }

        [MaxLength(150)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(150)]
        public string LastName { get; set; }
        public string Height { get; set; }
        public string complexion { get; set; }
        public int age { get; set; }
        public string photoUrl { get; set; }
        [Required]
        [MaxLength(150)]
        public string description { get; set; }
        public DateTime DateLastSeen { get; set; }


        [ForeignKey("Town")]
        public Guid? TownId { get; set; }
        public virtual Town Town { get; set; }


        [ForeignKey("Settlement")]
        public Guid SettlementId { get; set; }
        public virtual Settlement Settlement { get; set; }

        //Admin Authorization
        public bool isAdminAuthorized { get; set; } // 


        [ForeignKey("ApplicationUser")]
        public string LoserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("VGNGAStaff")]
        public string AdminAuthorizerID { get; set; }
        public virtual ApplicationUser VGNGAStaff { get; set; }

        [ForeignKey("BroadcastLevel")]
        public Guid BroadcastLevelId { get; set; } //e.g. lga, state; tbd by broadcaster ID
        public virtual BroadcastLevel BroadcastLevel { get; set; }
    }
}
