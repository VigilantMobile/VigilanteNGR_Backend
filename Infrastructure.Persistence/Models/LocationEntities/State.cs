using Domain.Common;
using Domain.Entities.AppTroopers.SecurityTip;
using Infrastructure.Persistence.Models.Identity;
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

        [ForeignKey("ApplicationUser")]
        public string StateSuperAdminId { get; set; }
        public virtual ICollection<ApplicationUser> NPFOperators { get; set; }
        public virtual ICollection<ApplicationUser> NPFAdmins { get; set; }
        public virtual ICollection<ApplicationUser> VigilanteOperators { get; set; }
        public virtual ICollection<ApplicationUser> VigilanteAdmins { get; set; }
        public int? SecurityTipId { get; set; }

        public virtual SecurityTip SecurityTip { get; set; }
        public decimal shapeLength { get; set; }
        public decimal shapeArea { get; set; }
        public Geometry Boundary { get; set; }
    }
}
