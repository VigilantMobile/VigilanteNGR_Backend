using Application.DTOs.Account;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Settings;
using Infrastructure.Identity.Helpers;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Cache;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Enums;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Application.DTOs.Email;
using Infrastructure.Identity.Contexts;
using Infrastructure.Shared.Services;
using Application.DTOs.Account.RoleManagement;
using Microsoft.EntityFrameworkCore;
using Application.DTOs.Account.UserRoleManagement;

namespace Infrastructure.Identity.Services
{
    public class UserRolesManagerService : IUserRolesManagerService
    {
        private readonly IdentityContext _identityContext;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;
        private readonly VGNGAEmailSenders _emailSenderAddresses;
        private readonly IDateTimeService _dateTimeService;
        private readonly IRandomNumberGeneratorInterface _randomNumberGenerator;

        public UserRolesManagerService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JWTSettings> jwtSettings,
            IOptions<VGNGAEmailSenders> emailSenderAddresses,
            IDateTimeService dateTimeService,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,
            IRandomNumberGeneratorInterface randomNumberGenerator,
            IdentityContext identityContext)
        {
            _identityContext = identityContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _emailSenderAddresses = emailSenderAddresses.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
            this._emailService = emailService;
            _randomNumberGenerator = randomNumberGenerator;

        }

      



        public async Task<Response<UserRolesViewModel>> AddUserToRoles(AddUserToRoleRequest addUserToRoleRequest)
        {
            UserRolesViewModel userRoles = new UserRolesViewModel(); 

            var user = await _userManager.FindByIdAsync(addUserToRoleRequest.UserId);

            if (user == null)
            {
                return new Response<UserRolesViewModel>(userRoles, message: $"User does not exist", successStatus: false);
            }

            var roles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                return new Response<UserRolesViewModel>(userRoles, message: $"An error occurred while attempting to remove existing roles.", successStatus: false);
            }

            foreach (var role in _roleManager.Roles)
            {
                var AddtoRolesresult = await _userManager.AddToRolesAsync(user, addUserToRoleRequest.Roles.Where(x => x.Selected).Select(y => y.RoleName));
                if (!AddtoRolesresult.Succeeded)
                {
                    return new Response<UserRolesViewModel>(userRoles, message: $"An error occurred while attempting to remove existing roles.", successStatus: false);
                }
            }

            //get just added roles
            var justAddedRoles = await GetUserRoles(user);

            userRoles.FirstName = user.FirstName;
            userRoles.Email = user.Email;
            userRoles.LastName = user.LastName;
            userRoles.Phone = user.PhoneNumber;
            userRoles.UserName = user.UserName;
            userRoles.Roles = justAddedRoles;

