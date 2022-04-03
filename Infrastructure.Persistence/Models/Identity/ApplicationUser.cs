using Application.DTOs.Account;
using Domain.Common;
using Domain.Entities.AppTroopers.Missing;
using Domain.Entities.AppTroopers.SecurityTip;
using Infrastructure.Persistence.Models.LocationEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infrastructure.Persistence.Models.Identity
{
    //public class ApplicationUser : IdentityUser
    //{
    //    public ApplicationUser()
    //    {
    //        this.FirstTimePromoUsed = false;
    //        this.CurrentPromoUsed = false;
    //        this.ActiveStatus = false;
    //    }

    //    public string FirstName { get; set; }
    //    public string MiddleName { get; set; }
    //    public string LastName { get; set; }
    //    public string AddressLine1 { get; set; }
    //    public string AddressLine2 { get; set; }

    //    //State
    //    [ForeignKey("State")]
    //    public int StateId { get; set; }
    //    public State State { get; set; }

    //    //LGA
    //    [ForeignKey("LGA")]
    //    public int LGAId { get; set; }
    //    public LGA LGA { get; set; }

    //    //Town
    //    [ForeignKey("Town")]
    //    public int TownId { get; set; }
    //    public Town Town { get; set; }

    //    //Settlement
    //    [ForeignKey("Settlement")]
    //    public int SettlementId { get; set; }
    //    public Settlement Settlement { get; set; }
    //    public string LastLocationCoordinates { get; set; }
    //    public string UniqueReferalCode { get; set; }
    //    public DateTime DateOfBirth { get; set; }
    //    public bool FirstTimePromoUsed { get; set; }
    //    public bool ActiveStatus { get; set; }
    //    public bool CurrentPromoUsed { get; set; }
    //    public string CreatedBy { get; set; }
    //    public DateTime Created { get; set; }
    //    public string LastModifiedBy { get; set; }
    //    public DateTime LastModified { get; set; }
    //    public string RefreshToken { get; set; }
    //    public DateTime RefreshTokenExpiry { get; set; }

    //    public virtual ICollection<SecurityTip> SecurityTips { get; set; }
    //    public virtual ICollection<MissingPerson> MissingPeople { get; set; }
    //    public virtual ICollection<MissingItem> MissingItems { get; set; }

    //}


    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.FirstTimePromoUsed = false;
            this.CurrentPromoUsed = false;
            this.ActiveStatus = false;
        }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        //State
        [ForeignKey("State")]
        public int StateId { get; set; }
        public State State { get; set; }

        //LGA
        [ForeignKey("LGA")]
        public int LGAId { get; set; }
        public LGA LGA { get; set; }

        //Town
        [ForeignKey("Town")]
        public int TownId { get; set; }
        public Town Town { get; set; }

        //Settlement
        [ForeignKey("Settlement")]
        public int SettlementId { get; set; }
        public Settlement Settlement { get; set; }
        public string LastLocationCoordinates { get; set; }
        public string UniqueReferalCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool FirstTimePromoUsed { get; set; }
        public bool ActiveStatus { get; set; }
        public bool CurrentPromoUsed { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }

        public virtual ICollection<SecurityTip> SecurityTips { get; set; }
        public virtual ICollection<MissingPerson> MissingPeople { get; set; }
        public virtual ICollection<MissingItem> MissingItems { get; set; }

    }
}
