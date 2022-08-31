using Application.Enums;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            //Get default Claims

            List<RolePermissionClaimsModel> customClaims = RolePermissionClaims.GetDefaultPermissionClaimPairs();

            var staffManageClaim = customClaims.Where(c => c.Type == CustomClaimTypes.CanManageStaff.ToString()).FirstOrDefault();

            var canConfigureSettingsClaim = customClaims.Where(c => c.Type == CustomClaimTypes.CanConfigureSettings.ToString()).FirstOrDefault();

            var canApproveOperationsClaim = customClaims.Where(c => c.Type == CustomClaimTypes.CanApproveOperatorActions.ToString()).FirstOrDefault();

            var canManageDepartmentsClaim = customClaims.Where(c => c.Type == CustomClaimTypes.CanManageDepartments.ToString()).FirstOrDefault();

            var canInitiateNotificationsClaim = customClaims.Where(c => c.Type == CustomClaimTypes.CanInitiateNotifications.ToString()).FirstOrDefault();

            var canApproveNotificationsClaim = customClaims.Where(c => c.Type == CustomClaimTypes.CanApproveNotifications.ToString()).FirstOrDefault();

            var canInitiateNPFNotificationsClaim = customClaims.Where(c => c.Type == CustomClaimTypes.CanInitiateNPFNotifications.ToString()).FirstOrDefault();

            var canApproveNPFNotificationsClaim = customClaims.Where(c => c.Type == CustomClaimTypes.CanApproveNPFNotifications.ToString()).FirstOrDefault();

            //Claims for roles || //var staffCreateClaim = new Claim(CustomClaimTypes.CanManageStaff.ToString(), "staff.manage"); || //var canConfigureSettingsCreateClaim = new Claim("CanConfigureSettings", "settings.configure"); ||  //var canApproveSettings = new Claim("CanApproveOperatorActions", "settings.approve");

            //Create Super Admin Role
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            var newlyCreatedSuperAdminRole = await roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());

            //Add super admin role claims.

            await roleManager.AddClaimAsync(newlyCreatedSuperAdminRole, new Claim(canManageDepartmentsClaim.Type, canManageDepartmentsClaim.Value));

            await roleManager.AddClaimAsync(newlyCreatedSuperAdminRole, new Claim(staffManageClaim.Type.ToString(), staffManageClaim.Value));

            await roleManager.AddClaimAsync(newlyCreatedSuperAdminRole, new Claim(canConfigureSettingsClaim.Type.ToString(), canConfigureSettingsClaim.Value));

            await roleManager.AddClaimAsync(newlyCreatedSuperAdminRole, new Claim(canApproveOperationsClaim.Type.ToString(), canApproveOperationsClaim.Value));

            await roleManager.AddClaimAsync(newlyCreatedSuperAdminRole, new Claim(canInitiateNotificationsClaim.Type.ToString(), canInitiateNotificationsClaim.Value));

            await roleManager.AddClaimAsync(newlyCreatedSuperAdminRole, new Claim(canApproveNotificationsClaim.Type.ToString(), canApproveNotificationsClaim.Value));


            //Create Admin Role
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            var newlyCreatedAdminRole = await roleManager.FindByNameAsync(Roles.Admin.ToString());

            //Add admin claims

            await roleManager.AddClaimAsync(newlyCreatedAdminRole, new Claim(staffManageClaim.Type.ToString(), staffManageClaim.Value));

            await roleManager.AddClaimAsync(newlyCreatedAdminRole, new Claim(canConfigureSettingsClaim.Type.ToString(), canConfigureSettingsClaim.Value));

            await roleManager.AddClaimAsync(newlyCreatedAdminRole, new Claim(canApproveOperationsClaim.Type.ToString(), canApproveOperationsClaim.Value));

            await roleManager.AddClaimAsync(newlyCreatedAdminRole, new Claim(canInitiateNotificationsClaim.Type.ToString(), canInitiateNotificationsClaim.Value));




            //Create Moderator Role
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
            var newlyCreatedModeratorRole = await roleManager.FindByNameAsync(Roles.Moderator.ToString());
            //Add Moderator Claim

            await roleManager.AddClaimAsync(newlyCreatedModeratorRole, new Claim(canApproveOperationsClaim.Type.ToString(), canApproveOperationsClaim.Value));

            await roleManager.AddClaimAsync(newlyCreatedModeratorRole, new Claim(canApproveNotificationsClaim.Type.ToString(), canApproveNotificationsClaim.Value));

            await roleManager.AddClaimAsync(newlyCreatedModeratorRole, new Claim(canConfigureSettingsClaim.Type.ToString(), canConfigureSettingsClaim.Value));

            await roleManager.AddClaimAsync(newlyCreatedModeratorRole, new Claim(canInitiateNotificationsClaim.Type.ToString(), canInitiateNotificationsClaim.Value));

            //Create Operator Role
            await roleManager.CreateAsync(new IdentityRole(Roles.VGNGAOperator.ToString()));
            var newlyCreatedOperatorRole = await roleManager.FindByNameAsync(Roles.VGNGAOperator.ToString());

            //Add Claims 

            await roleManager.AddClaimAsync(newlyCreatedOperatorRole, new Claim(canConfigureSettingsClaim.Type.ToString(), canConfigureSettingsClaim.Value));

            await roleManager.AddClaimAsync(newlyCreatedOperatorRole, new Claim(canInitiateNotificationsClaim.Type.ToString(), canInitiateNotificationsClaim.Value));


            // Other Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Customer.ToString()));

            //NPF Admin
            await roleManager.CreateAsync(new IdentityRole(Roles.NPFAdmin.ToString()));

            var newlyCreatedNPFAdminRole = await roleManager.FindByNameAsync(Roles.NPFAdmin.ToString());
            await roleManager.AddClaimAsync(newlyCreatedNPFAdminRole, new Claim(canInitiateNPFNotificationsClaim.Type.ToString(), canInitiateNPFNotificationsClaim.Value));
            await roleManager.AddClaimAsync(newlyCreatedNPFAdminRole, new Claim(canApproveNPFNotificationsClaim.Type.ToString(), canApproveNPFNotificationsClaim.Value));

            //NPF Operator
            await roleManager.CreateAsync(new IdentityRole(Roles.NPFOperator.ToString()));
            var newlyCreatedNPFOperatorRole = await roleManager.FindByNameAsync(Roles.NPFOperator.ToString());
            await roleManager.AddClaimAsync(newlyCreatedNPFOperatorRole, new Claim(canInitiateNPFNotificationsClaim.Type.ToString(), canInitiateNPFNotificationsClaim.Value));


            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));

            await roleManager.CreateAsync(new IdentityRole(Roles.Test.ToString()));
        }
    }
}
