using Domain.Common;
using Domain.Common.Enums;
using Infrastructure.Persistence.Models.Identity;
using Infrastructure.Persistence.Models.LocationEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
        public int TownId { get; set; }

        //NPF Authorization
        public bool isNPFAuthorized { get; set; } // 
        public string NPFAuthorizerID { get; set; }
        [ForeignKey("NPFAuthorizerID")]
        public virtual ApplicationUser AdminAuthorizer { get; set; }

        [ForeignKey("BroadcastLevel")]
        public int BroadcastLevelId { get; set; } //e.g. lga, state; tbd by broadcaster ID


    }
}
