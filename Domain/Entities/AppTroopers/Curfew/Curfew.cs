using Domain.Common;
using Domain.Entities.Identity;
using Domain.Entities.LocationEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int? StateId { get; set; }
        public virtual State State { get; set; }

        //LGA
        [ForeignKey("LGA")]
        public int? LGAId { get; set; }
        public virtual LGA LGA { get; set; }

        //Town
        [ForeignKey("Town")]
        public int? TownId { get; set; }
        public virtual Town Town { get; set; }

        //Settlement
        [ForeignKey("Settlement")]
        public int? SettlementId { get; set; }

        public virtual Settlement Settlement { get; set; }
    }
}
