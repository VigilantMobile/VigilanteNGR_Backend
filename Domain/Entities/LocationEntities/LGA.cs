using Domain.Common;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.Identity;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.LocationEntities
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
        //public virtual ICollection<ApplicationUser> Customers { get; set; }
        // VGNGA Users
        public virtual ICollection<ApplicationUser> VGNGALGAStaff { get; set; }

        // External
        //public virtual ICollection<ApplicationUser> ExternalLGAStaff { get; set; }

        public virtual ICollection<Town> Towns { get; set; }

        public ICollection<SecurityTip> SecurityTips { get; set; }

        public Geometry Boundary { get; set; }
    }
}
