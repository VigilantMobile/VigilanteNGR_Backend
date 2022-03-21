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
    public class LGA : AuditableBaseEntity
    {
        public string Name { get; set; }

        [ForeignKey("State")]
        public int StateId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string LGASuperAdminId { get; set; }
        public string NPFLGAAuthorityAddress { get; set; }
        public string NPFPhone { get; set; }
        public bool isCapital { get; set; }

        // NPF Users
        public virtual ICollection<NPFLGAOperator> NPFLGAOperators { get; set; }
        public virtual ICollection<NPFLGAAdmin> NPFLGAAdmins { get; set; }

        // VGNGA Users

        // Official Vigilante Users

        public ICollection<SecurityTip> SecurityTips { get; set; }

        public Geometry Boundary { get; set; }
    }
}
