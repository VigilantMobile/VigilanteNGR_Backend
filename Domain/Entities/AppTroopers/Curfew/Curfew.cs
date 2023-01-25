using Domain.Common;
using Domain.Entities.Identity;
using Domain.Entities.LocationEntities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.AppTroopers.Curfew
{
    public class Curfew : AuditableBaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Required]
        public int DailyStartHour { get; set; }
        [Required]
        public int DailyEndHour { get; set; }

        [ForeignKey("AdminAuthorizer")]
        public string AdminAuthorizerId { get; set; }
        public virtual ApplicationUser AdminAuthorizer { get; set; }

        [ForeignKey("OperatorIniator")]
        public string OperatorIniatorId { get; set; }
        public virtual ApplicationUser OperatorIniator { get; set; }

        public bool IsAuthorized { get; set; }
        //State
        [ForeignKey("State")]
        public Guid? StateId { get; set; }
        public virtual State State { get; set; }

        //LGA
        [ForeignKey("LGA")]
        public Guid? LGAId { get; set; }
        public virtual LGA LGA { get; set; }

        //Town
        [ForeignKey("Town")]
        public Guid? TownId { get; set; }
        public virtual Town Town { get; set; }

        //Settlement
        [ForeignKey("Settlement")]
        public Guid? SettlementId { get; set; }

        public virtual Settlement Settlement { get; set; }
    }
}
