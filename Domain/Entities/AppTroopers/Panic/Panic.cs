using Domain.Common;
using Domain.Common.Enums;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.AppTroopers.Panic
{
    public class Panic : AuditableBaseEntity
    {

        [Required]
        [ForeignKey("ApplicationUser")]
        public string PanicInitiator { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public PanicStatus PanicStatus { get; set; }
        public PanicType PanicType { get; set; }

        [Required]
        [ForeignKey("Commute")]
        public int CommuteId { get; set; }
        public virtual Commute Commute { get; set; }
    }
}
