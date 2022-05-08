using Application.DTOs.Account;
using Application.DTOs.Account.RoleManagement;
using Application.Exceptions;
using Infrastructure.Persistence.Models;
using Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace VGWebAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class RoleManagerController : ControllerBase
    {
        private readonly IRoleManagerService _roleManagerService;

        private readonly ILogger _logger;
        public RoleManagerController(IRoleManagerService roleManagerService, ILogger logger)
        {
            _roleManagerService = roleManagerService;
            _logger = logger;
        }


        [HttpPost("createrole")]
        public async Task<IActionResult> CreateRoleAsync(CreateRoleRequest roleRequest)
        {
            try
            {
                return Ok(await _roleManagerService.CreateRole(roleRequest));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }

        [HttpPost("editrole")]
        public async Task<IActionResult> EditRoleAsync(string roleId, string name)
        {
            try
            {
                return Ok(await _roleManagerService.EditRole(roleId, name));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }




        [HttpGet("getAllroles")]

        public async Task<IActionResult> GetAllRolesAsync()
        {
            try
            {
                return Ok(await _roleManagerService.GetAllRoles());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }


        [HttpGet("getroleById")]

        public async Task<IActionResult> GetRoleByIdAsync(string roleId)
        {
            try
            {
                return Ok(await _roleManagerService.GetRoleById(roleId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }

        [HttpPost("createcustomclaim")]
        public async Task<IActionResult> CreateCustomClaimAsync(CreateCustomClaimsModel request)
        {
            try
            {
                return Ok(await _roleManagerService.CreateCustomClaim(request));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }

        [HttpGet("getallclaims")]

        public async Task<IActionResult> GetAllClaimsAsync()
        {
            try
            {
                return Ok(await _roleManagerService.GetAllClaims());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }


        [HttpGet("getroleclaims")]

        public async Task<IActionResult> GetRoleClaimssync(string roleId)
        {
            try
            {
                return Ok(await _roleManagerService.GetRoleClaims(roleId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }

        [HttpPost("configurearoleandclaims")]

        public async Task<IActionResult> ConfigureARoleandClaims(ManageRoleClaimsRequest manageAllRoleClaimsRequest)
        {
            try
            {
                return Ok(await _roleManagerService.ConfigureARoleandClaims(manageAllRoleClaimsRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }

        [HttpPost("configurerolesandclaims")]

        public async Task<IActionResult> ConfigureAllRolesandClaims(List<ManageRoleClaimsRequest> manageAllRoleClaimsRequest)
        {
            try
            {
                return Ok(await _roleManagerService.ConfigureAllRolesandClaims(manageAllRoleClaimsRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }

        [HttpGet("getallrolesandclaims")]

        public async Task<IActionResult> GetAllRoleClaims()
        {
            try
            {
                return Ok(await _roleManagerService.GetAllRoleClaims());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }



    }
}