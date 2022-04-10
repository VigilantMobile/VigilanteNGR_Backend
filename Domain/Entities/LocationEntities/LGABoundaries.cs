using Domain.Common;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
