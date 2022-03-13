using Domain.Common;
using Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AppTroopers.SecurityTip
{
    public class AlertLevel : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AlertLevelEnum alertLevel { get; set; }
    }
}
