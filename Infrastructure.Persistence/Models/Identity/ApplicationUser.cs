using Application.DTOs.Account;
using Domain.Common;
using Domain.Entities.AppTroopers.SecurityTip;
using Infrastructure.Persistence.Models.LocationEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.FirstTimePromoUsed = false;
            this.CurrentPromoUsed = false;
            this.ActiveStatus = false;
            RefreshTokens = new List<RefreshToken>();
        }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public bool isVGNGAStaff { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string LocationID { get; set; }
        public string TempLocationID { get; set; }
        public string LastCurrentLocationID { get; set; }
        public string UniqueReferalCode { get; set; }
      
        public DateTime DateOfBirth { get; set; }
        public bool FirstTimePromoUsed { get; set; }
        public bool ActiveStatus { get; set; }
        public bool CurrentPromoUsed { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }

        //Location
        //State
        public int? NPFOperatorStateId { get; set; }
        public virtual State NPFOperatorState { get; set; }
        public int? NPFAdminStateId { get; set; }
        public virtual State NPFAdminState { get; set; }

        // state vigilante
        public int? VigilanteAdminStateId { get; set; }
        public virtual State VigilanteAdminState { get; set; }
        public int? VigilanteOperatorStateId { get; set; }
        public virtual State VigilanteOperatorState { get; set; }

        //LGA
        public int? NPFAdminLGAId { get; set; }
        public virtual LGA NPFAdminLGA { get; set; }
        public int? NPFOperatorLGAId { get; set; }
        public virtual LGA NPFOperatorLGA { get; set; }


        // lga vigilante
        public int? VigilanteAdminLGAId { get; set; }
        public virtual LGA VigilanteAdminLGA { get; set; }
        public int? VigilanteOperatorLGAId { get; set; }
        public virtual LGA VigilanteOperatorLGA { get; set; }

        //Town
        public int? NPFAdminTownId { get; set; }
        public virtual Town NPFAdminTown { get; set; }
        public int? NPFOperatorTownId { get; set; }
        public virtual Town NPFOperatorTown { get; set; }


        //town vigilante
        public int? VigilanteAdminTownId { get; set; }
        public virtual Town VigilanteAdminTown { get; set; }
        public int? VigilanteOperatorTownId { get; set; }
        public virtual Town VigilanteOperatorTown { get; set; }


        //Settlement
        public int? NPFOperatorSettlementId { get; set; }
        public Settlement NPFOperatorSettlement { get; set; }
        public int? NPFAdminSettlementId { get; set; }
        public Settlement NPFAdminSettlement { get; set; }

        //town vigilante
        public int? VigilanteAdminSettlementId { get; set; }
        public virtual Settlement VigilanteAdminSettlement { get; set; }
        public int? VigilanteOperatorSettlementId { get; set; }
        public virtual Settlement VigilanteOperatorSettlement { get; set; }


        //Relationships

        //Security Tips
        public virtual ICollection<SecurityTip> AdminAuthorizedTips { get; set; }
        public virtual ICollection<SecurityTip> VGNGAAuthorizedTips { get; set; }
        public virtual ICollection<SecurityTip> BroadcasterTips { get; set; }

        // </Location>
        public bool OwnsToken(string token)
        {
            return this.RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }
}
