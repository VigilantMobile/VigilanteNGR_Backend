using Application.DTOs.Account;
using Domain.Common;
using Domain.Entities.AppTroopers.SecurityTip;
using Domain.Entities.CompanyEntities;
using Infrastructure.Persistence.Models.LocationEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infrastructure.Persistence.Models.Identity
{
    public class VGNGAStaff : ApplicationUser
    {
        
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public string StaffId { get; set; }

        public decimal Salary { get; set; }    

        public string SalaryCurrency { get; set; }

        //
        public virtual ICollection<State> VGNGAOperatorStates { get; set; }
        public virtual ICollection<State> VGNGAAdminStates { get; set; }
        //
        public virtual ICollection<LGA> VGNGAOperatorLGAs { get; set; }
        public virtual ICollection<LGA> VGNGAAdminLGAs { get; set; }
        //
        public virtual ICollection<Town> VGNGAOperatorTowns { get; set; }
        public virtual ICollection<Town> VGNGAAdminTowns { get; set; }
        //
        public virtual ICollection<Settlement> VGNGAOperatorSettlements { get; set; }
        public virtual ICollection<Settlement> VGNGAAdminSettlements { get; set; }

    }
}
