using Domain.Common;
using Domain.Common.Enums;
using Domain.Entities.AppTroopers.Curfew;
using Domain.Entities.AppTroopers.Missing;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.CompanyEntities;
using Domain.Entities.LocationEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.FirstTimePromoUsed = false;
            this.CurrentPromoUsed = false;
            this.isActive = false;
        }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int LocationId { get; set; }

        //CustomerLocationTier

        [ForeignKey("LocationLevel")]
        public int? LocationLevelId { get; set; }
        public virtual BroadcastLevel LocationLevel { get; set; }

        //State
        //[ForeignKey("State")]
        //public int StateId { get; set; }
        //public State CustomerState { get; set; }

        //LGA
        //[ForeignKey("LGA")]
        //public int LGAId { get; set; }
        //public LGA CustomerLGA { get; set; }

        ////Town
        //[ForeignKey("Town")]
        //public int? TownId { get; set; }
        //public Town CustomerTown { get; set; }

        ////Settlement
        //[ForeignKey("Settlement")]
        //public int? SettlementId { get; set; }
        //public Settlement CustomerSettlement { get; set; }

        public string UniqueReferalCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool FirstTimePromoUsed { get; set; }
        public bool isActive { get; set; }
        public bool CurrentPromoUsed { get; set; }

        // VGNGA Staff Fields //

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public string StaffId { get; set; }
        public decimal Salary { get; set; }
        public string SalaryCurrency { get; set; }
        public bool IsAppSuperAdmin { get; set; }
        public bool IsAppAdmin { get; set; }
        public bool IsAppOperator { get; set; }
        public virtual ICollection<Department> SecretaryDepartments { get; set; }
        public virtual ICollection<Department> HODDepartments { get; set; }
        public virtual ICollection<State> InternalStaffStates { get; set; }
        public virtual ICollection<LGA> InternalStaffLGAs { get; set; }
        public virtual ICollection<Town> InternalStaffTowns { get; set; }
        public virtual ICollection<Settlement> InternalStaffSettlements { get; set; }
        public virtual ICollection<Curfew> AdminAuthorizedCurfews { get; set; }
        public virtual ICollection<Curfew> OperatorIniatedCurfews { get; set; }
        public virtual ICollection<TrustedPerson> TrustedPeople { get; set; }

        //External Staff fields
        public ExternalStaffType ExternalStaffType { get; set; }
        public bool IsExternalSuperAdmin { get; set; }
        public bool IsExternalAdmin { get; set; }
        public bool IsExternalOperator { get; set; }
        public virtual ICollection<SecurityTip> ExternalStaffIniatedTips { get; set; }
        public virtual ICollection<SecurityTip> ExternalStaffAuthorizedTips { get; set; }
        public virtual ICollection<EscalatedTip> ApprovedEscalatedTips { get; set; }
        //

        //Comments
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<CommentFlags> CommentFlags { get; set; }

        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
        public virtual ICollection<SecurityTip> CustomerSecurityTips { get; set; }
        public virtual ICollection<MissingPerson> CustomerMissingPeople { get; set; }
        public virtual ICollection<MissingItem> CustomerMissingItems { get; set; }
    }
}

