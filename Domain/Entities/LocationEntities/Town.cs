﻿using Domain.Common;
using Domain.Entities.AppTroopers.Missing;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.AppTroopers.Wanted;
using Domain.Entities.Identity;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.LocationEntities
{
    public class Town : AuditableBaseEntity
    {
        [Required]
        public string Name { get; set; }

        [ForeignKey("LGA")]
        public Guid LGAId { get; set; }
        public virtual LGA LGA { get; set; }
        public string NPFTownAuthorityAddress { get; set; }
        public string NPFPhone { get; set; }
        public string GoogleMapsPlaceId { get; set; }
        public string GoogleMapsShortName { get; set; }
        public string GoogleMapsLongName { get; set; }
        public string GoogleMapsLocationType { get; set; }
        public string GoogleMapsGeometryInfo { get; set; }
        public string GoogleMapsFormattedAddress { get; set; }


        //Customers
        public virtual ICollection<ApplicationUser> TownResidents { get; set; }

        // VGNGA Users
        public virtual ICollection<ApplicationUser> VGNGATownStaff { get; set; }

        // External
        //public virtual ICollection<ApplicationUser> ExternalTownStaff { get; set; }

        public virtual ICollection<Settlement> Settlements { get; set; }

        //Commute
        public virtual ICollection<Commute> DepartureTownCommutes { get; set; }
        public virtual ICollection<Commute> DestinationTownCommutes { get; set; }

        //missing/wanted
        public virtual ICollection<WantedPerson> TownWantedPeople { get; set; }
        public virtual ICollection<MissingPerson> TownMissingPeople { get; set; }
        public virtual ICollection<MissingItem> TownMissingItems { get; set; }

        //SecurityTip
        public ICollection<SecurityTip> SecurityTips { get; set; }

        public Geometry Boundary { get; set; }
    }
}
