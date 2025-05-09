﻿using Domain.Common;
using NetTopologySuite.Geometries;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.LocationEntities
{
    public class TownBoundaryPoints : AuditableBaseEntity
    {
        [ForeignKey("Town")]
        public Guid TownId { get; set; }
        public virtual Town Town { get; set; }
        public Geometry BoundaryCoordinates { get; set; }
    }
}
