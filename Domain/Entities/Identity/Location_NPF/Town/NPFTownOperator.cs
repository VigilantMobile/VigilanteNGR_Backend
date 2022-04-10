using Domain.Common;
using Domain.Entities.AppTroopers.SecurityTip;
using Domain.Entities.LocationEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Identity.Location
{
    public class NPFTownOperator : ApplicationUser
    {
        public virtual ICollection<Town> Towns { get; set; }
    }
}
