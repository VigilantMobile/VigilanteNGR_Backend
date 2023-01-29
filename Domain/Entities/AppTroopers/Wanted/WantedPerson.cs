using Domain.Common;
using Domain.Common.Enums;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.LocationEntities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.AppTroopers.Wanted
{
    public class WantedPerson : AuditableBaseEntity
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
        public int photoUrl { get; set; }
        [Required]
        [MaxLength(150)]
        public string description { get; set; }
        public DateTime DateLastSeen { get; set; }

        [ForeignKey("Town")]
        public Guid TownId { get; set; }
        public virtual Town Town { get; set; }

        [ForeignKey("Settlement")]
        public Guid SettlementId { get; set; }
        public virtual Settlement Settlement { get; set; }


        //NPF Authorization
        public bool isAuthorized { get; set; } // 
        public string NPFAuthorizerID { get; set; }
        public NPFAuthorityType NPFAuthorityType { get; set; }

        [Required]
        [ForeignKey("BroadcastLevel")]
        public Guid BroadcastLevelId { get; set; } //e.g. lga, state; tbd by broadcaster ID
        public virtual BroadcastLevel BroadcastLevel { get; set; }

    }
}
