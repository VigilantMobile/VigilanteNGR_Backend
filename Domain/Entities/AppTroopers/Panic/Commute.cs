using Domain.Common;
using Domain.Common.Enums;
using Domain.Entities.LocationEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.AppTroopers.Panic
{
    public class Commute : AuditableBaseEntity
    {
        public string DepartureCoordinates { get; set; }

        //Departure
        [Required]
        [MaxLength(150)]

        //Town
        //Departure
        [ForeignKey("DepartureTown")]
        public int DepartureTownId { get; set; }
        public virtual Town DepartureTown { get; set; }

        [Required]
        public string DepartureTownAddress { get; set; }

        //Destination
        [ForeignKey("DestinationTown")]
        public int DestinationTownId { get; set; }
        public virtual Town DestinationTown { get; set; }
        public string DestinationCoordinates { get; set; }
        [Required]
        public string DestinationTownAddress { get; set; }


        //Settlement
        //Departure
        [ForeignKey("DepartureSettlement")]
        public int DepartureSettlementId { get; set; }
        public virtual Settlement DepartureSettlement { get; set; }

        [Required]
        public string DepartureSettlementAddress { get; set; }

        //Destination
        [ForeignKey("DestinationSettlement")]
        public int DestinationSettlementId { get; set; }
        public virtual Settlement DestinationSettlement { get; set; }
        public string DestinationSettlementAddress { get; set; }


        //Visiting
        public string VisiteeName { get; set; }
        public string VisiteePhone { get; set; }
        public string PurposeOfVisit { get; set; }
        public string AdditionalTripInformation { get; set; }

        [Required]
        public int PanicIntervalInMinutes { get; set; }
        public CommuteStatus CommuteStatus { get; set; }
        public PanicStatus PanicStatus { get; set; }
        public bool PanicInitiated { get; set; }
    }
}
