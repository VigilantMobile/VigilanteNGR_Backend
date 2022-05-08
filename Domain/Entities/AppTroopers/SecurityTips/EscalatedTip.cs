using Domain.Common;
using Domain.Common.Enums;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AppTroopers.SecurityTips
{
    public class EscalatedTip : AuditableBaseEntity
    {
        [ForeignKey("SecurityTip")]
        public int SecurityTipId { get; set; }
        public virtual SecurityTip SecurityTip { get; set; }

        public int EscalationLocationId { get; set; } //No FK

        [Required]
        [ForeignKey("EscalationBroadcastLevel")]
        public int EscalationBroadcastLevelId { get; set; } //e.g. lga, state; tbd by broadcaster ID
        public virtual BroadcastLevel EscalationBroadcastLevel { get; set; }

        [ForeignKey("EscalationAuthorizer")]
        public string EscalationAuthorizerID { get; set; }
        public virtual ApplicationUser EscalationAuthorizer { get; set; }
        public bool isEscalationApproved { get; set; }
    }
}
