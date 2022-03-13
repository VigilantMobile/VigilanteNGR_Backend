using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Models
{
    public class DemographicEntitiesCoordinatesJSON : AuditableBaseEntity
    {
        public string DemographicType { get; set; }
        public string JsonString { get; set; }
    }
}
