using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Account;
using Application.Exceptions;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;





namespace VGWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger _logger;
        public AccountController(IAccountService accountService, ILogger logger)
        {
            _accountService = accountService;
            _logger = logger;
        }


        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            try
            {
                var result = await _accountService.AuthenticateAsync(request, GenerateIPAddress());
                SetRefreshTokenInCookie(result.Data.RefreshToken);
                return Ok(result);

                //return Ok(await _accountService.AuthenticateAsync(request, GenerateIPAddress()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }

        [HttpPost("register")]

        public async Task<IActionResult> RegisterAsync(CustomerRegisterRequest request)
        {
            try
            {
                var origin = Request.Headers["origin"].ToString();
                return Ok(await _accountService.RegisterCustomerAsync(request, origin));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost("createstaff")]

        public async Task<IActionResult> RegisterStaffAsync(StaffRegisterRequest request)
        {
            try
            {
                var origin = Request.Headers["origin"];
                return Ok(await _accountService.RegisterStaffAsync(request, origin));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery]string userId, [FromQuery]string code)
        {          
            try
            {
                var origin = Request.Headers["origin"];
                return Ok(await _accountService.ConfirmEmailAsync(userId, code));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {          
            try
            {
                await _accountService.ForgotPassword(model, Request.Headers["origin"]);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {          
            try
            {
                return Ok(await _accountService.ResetPassword(model));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }

        private string GenerateIPAddress()
        {

            try
            {
                if (Request.Headers.ContainsKey("X-Forwarded-For"))
                    return Request.Headers["X-Forwarded-For"];
                else
                    return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest model)
        {        
            try
            {
                // accept token from request body or cookie
                string refreshToken = model.Token ?? Request.Cookies["refreshToken"];
                var response = await _accountService.RefreshTokenAsync(refreshToken);
                if (!string.IsNullOrEmpty(response.Data.RefreshToken))
                    SetRefreshTokenInCookie(response.Data.RefreshToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest model)
        {
            try
            {
                string token = model.Token ?? Request.Cookies["refreshToken"];

                if (string.IsNullOrEmpty(token))
                    return BadRequest(new { message = "Token is required" });

                var response = _accountService.RevokeToken(token);

                if (!response)
                    return NotFound(new { message = "Token not found" });

                return Ok(new { message = "Token revoked" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }


        private void SetRefreshTokenInCookie(string refreshToken)
        {   
            try
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(10),
                };
                Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }


        [HttpPost("tokens/{id}")]
        public IActionResult GetRefreshTokens(string id)
        {
            try
            {
                var user = _accountService.GetById(id);
                return Ok(user.RefreshTokens);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message.ToString());
            }
        }



        //[HttpPost("addrole")]
        //[Authorize(Roles = "Admin, SuperAdmin")]
        //public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
        //{

        //    try
        //    {
        //        var result = await _accountService.AddRoleAsync(model);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message, ex);
        //        throw new ApiException(ex.Message.ToString());
        //    }
        //}
    }
}