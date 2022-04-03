using Application.DTOs.Account;
using Domain.Common;
using Domain.Entities.AppTroopers.SecurityTip;
using Infrastructure.Persistence.Models.LocationEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Models.Identity.Location
{
    public class NPFStateAdmin : ApplicationUser
    {
        public bool IsSuperAdmin { get; set; }

        public virtual ICollection<State> States { get; set; }
    }
}
