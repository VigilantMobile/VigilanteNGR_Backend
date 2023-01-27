using Domain.Common;
using Domain.Entities.AppTroopers.Comments;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AppTroopers.Subscription
{
    public class Wallet : AuditableBaseEntity
    {
        public decimal WalletBalance { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser Customer { get; set; }
    }
}
