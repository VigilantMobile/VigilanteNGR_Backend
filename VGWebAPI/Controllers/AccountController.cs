using Application.DTOs.Account;
using Application.Exceptions;
using Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;


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
            var result = await _accountService.AuthenticateAsync(request, GenerateIPAddress());
            SetRefreshTokenInCookie(result.Data.RefreshToken);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(CustomerRegisterRequest request)
        {

            var origin = Request.Headers["origin"].ToString();
            return Ok(await _accountService.RegisterCustomerAsync(request, origin));

        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOTPAsync(VerifyOTPRequest request)
        {

            var origin = Request.Headers["origin"].ToString();
            return Ok(await _accountService.VerifyOTPandSignInAsync(request, origin));
        }

        [HttpPost("resend-otp")]
        public async Task<IActionResult> ResendOTPAsync(ResendOTPRequest request)
        {

            var origin = Request.Headers["origin"].ToString();
            return Ok(await _accountService.ResendOTPAsync(request, origin));
        }

        [HttpPost("update-customer-profile")]
        public async Task<IActionResult> UpdateCustomerProfile(UpdateProfileRequest request)
        {
            var origin = Request.Headers["origin"].ToString();
            return Ok(await _accountService.UpdateCustomerProfileAsync(request, origin));
        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost("create-staff")]

        public async Task<IActionResult> RegisterStaffAsync(StaffRegisterRequest request)
        {

            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterStaffAsync(request, origin));
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ConfirmEmailAsync(userId, code));
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            await _accountService.ForgotPassword(model, Request.Headers["origin"]);
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            return Ok(await _accountService.ResetPassword(model));
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
            // accept token from request body or cookie
            string refreshToken = model.Token ?? Request.Cookies["refreshToken"];
            var response = await _accountService.RefreshTokenAsync(refreshToken);
            if (!string.IsNullOrEmpty(response.Data.RefreshToken))
                SetRefreshTokenInCookie(response.Data.RefreshToken);
            return Ok(response);

        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest model)
        {

            string token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            var response = _accountService.RevokeToken(token);

            if (!response)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
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
            var user = _accountService.GetById(id);
            return Ok(user.RefreshToken);
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