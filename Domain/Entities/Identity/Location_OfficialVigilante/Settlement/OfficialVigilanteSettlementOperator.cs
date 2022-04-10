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
    public class OfficialVigilanteSettlementOperator : ApplicationUser
    {
        public virtual ICollection<Settlement> Settlements { get; set; }
    }
}
