using Domain.Common;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Models.LocationEntities
{
    public class TownBoundaryPoints : AuditableBaseEntity
    {
        [ForeignKey("Town")]
        public string TownId { get; set; }
        public virtual Town Town { get; set; }
        public Geometry BoundaryCoordinates { get; set; }
    }
}
