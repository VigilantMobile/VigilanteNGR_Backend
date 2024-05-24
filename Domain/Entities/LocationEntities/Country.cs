using Domain.Common;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.Identity;
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.LocationEntities
{
    public class Country : AuditableBaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string GoogleMapsShortName { get; set; }
        public string GoogleMapsLongName { get; set; }
        public string GoogleMapsLocationType { get; set; }
        public string GoogleMapsGeometryInfo { get; set; }
        public string GoogleMapsPlaceId { get; set; }
        public string GoogleMapsFormattedAddress { get; set; }
        

    }
}
