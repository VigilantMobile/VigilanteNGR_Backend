﻿using Domain.Common;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.Identity;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.LocationEntities
{
    public class State : AuditableBaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string NPFStateAuthorityPhone { get; set; }
        public string NPFStateAuthorityAddress { get; set; }
        public Guid CountryId { get; set; }

        // Customer
        //public virtual ICollection<ApplicationUser> Customers { get; set; }
        // VGNGA Users
        public virtual ICollection<ApplicationUser> VGNGAStateStaff { get; set; }

        // External
        //public virtual ICollection<ApplicationUser> ExternalStateStaff { get; set; }

        //public virtual ICollection<ApplicationUser> NPFStateAdmins { get; set; }

        // Official Vigilante Users
        //public virtual ICollection<ApplicationUser> OfficialVigilanteStateAdmins { get; set; }

        //
        public string GoogleMapsPlaceId { get; set; }
        public string GoogleMapsShortName { get; set; }
        public string GoogleMapsLongName { get; set; }
        public string GoogleMapsLocationType { get; set; }
        public string GoogleMapsGeometryInfo { get; set; }
        public string GoogleMapsFormattedAddress { get; set; }
        public virtual ICollection<LGA> LGAs { get; set; }


        public ICollection<SecurityTip> SecurityTips { get; set; }
        public decimal shapeLength { get; set; }
        public decimal shapeArea { get; set; }
        public Geometry Boundary { get; set; }
    }
}
