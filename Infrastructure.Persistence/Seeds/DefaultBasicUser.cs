using Application.Enums;
using Infrastructure.Persistence.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
          try
          {
            //Seed Default User
                var defaultUser = new ApplicationUser
                {
                    UserName = "basicuser",
                    Email = "basicuser@gmail.com",
                    FirstName = "John",
                    LastName = "Doe",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    StateId = 25,
                    LGAId = 514
                };
                if (userManager.Users.All(u => u.Id != defaultUser.Id))
                {
                    var user = await userManager.FindByEmailAsync(defaultUser.Email);
                    if (user == null)
                    {
                        await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                        await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    }

                }
          }
          catch (Exception ex)
          {

          }
        }
    }
}
