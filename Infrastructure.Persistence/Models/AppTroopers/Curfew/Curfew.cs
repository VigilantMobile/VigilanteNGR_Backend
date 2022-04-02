using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Models.AppTroopers.Curfew
{
    public class Curfew : AuditableBaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DailyStartHour { get; set; }
        public int DailyEndHour { get; set; }
    }
}
