using Domain.Common;
using Domain.Entities.AppTroopers.SecurityTip;
using Infrastructure.Persistence.Models.Identity;
using Infrastructure.Persistence.Models.Identity.Location;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infrastructure.Persistence.Models.LocationEntities
{
    public class LGA : AuditableBaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey("State")]
        public int StateId { get; set; }
        public virtual State State { get; set; }


        public string NPFLGAAuthorityAddress { get; set; }
        public string NPFPhone { get; set; }
        public bool isCapital { get; set; }

        //Customers
        public virtual ICollection<ApplicationUser> Customers { get; set; }

        // VGNGA Users
        public virtual ICollection<VGNGAStaff> VGNGALGAAdmins { get; set; }
        public virtual ICollection<VGNGAStaff> VGNGALGAOperators { get; set; }

        // NPF Users
        public virtual ICollection<NPFLGAAdmin> NPFLGAAdmins { get; set; }
        public virtual ICollection<NPFLGAOperator> NPFLGAOperators { get; set; }

        // Official Vigilante Users
        public virtual ICollection<OfficialVigilanteLGAAdmin> OfficialVigilanteLGAAdmins { get; set; }
        public virtual ICollection<OfficialVigilanteLGAOperator> OfficialVigilanteLGAOperators{ get; set; }

        public virtual ICollection<Town> Towns { get; set; }

        public ICollection<SecurityTip> SecurityTips { get; set; }

        public Geometry Boundary { get; set; }
    }
}
