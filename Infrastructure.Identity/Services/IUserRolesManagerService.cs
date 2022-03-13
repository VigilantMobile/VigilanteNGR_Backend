using Application.DTOs.Account;
using Application.DTOs.Account.RoleManagement;
using Application.DTOs.Account.UserRoleManagement;
using Application.Wrappers;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRolesManagerService
    {
        Task<Response<UserRolesViewModel>> GetUserWithRoles(string userId);
        //Task<Response<UserAndRolesResponse>> GetUserRoles(UserAndRolesRequest request);
        Task<Response<UsersAndClaimsResponse>> GetUserClaims(string userId);
        Task<Response<GetAllStaffRolesViewModel>> GetAllStaffRoles();
        Task<Response<UserRolesViewModel>> AddUserToRoles(AddUserToRoleRequest addUserToRoleRequest);
        Task<Response<UsersAndClaimsResponse>> ConfigureUserClaims(ManageUserClaimsRequest manageUserClaimsRequest);
    }
}
