using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Identity.Models
{
    //public class CustomSignInManager<TUser> : SignInManager<ApplicationUser> where TUser : class
    //{

    //    public CustomSignInManager(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, 
    //        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
    //        IOptions<IdentityOptions> optionsAccessor,
    //        ILogger<SignInManager<ApplicationUser>> logger, IAuthenticationSchemeProvider schemes,
    //        IUserConfirmation<ApplicationUser> confirmation)

    //    {

    //    }


    //    public override async Task<bool> CanSignInAsync(ApplicationUser user)
    //    {
    //        var emailConfirmed = await UserManager.IsEmailConfirmedAsync(user);
    //        var phoneConfirmed = await UserManager.IsPhoneNumberConfirmedAsync(user);

    //        return emailConfirmed || phoneConfirmed;

    //    }
    //}
}
