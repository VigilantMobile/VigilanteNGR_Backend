using Domain.Common;
using Domain.Entities.AppTroopers.SecurityTip;
using Domain.Entities.Identity;
using Domain.Entities.LocationEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Identity.Identity
{
    public class OfficialVigilanteLGAOperator : ApplicationUser
    {
        public virtual ICollection<LGA> LGAs { get; set; }
    }
}
