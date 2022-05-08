using Domain.Common;

namespace Infrastructure.Persistence.Models
{
    public class DemographicEntitiesCoordinatesJSON : AuditableBaseEntity
    {
        public string DemographicType { get; set; }
        public string JsonString { get; set; }
    }
}
