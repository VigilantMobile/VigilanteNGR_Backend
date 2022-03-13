using Infrastructure.Identity.Contexts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Models
{
    public static class RolePermissionClaims
    {
        public static List<RolePermissionClaimsModel> ClaimPairs { get; set; }

        public static List<RolePermissionClaimsModel> GetDefaultPermissionClaimPairs()
        {
            List<RolePermissionClaimsModel> permissionClaims = new List<RolePermissionClaimsModel>();

            permissionClaims.Add(new RolePermissionClaimsModel { Type = CustomClaimTypes.CanManageStaff.ToString(), Value = "Manage.Staff",FriendlyName = "Staff Management", Selected = false });

            permissionClaims.Add(new RolePermissionClaimsModel { Type = CustomClaimTypes.CanConfigureSettings.ToString(), Value = "Configure.Settings", FriendlyName = " Configure Settings", Selected = false });

            permissionClaims.Add(new RolePermissionClaimsModel { Type = CustomClaimTypes.CanApproveOperatorActions.ToString(), Value = "Approve.Operator.Actions",  FriendlyName = "Approve Operator Actions", Selected = false });

            permissionClaims.Add(new RolePermissionClaimsModel { Type = CustomClaimTypes.CanManageDepartments.ToString(), Value = "Manage.Departments", FriendlyName = "Manage Departments", Selected = false });
            
            permissionClaims.Add(new RolePermissionClaimsModel { Type = CustomClaimTypes.CanInitiateNotifications.ToString(), Value = "Initiate.Notifications", FriendlyName = "Initiate Notifications", Selected = false });

            permissionClaims.Add(new RolePermissionClaimsModel { Type = CustomClaimTypes.CanApproveNotifications.ToString(), Value = "Approve.Notifications", FriendlyName = "Approve Notifications", Selected = false });

            permissionClaims.Add(new RolePermissionClaimsModel { Type = CustomClaimTypes.CanInitiateNPFNotifications.ToString(), Value = "Initiate.NPF.Notifications", FriendlyName = "Initiate NPF Notifications", Selected = false });

            permissionClaims.Add(new RolePermissionClaimsModel { Type = CustomClaimTypes.CanApproveNPFNotifications.ToString(), Value = "Approve.NPF.Notifications", FriendlyName = "Approve NPFNotifications", Selected = false });

            return permissionClaims;
        }
    }

    public class RolePermissionClaimsModel
    {

        public string RoleId { get; set; }

        public string RoleName { get; set; }

        [Required]
        [Display(Name = "Claim Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Claim Value")]
        public string Value { get; set; }
        public string FriendlyName {get; set; }

        public bool Selected { get; set; }

    }

    public enum CustomClaimTypes
    {
        CanManageStaff,
        CanConfigureSettings,
        CanApproveOperatorActions,
        CanManageDepartments,
        CanInitiateNotifications,
        CanApproveNotifications,
        CanInitiateNPFNotifications,
        CanApproveNPFNotifications,
    }
}
