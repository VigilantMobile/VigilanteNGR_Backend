using Domain.Common;
using Infrastructure.Persistence.Models.Identity;
using Infrastructure.Persistence.Models.LocationEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Models.AppTroopers.Curfew
{
    public class TownCurfew : Curfew
    {
        [ForeignKey("Town")]
        public int TownId { get; set; }
        public Town Town { get; set; }

        [ForeignKey("VGNGAStaff")]
        public string AdminAuthorizerID { get; set; }
        public virtual VGNGAStaff VGNGAStaff { get; set; }
    }
}
