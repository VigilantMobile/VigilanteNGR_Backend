using Domain.Common;
using NetTopologySuite.Geometries;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.LocationEntities
{
    public class LGABoundaryPoints : AuditableBaseEntity
    {
        [ForeignKey("LGA")]
        public Guid LGAId { get; set; }
        public virtual LGA LGA { get; set; }
        public Geometry BoundaryCoordinates { get; set; }
    }
}
