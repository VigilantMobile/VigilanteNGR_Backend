using Domain.Common;
using Domain.Entities.AppTroopers.Curfew;
using Domain.Entities.AppTroopers.SecurityTip;
using Domain.Entities.CompanyEntities;
using Domain.Entities.Identity;
using Domain.Entities.LocationEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Identity
{
    public class VGNGAStaff : ApplicationUser
    {

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

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

        public virtual ICollection<Department> SecretaryDepartments { get; set; }
        public virtual ICollection<Department> HODDepartments { get; set; }
        public virtual ICollection<SecurityTip> VGNGAAdminApprovedSecurityTips { get; set; }

        //Curfew
        public virtual ICollection<StateCurfew> StateCurfew { get; set; }
        public virtual ICollection<LGACurfew> LGACurfew { get; set; }
        public virtual ICollection<TownCurfew> TownCurfew { get; set; }
        public virtual ICollection<SettlementCurfew> SettlementCurfew { get; set; }

    }
}