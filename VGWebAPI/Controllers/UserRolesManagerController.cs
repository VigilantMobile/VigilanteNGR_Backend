using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Account;
using Application.DTOs.Account.RoleManagement;
using Application.DTOs.Account.UserRoleManagement;
using Application.Exceptions;
using Application.Interfaces;
using Infrastructure.Persistence.Models;
using Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;





namespace VGWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesManagerController : ControllerBase
    {
        private readonly IUserRolesManagerService _userRolesManagerService;

        private readonly ILogger _logger;
        public UserRolesManagerController(IUserRolesManagerService userRolesManagerService, ILogger logger)
        {
            _userRolesManagerService = userRolesManagerService;
            _logger = logger;
        }


        [HttpPost("modifystaffroles")]
        public async Task<IActionResult> AddUserToRolesAsync(AddUserToRoleRequest  addUserToRoleRequest )
        {
            try
            {
                return Ok(await _userRolesManagerService.AddUserToRoles(addUserToRoleRequest));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }


        [HttpGet("getuserwithroles")]

        public async Task<IActionResult> GetUserWithRolesAsync(string userId)
        {
            try
            {
                return Ok(await _userRolesManagerService.GetUserWithRoles(userId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }


        [HttpGet("getallsaffwithroles")]
        public async Task<IActionResult> GetAllStaffRolesAsync()
        {
            try
            {
                return Ok(await _userRolesManagerService.GetAllStaffRoles());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }

        [HttpGet("getuserclaims")]
        public async Task<IActionResult> GetUserClaimsAsync(string userId)
        {
            try
            {
                return Ok(await _userRolesManagerService.GetUserClaims(userId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }

        [HttpPost("configureuserclaims")]

        public async Task<IActionResult> ConfigureUserClaimsAsync(ManageUserClaimsRequest manageUserClaimsRequest)
        {
            try
            {
                return Ok(await _userRolesManagerService.ConfigureUserClaims(manageUserClaimsRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }



    }
}