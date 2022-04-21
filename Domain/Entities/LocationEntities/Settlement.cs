using Domain.Common;
using Domain.Entities.AppTroopers.Missing;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTip;
using Domain.Entities.AppTroopers.Wanted;
using Domain.Entities.Identity;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.LocationEntities
{
    public class Settlement : AuditableBaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Town")]
        public int TownId { get; set; }
        public virtual Town Town { get; set; }

        public string NPFSettlementAuthorityAddress { get; set; }
        public string AuthorityPhone { get; set; }

        //Customers
        public virtual ICollection<ApplicationUser> Customers { get; set; }
        // VGNGA Users
        public virtual ICollection<ApplicationUser> VGNGASettlementStaff { get; set; }

        // External
        //public virtual ICollection<ApplicationUser> ExternalSettlementStaff { get; set; }

        //SecurityTip
        public ICollection<SecurityTip> SecurityTips { get; set; }

        //Commute
        public virtual ICollection<Commute> DepartureSettlementCommutes { get; set; }
        public virtual ICollection<Commute> DestinationSettlementCommutes { get; set; }

        //missing/wanted
        public virtual ICollection<WantedPerson> SettlementWantedPeople { get; set; }
        public virtual ICollection<MissingPerson> SettlementMissingPeople { get; set; }
        public virtual ICollection<MissingItem> SettlementMissingItems { get; set; }

        //Geography
        public Geometry Boundary { get; set; }
    }
}
