using Domain.Common;
using Domain.Entities.AppTroopers.SecurityTip;
using Infrastructure.Persistence.Models.Identity;
using Infrastructure.Persistence.Models.Identity.Location;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infrastructure.Persistence.Models.LocationEntities
{
    public class State : AuditableBaseEntity
    {
        public string Name { get; set; }

        public string NPFStatePhone { get; set; }
        public string NPFStateAuthorityAddress { get; set; }

        // NPF Users
        public virtual ICollection<NPFStateOperator> NPFStateOperators { get; set; }
        public virtual ICollection<NPFStateAdmin> NPFStateAdmins { get; set; }

        // VGNGA Users

        // Official Vigilante Users

        public ICollection<SecurityTip> SecurityTips { get; set; }
        public decimal shapeLength { get; set; }
        public decimal shapeArea { get; set; }
        public Geometry Boundary { get; set; }
    }
}
