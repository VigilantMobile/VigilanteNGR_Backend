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
    public class Panic : AuditableBaseEntity
    {
        public string IniatiatorId { get; set; }
        public virtual ApplicationUser Initiator { get; set; }
        public string PanicStatus { get; set; }
        public int CommuteId { get; set; }
        public  Commute CommuteDetails { get; set; }       
    }
}
