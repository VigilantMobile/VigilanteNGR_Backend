using Domain.Common;
using Domain.Entities.AppTroopers.Comments;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AppTroopers.Subscription
{
    public class Subscription : AuditableBaseEntity
    {
        public string SubscriptionName { get; set; }
        public string SubscriptionDescription { get; set; }
        public decimal MonthlyFee { get; set; }
        public ICollection<ApplicationUser> SubscribedUsers { get; set; }
    }
}
