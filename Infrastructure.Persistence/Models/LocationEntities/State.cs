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
        public string NPFStateAuthorityPhone { get; set; }
        public string NPFStateAuthorityAddress { get; set; }

        // VGNGA Users
        public virtual ICollection<ApplicationUser> VigilanteNGOperators { get; set; }
        public virtual ICollection<ApplicationUser> VigilanteNGAdmins { get; set; }

        // NPF Users
        public virtual ICollection<NPFStateOperator> NPFStateOperators { get; set; }
        public virtual ICollection<NPFStateAdmin> NPFStateAdmins { get; set; }

        // Official Vigilante Users
        public virtual ICollection<OfficialVigilanteStateOperator> OfficialVigilanteStateOperators { get; set; }
        public virtual ICollection<OfficialVigilanteStateAdmin> OfficialVigilanteStateAdmins { get; set; }

        //

        public ICollection<SecurityTip> SecurityTips { get; set; }
        public decimal shapeLength { get; set; }
        public decimal shapeArea { get; set; }
        public Geometry Boundary { get; set; }
    }
}
