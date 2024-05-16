//using Application.DTOs.Account;
//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;

//namespace Infrastructure.Identity.Models
//{
//    public class ApplicationUser : IdentityUser
//    {
//        public ApplicationUser()
//        {
//            this.FirstTimePromoUsed = false;
//            this.CurrentPromoUsed = false;
//            this.ActiveStatus = false;
//            RefreshTokens = new List<RefreshToken>();
//        }

//        public string FirstName { get; set; }
//        public string MiddleName { get; set; }
//        public string LastName { get; set; }
//        public string AddressLine1 { get; set; }
//        public string AddressLine2 { get; set; }
//        public string LocationID { get; set; }
//        public string TempLocationID { get; set; }
//        public string LastCurrentLocationID { get; set; }
//        public string UniqueReferalCode { get; set; }
//        public DateTime DateOfBirth { get; set; }
//        public bool FirstTimePromoUsed { get; set; }
//        public bool ActiveStatus { get; set; }
//        public bool CurrentPromoUsed { get; set; }
//        public string CreatedBy { get; set; }
//        public DateTime Created { get; set; }
//        public string LastModifiedBy { get; set; }
//        public DateTime LastModified { get; set; }

//        public List<RefreshToken> RefreshTokens { get; set; }
//        public bool OwnsToken(string token)
//        {
//            return this.RefreshTokens?.Find(x => x.Token == token) != null;
//        }
//    }
//}
