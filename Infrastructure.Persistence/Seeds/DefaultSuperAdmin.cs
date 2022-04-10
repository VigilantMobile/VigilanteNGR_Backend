using Application.Enums;
using Domain.Entities.Identity;
using Infrastructure.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto.Prng.Drbg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Persistence.Seeds
{
    public static class DefaultSuperAdmin
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var superAdminRoleClaim = new Claim("Role", "SuperAdmin");
            var godModeClaim = new Claim("SuperAccess", "GodMode");
            var manageDepartmentsClaim = new Claim("CanManageDepartments", "Manage.Departments");

            var adminClaim = new Claim("Role", "Admin");
            var moderatorClaim = new Claim("Role", "Moderator");
            var operatorClaim = new Claim("Role", "VGNGAOperator");
            var staffCreateClaim = new Claim("CanCreateStaff", "staff.create");
            var canConfigureSettingsCreateClaim = new Claim("CanConfigureSettings", "settings.create");

            //Seed Admin
            var superAdminUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "anthony.odu@hotmail.com",
                FirstName = "Anthony",
                LastName = "Odu",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                StateId = 25,
                LGAId = 514
            };

            if (userManager.Users.All(u => u.Id != superAdminUser.Id))
            {
                var user = await userManager.FindByEmailAsync(superAdminUser.Email);
                if (user == null)
                {                   
                    await userManager.CreateAsync(superAdminUser, "123Pa$$word!");

                    //add to roles
                    await userManager.AddToRoleAsync(superAdminUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(superAdminUser, Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(superAdminUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(superAdminUser, Roles.SuperAdmin.ToString());

                    // claims
                    await userManager.AddClaimAsync(superAdminUser, superAdminRoleClaim);

                    await userManager.AddClaimAsync(superAdminUser, godModeClaim);
                    await userManager.AddClaimAsync(superAdminUser, adminClaim);
                    await userManager.AddClaimAsync(superAdminUser, moderatorClaim);
                    await userManager.AddClaimAsync(superAdminUser, operatorClaim);
                    await userManager.AddClaimAsync(superAdminUser, staffCreateClaim);
                }
            }
        }
    }
}