            return new Response<UserRolesViewModel>(userRoles, message: $"User {user.Email} was successfully added to specified roles.");

        }

        public async Task<Response<GetAllStaffRolesViewModel>> GetAllStaffRoles()
        {
            var userRolesList = new List<UserRolesViewModel>();
            var staffAndRoles = new GetAllStaffRolesViewModel();
            List<ApplicationUser> applicationUsers = new List<ApplicationUser>();

            var customerRole = await _roleManager.FindByNameAsync(Application.Enums.Roles.Customer.ToString());
            var superAdminRole = await _roleManager.FindByNameAsync(Application.Enums.Roles.SuperAdmin.ToString());

            var allStaffUserIds = await _identityContext.UserRoles.Where(r => r.RoleId != customerRole.Id && r.RoleId != superAdminRole.Id).ToListAsync();

            foreach (var staffUserId in allStaffUserIds)
            {
                var staffUser = await _userManager.Users.Where(x => x.Id == staffUserId.UserId).FirstOrDefaultAsync();
                applicationUsers.Add(staffUser);
            }

            foreach (ApplicationUser user in applicationUsers)
            {
                var thisViewModel = new UserRolesViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.FirstName = user.FirstName;
                thisViewModel.LastName = user.LastName;
                thisViewModel.Roles = await GetUserRoles(user);
                userRolesList.Add(thisViewModel);
            }

            staffAndRoles.StaffandRoles = userRolesList;

            return new Response<GetAllStaffRolesViewModel>(staffAndRoles, message: $"List of all staff and associated roles successfully retrieved.");

        }

        public async Task<Response<UserRolesViewModel>> GetUserWithRoles(string userId)
        {
            UserRolesViewModel StaffUserRoles = new UserRolesViewModel();
            var staffUser = await _userManager.FindByIdAsync(userId);

            if (staffUser == null)
            {
                return new Response<UserRolesViewModel>(StaffUserRoles, message: $"User does not exist", successStatus: false);
            }
            var staffUserRoles = await GetUserRoles(staffUser);

            StaffUserRoles.FirstName = staffUser.FirstName;
            StaffUserRoles.Email = staffUser.Email;
            StaffUserRoles.LastName = staffUser.LastName;
            StaffUserRoles.Phone = staffUser.PhoneNumber;
            StaffUserRoles.UserName = staffUser.UserName;
            StaffUserRoles.Roles = staffUserRoles;

            return new Response<UserRolesViewModel>(StaffUserRoles, message: $"An error occurred while attempting to remove existing roles.", successStatus: false);

        }

        public async Task<Response<UsersAndClaimsResponse>> GetUserClaims(string userId)
        {
            UsersAndClaimsResponse usersAndClaimsViewModel = new UsersAndClaimsResponse();


            var user = GetById(userId);
            var userClaims = await _userManager.GetClaimsAsync(user);
            List<CustomClaims> customClaims = await _identityContext.CustomClaims.ToListAsync();
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                IdentityRole rl = await _identityContext.Roles.Where(r => r.Name == role).FirstOrDefaultAsync();
                roleClaims = _roleManager.GetClaimsAsync(rl).Result.ToList();
            }

            //var allUserClaims = userClaims.Union(roleClaims);

            //role claims to VM
            foreach (var claim in roleClaims)
            {
                RolePermissionClaimsViewModel claimVM = new RolePermissionClaimsViewModel();
                claimVM.Type = claim.Type;
                claimVM.Value = claim.Value;
                claimVM.FriendlyName = claim.Value;
                claimVM.Selected = true;
                claimVM.isUsersRoleClaim = true;
                claimVM.IsEditable = false;
                usersAndClaimsViewModel.UserClaimsList.Add(claimVM);

                usersAndClaimsViewModel.RoleClaimsList.Add(claimVM);
            }

            var claimsInUserClaimsThatDontExistInRoleClaims = userClaims.Where(uc => !roleClaims.Any(rc => rc.Type == uc.Type && rc.Value == uc.Value)).ToList();
            foreach (var claim in claimsInUserClaimsThatDontExistInRoleClaims)
            {
                RolePermissionClaimsViewModel claimVM = new RolePermissionClaimsViewModel();
                claimVM.Type = claim.Type;
                claimVM.Value = claim.Value;
                claimVM.FriendlyName = claim.Value;
                claimVM.Selected = true;
                claimVM.isUsersRoleClaim = false;
                claimVM.IsEditable = true;
                usersAndClaimsViewModel.UserClaimsList.Add(claimVM);
            }


            //claims in custom claims that dont exist in user claims
            var claimsInCustomClaimsThatDontExistInUserClaims = customClaims.Where(cc => !userClaims.Any(cl => cl.Type == cc.type && cl.Value == cc.value)).ToList();


            //claims in custom claims that dont exist in user claims to VM
            foreach (var claim in claimsInCustomClaimsThatDontExistInUserClaims)
            {
                RolePermissionClaimsViewModel claimVM = new RolePermissionClaimsViewModel();
                claimVM.Type = claim.type;
                claimVM.Value = claim.value;
                claimVM.FriendlyName = claim.FriendlyName;
                claimVM.Selected = false;
                claimVM.isUsersRoleClaim = false;
                claimVM.IsEditable = true;
                usersAndClaimsViewModel.UserClaimsList.Add(claimVM);
            }

            usersAndClaimsViewModel.UserId = user.Id;
            usersAndClaimsViewModel.UserFullName = $"{user.FirstName} {user.LastName}";

            return new Response<UsersAndClaimsResponse>(usersAndClaimsViewModel, $" User Claims successfully retrieved.");
        }

        public async Task<Response<UsersAndClaimsResponse>> ConfigureUserClaims(ManageUserClaimsRequest manageUserClaimsRequest)
        {
            UsersAndClaimsResponse usersAndClaimsViewModel = new UsersAndClaimsResponse();


            var user = GetById(manageUserClaimsRequest.UserId);

            var userClaims = await _userManager.GetClaimsAsync(user);

            List<CustomClaims> customClaims = await _identityContext.CustomClaims.ToListAsync();

            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                IdentityRole rl = await _identityContext.Roles.Where(r => r.Name == role).FirstOrDefaultAsync();
                roleClaims = _roleManager.GetClaimsAsync(rl).Result.ToList();
            }

            //var allUserClaims = userClaims.Union(roleClaims);


            //ensure claims to be edited are absent from customers roles list.

            var claimsinClaimstoBeEditedDontThatExistInAllClaimsList = customClaims.Where(c => !manageUserClaimsRequest.UserClaimsList.Any(ucl => ucl.Type == c.type && ucl.Value == c.value)).ToList();

            if (claimsinClaimstoBeEditedDontThatExistInAllClaimsList.Any())
            {
                return new Response<UsersAndClaimsResponse>(usersAndClaimsViewModel, message: $"An invalid claim pair was detected.", successStatus: false);
            }

            //remove existing users claims in role claims to prevent duplicate permissions
            foreach (var roleclaim in roleClaims)
            {
                foreach (var claim in manageUserClaimsRequest.UserClaimsList)
                {
                    if ((claim.Type == roleclaim.Type) && (claim.Value == roleclaim.Value))
                    {
                        manageUserClaimsRequest.UserClaimsList.Remove(claim);
                    }
                }
            }

            //finally add or remove
            foreach (var claimToBeAddedOrRemoved in manageUserClaimsRequest.UserClaimsList)
            {
                Claim claim = new Claim(claimToBeAddedOrRemoved.Type, claimToBeAddedOrRemoved.Value);

                if (claimToBeAddedOrRemoved.Selected == true)
                {
                    await _userManager.AddClaimAsync(user, claim);
                }
                else
                {
                    await _userManager.RemoveClaimAsync(user, claim);
                }
            }

            return await GetUserClaims(user.Id);

        }

        //for in service use
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        public ApplicationUser GetById(string id)
        {
            return _identityContext.Users.Find(id);
        }
    }

}
