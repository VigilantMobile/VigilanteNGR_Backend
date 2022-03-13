using Domain.Common;
using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Models
{
    //Get User Claims
    //Request

    public class UsersAndClaimsRequest
    {
        [Required]
        public string UserId { get; set; }
    }

    public class UserAndRolesRequest
    {
        [Required]
        public string UserId { get; set; }
    }

    public class UserAndRolesResponse
    {
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public IList<string> Roles { get; set; }
    }


    //Response
    public class UsersAndClaimsResponse
    {
        public string UserId { get; set; }
        public string UserFullName { get; set; }

        public List<RolePermissionClaimsViewModel> RoleClaimsList { get; set; }
        public List<RolePermissionClaimsViewModel> UserClaimsList { get; set; }
    }

    //ManageUSerClaims
    //Request
    public class ManageUserClaimsRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public List<RolePermissionClaimsViewModel> UserClaimsList { get; set; }
    }


    //Role Claims
    //Response 
    public class RolesAndClaimsResponse
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public List<GetRoleClaimsResponse> RoleClaimsList { get; set; }
    }

    //Role And Claims 
    public class GetRoleClaimsRequest
    {
        [Required]
        public string RoleId { get; set; }

    }

    public class GetAllRoleClaimsResponse
    {
        List<RolesAndClaimsResponse> rolesAndClaimsResponses { get; set; }
    }


    public class GetRoleClaimsResponse
    {
        [Required]
        //[Display(Name = "Claim Type")]
        public string Type { get; set; }

        [Required]
        //[Display(Name = "Claim Value")]
        public string Value { get; set; }

        public string FriendlyName { get; set; }

        public bool Selected { get; set; }

        public bool IsEditable { get; set; }
    }

    public class ManageRoleClaimsRequest
    {
        [Required]
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        [Required]
        public List<RolePermissionClaimsViewModel> RoleClaimsList { get; set; }
    }


    //
    public class RolePermissionClaimsViewModel
    {
        [Required]
        //[Display(Name = "Claim Type")]
        public string Type { get; set; }

        [Required]
        //[Display(Name = "Claim Value")]
        public string Value { get; set; }

        public string FriendlyName { get; set; }

        [Required]
        public bool Selected { get; set; }
        public bool isUsersRoleClaim { get; set; }
        public bool IsEditable { get; set; }
    }

    public class RolePermissionManageVM
    {
        [Required]
        public string RoleId { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public bool Selected { get; set; }
    }


    //Get Claims 
    //public class GetAllClaimsRequestModel
    //{
    //    [Required]
    //    //[Display(Name = "Claim Type")]
    //    public string Type { get; set; }

    //    [Required]
    //    //[Display(Name = "Claim Value")]
    //    public string Value { get; set; }

    //    public bool Selected { get; set; }
    //}

    //public class GetAllClaimsResponse
    //{
    //    public string Type { get; set; }

    //    public string Value { get; set; }
    //}





}