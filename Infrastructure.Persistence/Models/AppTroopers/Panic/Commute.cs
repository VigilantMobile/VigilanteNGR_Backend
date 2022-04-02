using Domain.Common;
using Domain.Common.Enums;
using Infrastructure.Persistence.Models.Identity;
using Infrastructure.Persistence.Models.LocationEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.AppTroopers.Panic
{
    public class Commute : AuditableBaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string DepartureTownAddress { get; set; }
        public Town DepartureTown { get; set; }
        public string DepartureCoordinates { get; set; }
        public string DestinationTownAddress { get; set; }

        public Town DestinationTown { get; set; }
        public string DestinationDestinationCoordinates { get; set; }

        //Visiting
        public string VisiteeName { get; set; }
        public string VisiteePhone { get; set; }
        public string PurposeOfVisit { get; set; }
        public string AdditionalTripInformation { get; set; }

        [Required]
        public int PanicIntervalInMinutes { get; set; }
        public string CommuteStatus { get; set; }
        public string PanicStatus { get; set; }
        public bool PanicInitiated { get; set; }
    }
}
