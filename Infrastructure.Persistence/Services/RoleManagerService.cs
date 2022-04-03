using Application.DTOs.Account;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Settings;
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
using Infrastructure.Persistence.Contexts;
using Infrastructure.Shared.Services;
using Application.DTOs.Account.RoleManagement;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Models.Identity;
using Infrastructure.Persistence.Models;

namespace Infrastructure.Identity.Services
{
    public class RoleManagerService : IRoleManagerService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;
        private readonly VGNGAEmailSenders _emailSenderAddresses;
        private readonly IDateTimeService _dateTimeService;
        private readonly IRandomNumberGeneratorInterface _randomNumberGenerator;

        public RoleManagerService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JWTSettings> jwtSettings,
            IOptions<VGNGAEmailSenders> emailSenderAddresses,
            IDateTimeService dateTimeService,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,
            IRandomNumberGeneratorInterface randomNumberGenerator,
            ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _emailSenderAddresses = emailSenderAddresses.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
            this._emailService = emailService;
            _randomNumberGenerator = randomNumberGenerator;

        }

        public async Task<Response<string>> CreateRole(CreateRoleRequest roleRequest)
        {
            if (roleRequest.role != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleRequest.role.Trim()));
            }
          
            return new Response<string>($"{roleRequest.role} role was created successfully");
        }

        public async Task<Response<string>> EditRole(string roleId, string name)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                role.Name = name;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                    return new Response<string>($"Role {role.Name} was edited successfully");
            }
            return null;
        }


        public async Task<Response<List<IdentityRole>>> GetAllRoles()
        {
           
            var roles = await _roleManager.Roles.ToListAsync();
            return new Response<List<IdentityRole>>(roles, message: $"{ roles.Count().ToString() } roles retrieved.");
        }

        public async Task<Response<IdentityRole>> GetRoleById(string roleId)
        {

            var role = await _roleManager.FindByIdAsync(roleId);

            return new Response<IdentityRole>(role, message: $"Role found.");
        }



        public async Task<string> AddRoleAsync(AddToRoleModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return $"No Accounts Registered with {model.Email}.";
            }
     
            var roleExists = Enum.GetNames(typeof(Roles)).Any(x => x.ToLower() == model.Role.ToLower());
            if (roleExists)
            {
                var validRole = Enum.GetValues(typeof(Roles)).Cast<Roles>().Where(x => x.ToString().ToLower() == model.Role.ToLower()).FirstOrDefault();
                await _userManager.AddToRoleAsync(user, validRole.ToString());
                return $"Added {model.Role} to user {model.Email}.";
            }
            return $"Role {model.Role} not found.";

        }

        //done
        public async Task<Response<CustomClaims>> CreateCustomClaim(CreateCustomClaimsModel model)
        {
            var claim = await _context.CustomClaims.Where(x=>x.type == model.ClaimType).FirstOrDefaultAsync();
            if ((claim.type == model.ClaimType) && (claim.value == model.ClaimValue))
            {
                return new Response<CustomClaims>(claim, $"Claim Already Exists");
            }

            CustomClaims newclaim = new CustomClaims { type = model.ClaimType, value = model.ClaimValue };
            _context.CustomClaims.Add(newclaim);

            await _context.SaveChangesAsync();

            return new Response<CustomClaims>(newclaim, $"Claim was successfully created.");
        }

        //done
        public async Task<Response<List<CustomClaims>>> GetAllClaims()
        {
            List<CustomClaims> customClaims = await _context.CustomClaims.ToListAsync();

            return new Response<List<CustomClaims>>($"Custom Claims successfully retrieved.");
        }

        public async Task<Response<UserAndRolesResponse>> GetUserRoles(UserAndRolesRequest usersAndRolesRequest)
        {
            var user = await _userManager.FindByIdAsync(usersAndRolesRequest.UserId);
            
            var roles = await _userManager.GetRolesAsync(user);

            UserAndRolesResponse response = new UserAndRolesResponse();
            response.UserId = user.Id;
            response.UserFullName = $"{ user.FirstName} {user.LastName}";
            response.Roles = roles;

            return new Response<UserAndRolesResponse>(response, $"User roles successfully retrieved.");
        }


        public async Task<Response<RolesAndClaimsResponse>> GetRoleClaims(string roleId)
        {
            RolesAndClaimsResponse roleAndClaimsResponse = new RolesAndClaimsResponse();


            List<CustomClaims> customClaims = await _context.CustomClaims.ToListAsync();


            var role = await _context.Roles.Where(r => r.Id == roleId).FirstOrDefaultAsync();

            //role claims 
            var roleClaims = new List<Claim>();


            //ensure passed role claims exist in claims

            roleClaims = _roleManager.GetClaimsAsync(role).Result.ToList();
            
            //var allUserClaims = userClaims.Union(roleClaims);

            //role claims to VM
            foreach (var claim in roleClaims)
            {
                GetRoleClaimsResponse claimVM = new GetRoleClaimsResponse();
                claimVM.Type = claim.Type;
                claimVM.Value = claim.Value;
                claimVM.FriendlyName = claim.Value;
                claimVM.Selected = true;
                claimVM.IsEditable = true;

                roleAndClaimsResponse.RoleClaimsList.Add(claimVM);
            }

            roleAndClaimsResponse.RoleId = role.Id;
            roleAndClaimsResponse.RoleName = role.Name;

            return new Response<RolesAndClaimsResponse>(roleAndClaimsResponse, $" Role claims successfully retrieved.");
        }
        //




        //All Roles and their claims Claims - done
        public async Task<Response<List<RolesAndClaimsResponse>>> GetAllRoleClaims()
        {
            List<RolesAndClaimsResponse> rolesAndClaimsResponse = new List<RolesAndClaimsResponse>();

            List<IdentityRole> identityRoles = await _roleManager.Roles.ToListAsync();

            foreach (var identityrole in identityRoles)
            {

                var roleWithClaims = await GetRoleClaims(identityrole.Id);
                rolesAndClaimsResponse.Add(roleWithClaims.Data);
            }

            return new Response<List<RolesAndClaimsResponse>>(rolesAndClaimsResponse, $" User Custom Claims successfully retrieved.");
        }





        // configure all role claims
        public async Task<Response<List<RolesAndClaimsResponse>>> ConfigureAllRolesandClaims(List<ManageRoleClaimsRequest> manageAllRoleClaimsRequest)
        {
            List<RolesAndClaimsResponse> rolesAndClaimsResponses = new List<RolesAndClaimsResponse>();

            //validate all claim pairs
            List<RolePermissionClaimsViewModel> rolePermissionClaimsViewModel = new List<RolePermissionClaimsViewModel>();

            List<CustomClaims> customClaims = await _context.CustomClaims.ToListAsync();


            foreach (var roleClaimData in manageAllRoleClaimsRequest)
            {
                IdentityRole roletobeModified = await _roleManager.Roles.Where(x => x.Id == roleClaimData.RoleId).FirstOrDefaultAsync();

                if (roletobeModified == null)
                {
                    return new Response<List<RolesAndClaimsResponse>>(rolesAndClaimsResponses, message: $"One or more of the roles supplied do not exist.", successStatus: false);

                }
                // no foreign claims are introduced.
                var claimsinClaimstoBeEditedDontThatExistInAllClaimsList = customClaims.Where(c => !roleClaimData.RoleClaimsList.Any(ucl => ucl.Type == c.type && ucl.Value == c.value)).ToList();

                if (claimsinClaimstoBeEditedDontThatExistInAllClaimsList.Any())
                {
                    return new Response<List<RolesAndClaimsResponse>>(rolesAndClaimsResponses, message: $"An invalid claim pair was detected.", successStatus: false);
                }

            }


            //finally add or remove
            foreach (var claimToBeAddedOrRemoved in manageAllRoleClaimsRequest)
            {

                foreach (var entry in claimToBeAddedOrRemoved.RoleClaimsList)
                {
                    IdentityRole roletobeModified = await _roleManager.Roles.Where(x => x.Id == claimToBeAddedOrRemoved.RoleId).FirstOrDefaultAsync();

                    Claim claim = new Claim(entry.Type, entry.Value);

                    if (entry.Selected == true)
                    {
                        await _roleManager.AddClaimAsync(roletobeModified, claim);
                    }
                    else
                    {
                        await _roleManager.RemoveClaimAsync(roletobeModified, claim);
                    }
                }
            }

            return await GetAllRoleClaims();
        }


        public async Task<Response<RolesAndClaimsResponse>> ConfigureARoleandClaims(ManageRoleClaimsRequest manageAllRoleClaimsRequest)
        {
            RolesAndClaimsResponse rolesAndClaimsResponses = new RolesAndClaimsResponse();

            //validate all claim pairs
            List<RolePermissionClaimsViewModel> rolePermissionClaimsViewModel = new List<RolePermissionClaimsViewModel>();

            List<CustomClaims> customClaims = await _context.CustomClaims.ToListAsync();


            IdentityRole roletobeModified = await _roleManager.Roles.Where(x => x.Id == manageAllRoleClaimsRequest.RoleId).FirstOrDefaultAsync();

            if (roletobeModified == null)
            {
                return new Response<RolesAndClaimsResponse>(rolesAndClaimsResponses, message: $"Role not found", successStatus: false);

            }

            foreach (var roleClaimData in manageAllRoleClaimsRequest.RoleClaimsList)
            {

              
                // no foreign claims are introduced.
                var claimsinClaimstoBeEditedDontThatExistInAllClaimsList = customClaims.Where(c => !manageAllRoleClaimsRequest.RoleClaimsList.Any(ucl => ucl.Type == c.type && ucl.Value == c.value)).ToList();

                if (claimsinClaimstoBeEditedDontThatExistInAllClaimsList.Any())
                {
                    return new Response<RolesAndClaimsResponse>(rolesAndClaimsResponses, message: $"An invalid claim pair was detected.", successStatus: false);
                }

            }


            //finally add or remove
            foreach (var claimToBeAddedOrRemoved in manageAllRoleClaimsRequest.RoleClaimsList)
            {
                    Claim claim = new Claim(claimToBeAddedOrRemoved.Type, claimToBeAddedOrRemoved.Value);

                    if (claimToBeAddedOrRemoved.Selected == true)
                    {
                        await _roleManager.AddClaimAsync(roletobeModified, claim);
                    }
                    else
                    {
                        await _roleManager.RemoveClaimAsync(roletobeModified, claim);
                    }         
            }


            GetRoleClaimsRequest rolesAndClaimsRequest = new GetRoleClaimsRequest();

            return await GetRoleClaims(manageAllRoleClaimsRequest.RoleId);
        }

        public ApplicationUser GetById(string id)
        {
            return _context.Users.Find(id);
        }
    }

}
