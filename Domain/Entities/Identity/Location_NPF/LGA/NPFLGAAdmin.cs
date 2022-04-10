using Domain.Common;
using Domain.Entities.AppTroopers.SecurityTip;
using Domain.Entities.LocationEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Identity.Location
{
    public class NPFLGAAdmin : ApplicationUser
    {
        public bool IsSuperAdmin { get; set; }

        public virtual ICollection<LGA> LGAs { get; set; }
    }
}
