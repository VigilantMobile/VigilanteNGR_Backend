﻿//using Application.DTOs.Account;
//using Application.DTOs.Account.RoleManagement;
//using Application.Wrappers;
//using Domain.Entities.Identity;
//using Infrastructure.Identity.Models;
//using Microsoft.AspNetCore.Identity;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Application.Interfaces
//{
//    public interface IRoleManagerService
//    {
//        Task<Response<string>> CreateRole(CreateRoleRequest roleRequest);
//        Task<Response<string>> EditRole(string roleId, string name);

//        Task<Response<List<IdentityRole>>> GetAllRoles();
//        Task<Response<IdentityRole>> GetRoleById(string roleId);
//        Task<Response<CustomClaims>> CreateCustomClaim(CreateCustomClaimsModel model);
//        Task<Response<List<CustomClaims>>> GetAllClaims();
//        Task<Response<RolesAndClaimsResponse>> GetRoleClaims(string roleId);
//        Task<Response<RolesAndClaimsResponse>> ConfigureARoleandClaims(ManageRoleClaimsRequest manageAllRoleClaimsRequest);
//        Task<Response<List<RolesAndClaimsResponse>>> GetAllRoleClaims();
//        Task<Response<List<RolesAndClaimsResponse>>> ConfigureAllRolesandClaims(List<ManageRoleClaimsRequest> manageAllRoleClaimsRequest);
//    }
//}
