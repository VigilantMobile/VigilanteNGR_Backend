using Domain.Common;
using Domain.Entities.AppTroopers.SecurityTip;
using Domain.Entities.Identity;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.LocationEntities
{
    public class State : AuditableBaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string NPFStateAuthorityPhone { get; set; }
        public string NPFStateAuthorityAddress { get; set; }

        // Customer
        public virtual ICollection<ApplicationUser> Customers { get; set; }
        // VGNGA Users
        public virtual ICollection<ApplicationUser> VGNGAStateStaff { get; set; }

        // External
        //public virtual ICollection<ApplicationUser> ExternalStateStaff { get; set; }

        //public virtual ICollection<ApplicationUser> NPFStateAdmins { get; set; }

        // Official Vigilante Users
        //public virtual ICollection<ApplicationUser> OfficialVigilanteStateAdmins { get; set; }

        //
        public virtual ICollection<LGA> LGAs { get; set; }


        public ICollection<SecurityTip> SecurityTips { get; set; }
        public decimal shapeLength { get; set; }
        public decimal shapeArea { get; set; }
        public Geometry Boundary { get; set; }
    }
}
