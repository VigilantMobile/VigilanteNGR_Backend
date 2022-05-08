using Domain.Common;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.LocationEntities
{
    public class LGABoundaryPoints : AuditableBaseEntity
    {
        [ForeignKey("LGA")]
        public string LGAId { get; set; }
        public virtual LGA LGA { get; set; }
        public Geometry BoundaryCoordinates { get; set; }
    }
}
