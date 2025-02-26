//using Application.DTOs.Account;
//using Application.DTOs.Email;
//using Application.Enums;
//using Application.Exceptions;
//using Application.Interfaces;
//using Application.Wrappers;
//using Domain.Settings;
//using Infrastructure.Identity.Contexts;
//using Infrastructure.Identity.Helpers;
//using Infrastructure.Identity.Models;
//using Infrastructure.Shared.Services;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.WebUtilities;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;

//namespace Infrastructure.Identity.Services
//{
//    public class AccountService : IAccountService
//    {
//        private readonly IdentityContext _context;

//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly RoleManager<IdentityRole> _roleManager;
//        private readonly SignInManager<ApplicationUser> _signInManager;
//        private readonly IEmailService _emailService;
//        private readonly JWTSettings _jwtSettings;
//        private readonly VGNGAEmailSenders _emailSenderAddresses;
//        private readonly IDateTimeService _dateTimeService;
//        private readonly IRandomNumberGeneratorInterface _randomNumberGenerator;
//        private IHttpContextAccessor _accessor;

//        public AccountService(UserManager<ApplicationUser> userManager,
//            RoleManager<IdentityRole> roleManager,
//            IOptions<JWTSettings> jwtSettings,
//            IOptions<VGNGAEmailSenders> emailSenderAddresses,
//            IDateTimeService dateTimeService,
//            SignInManager<ApplicationUser> signInManager,
//            IEmailService emailService,
//            IRandomNumberGeneratorInterface randomNumberGenerator,
//            IdentityContext context,
//            IHttpContextAccessor accessor)
//        {
//            _context = context;
//            _userManager = userManager;
//            _roleManager = roleManager;
//            _jwtSettings = jwtSettings.Value;
//            _emailSenderAddresses = emailSenderAddresses.Value;
//            _dateTimeService = dateTimeService;
//            _signInManager = signInManager;
//            this._emailService = emailService;
//            _randomNumberGenerator = randomNumberGenerator;
//            _accessor = accessor;
//        }

//        public ClaimsPrincipal User => _accessor.HttpContext.User;

//        //login
//        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress = null)
//        {
//            //verify user
//            var user = await _userManager.FindByEmailAsync(request.Username);
//            if (user == null)
//            {
//                throw new ApiException($"No Accounts Registered with {request.Username}.");
//            }

//            //very credentials
//            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

//            if (!result.Succeeded)
//            {
//                throw new ApiException($"Invalid Credentials for '{request.Username}'.");
//            }

//            if (!user.EmailConfirmed)
//            {
//                throw new ApiException($"Account Not Confirmed for '{request.Username}'.");
//            }

//            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
//            AuthenticationResponse response = new AuthenticationResponse();
//            response.Id = user.Id;
//            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
//            response.Email = user.Email;
//            response.UserName = user.UserName;

//            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
//            response.Roles = rolesList.ToList();
//            response.IsVerified = user.EmailConfirmed;

//            //var refreshToken = GenerateRefreshToken(ipAddress);
//            //response.RefreshToken = refreshToken.Token;

//            if (user.RefreshTokens.Any(a => a.IsActive))
//            //if (!(user.RefreshTokens.Count < 1))
//            {
//                var activeRefreshToken = user.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
//                response.RefreshToken = activeRefreshToken.Token;
//                response.RefreshTokenExpiration = activeRefreshToken.Expires;
//            }

//            else
//            {
//                var refreshToken = GenerateRefreshToken(ipAddress);
//                response.RefreshToken = refreshToken.Token;
//                response.RefreshTokenExpiration = refreshToken.Expires;
//                user.RefreshTokens.Add(refreshToken);
//                _context.Update(user);
//                _context.SaveChanges();
//            }

//            return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}");
//        }

//        //For customer:
//        public async Task<Response<CustomerRegistrationResponse>> RegisterCustomerAsync(CustomerRegisterRequest request, string origin)
//        {
//            var userWithSameUserName = await _userManager.FindByNameAsync(request.PhoneNumber);
//            if (userWithSameUserName != null)
//            {
//                CustomerRegistrationResponse response = new CustomerRegistrationResponse { Message = "Account already exists.", UserAlreadyExists = true };
//                return new Response<CustomerRegistrationResponse>(response, message: $"Phone number is already registered, proceed to login. Thanks.", successStatus: false);

//                //throw new ApiException($"User already exists.");
//            }
//            var user = new ApplicationUser
//            {

//                Email = request.Email,
//                FirstName = request.FirstName,
//                LastName = request.LastName,
//                UserName = request.PhoneNumber,
//                PhoneNumber = request.PhoneNumber,
//                EmailConfirmed = false,      // set email and phone confirmed automatically after configuring twilio sendgrid for Otps
//                PhoneNumberConfirmed = false

//            };

//            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);

//            if (userWithSameEmail == null)
//            {
//                var result = await _userManager.CreateAsync(user, request.Password);

//                if (result.Succeeded)
//                {
//                    await _userManager.AddToRoleAsync(user, Roles.Customer.ToString());

//                    var verificationUri = await SendVerificationEmail(user, origin);
//                    //TODO: Attach Email Service here and configure it via appsettings
//                    await _emailService.SendAsync(new Application.DTOs.Email.EmailRequest() { From = "info@vigilanteng.com", To = user.Email, Body = $"Please confirm your account by visiting this URL {verificationUri}", Subject = "Confirm Registration" });


//                    CustomerRegistrationResponse response = new CustomerRegistrationResponse { Message = "Account Created Successfully.", UserAlreadyExists = false };
//                    return new Response<CustomerRegistrationResponse>(response, message: $"Your account has been created successfuly. ");

//                }
//                else
//                {
//                    //throw new ApiException($"{result.Errors}"); (Original)

//                    // throw new ApiException($"{string.Join(", ", result.Errors.Select(x => "Code " + x.Code + " Description" + x.Description))}"); 

//                    throw new ApiException($"{string.Join(", ", result.Errors.Select(x => x.Description))}");
//                }
//            }
//            else
//            {
//                CustomerRegistrationResponse response = new CustomerRegistrationResponse { Message = "Account already exists.", UserAlreadyExists = true };
//                return new Response<CustomerRegistrationResponse>(response, message: $"Email is already registered, Kindly login with the associated details. Thanks.", successStatus: false);

//                //throw new ApiException($"Email {request.Email } is already registered.");
//            }
//        }

//        //For staff.
//        public async Task<Response<StaffRegistrationResponse>> RegisterStaffAsync(StaffRegisterRequest request, string origin)
//        {
//            var userWithSameUserName = await _userManager.FindByEmailAsync(request.Email);
//            if (userWithSameUserName != null)
//            {
//                // throw new ApiException($"Username '{request.Email}' is already taken.");

//                StaffRegistrationResponse response = new StaffRegistrationResponse { Message = "Account already exists.", UserAlreadyExists = true };

//                return new Response<StaffRegistrationResponse>(response, message: $"User is already registered, Kindly login with the associated details. Thanks.", successStatus: false);
//            }

//            string defaultPassword = _randomNumberGenerator.GenerateRandomNumber(6, Mode.AlphaNumeric);

//            var user = new ApplicationUser
//            {
//                Email = request.Email,
//                FirstName = request.FirstName,
//                LastName = request.LastName,
//                UserName = request.Email,
//                PhoneNumber = request.PhoneNumber,
//                EmailConfirmed = false,
//                PhoneNumberConfirmed = false,
//                PasswordHash = defaultPassword
//            };

//            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);

//            if (userWithSameEmail == null)
//            {
//                var result = await _userManager.CreateAsync(user, request.Password);

//                if (result.Succeeded)
//                {
//                    //add to all roles

//                    foreach (var roleId in request.RoleIds)
//                    {
//                        var role = await _roleManager.FindByIdAsync(roleId);

//                        await _userManager.AddToRoleAsync(user, role.Name);

//                    }
//                    var verificationUri = await SendVerificationEmail(user, origin);


//                    //TODO: Attach Email Service here and configure it via appsettings
//                    await _emailService.SendAsync(new Application.DTOs.Email.EmailRequest() { From = "admin@vigilanteng.com", To = user.Email, BodyParagraph1 = $"Please confirm your account by visiting this URL {verificationUri}", Subject = "Email Set Up", Heading = "Email Setup Successful" });

//                    StaffRegistrationResponse response = new StaffRegistrationResponse { Message = "Staff account created.", VerificationUrl = verificationUri, UserAlreadyExists = false };
//                    return new Response<StaffRegistrationResponse>(response, message: $"Staff account was registered successfully. {verificationUri}");

//                }
//                else
//                {
//                    //throw new ApiException($"{result.Errors}"); (Original)

//                    // throw new ApiException($"{string.Join(", ", result.Errors.Select(x => "Code " + x.Code + " Description" + x.Description))}"); 

//                    throw new ApiException($"{string.Join(", ", result.Errors.Select(x => x.Description))}");
//                }
//            }
//            else
//            {
//                StaffRegistrationResponse response = new StaffRegistrationResponse { Message = "Account already exists.", UserAlreadyExists = true };

//                return new Response<StaffRegistrationResponse>(response, message: $"User is already registered, Kindly login with the associated details. Thanks.", successStatus: false);
//            }
//        }



//        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
//        {
//            var userClaims = await _userManager.GetClaimsAsync(user);
//            var roles = await _userManager.GetRolesAsync(user);

//            var roleClaims = new List<Claim>();

//            for (int i = 0; i < roles.Count; i++)
//            {
//                roleClaims.Add(new Claim("roles", roles[i]));
//            }



//            string ipAddress = IpHelper.GetIpAddress();

//            var claims = new[]
//            {
//                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                new Claim(JwtRegisteredClaimNames.Email, user.Email),
//                new Claim("uid", user.Id),
//                new Claim("ip", ipAddress)
//            }
//            .Union(userClaims)
//            .Union(roleClaims);

//            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
//            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

//            var jwtSecurityToken = new JwtSecurityToken(
//                issuer: _jwtSettings.Issuer,
//                audience: _jwtSettings.Audience,
//                claims: claims,
//                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
//                signingCredentials: signingCredentials);
//            return jwtSecurityToken;
//        }

//        private string RandomTokenString()
//        {
//            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
//            var randomBytes = new byte[40];
//            rngCryptoServiceProvider.GetBytes(randomBytes);
//            // convert random bytes to hex string
//            return BitConverter.ToString(randomBytes).Replace("-", "");
//        }

//        private async Task<string> SendVerificationEmail(ApplicationUser user, string origin)
//        {
//            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
//            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
//            var route = "api/account/confirm-email/";
//            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
//            var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id);
//            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
//            //Email Service Call Here
//            return verificationUri;
//        }

//        public async Task<Response<string>> ConfirmEmailAsync(string userId, string code)
//        {
//            var user = await _userManager.FindByIdAsync(userId);
//            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
//            var result = await _userManager.ConfirmEmailAsync(user, code);
//            if (result.Succeeded)
//            {
//                return new Response<string>(user.Id, message: $"Account Confirmed for {user.Email}. You can now use the /api/Account/authenticate endpoint.");
//            }
//            else
//            {
//                throw new ApiException($"An error occured while confirming {user.Email}.");
//            }
//        }

//        private RefreshToken GenerateRefreshToken(string ipAddress = null)
//        {
//            return new RefreshToken
//            {
//                Token = RandomTokenString(),
//                Expires = DateTime.UtcNow.AddDays(7),
//                Created = DateTime.UtcNow,
//                CreatedByIp = ipAddress == null ? null : ipAddress
//            };
//        }

//        public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
//        {
//            var account = await _userManager.FindByEmailAsync(model.Email);

//            // always return ok response to prevent email enumeration
//            if (account == null) return;

//            var code = await _userManager.GeneratePasswordResetTokenAsync(account);
//            var route = "api/account/reset-password/";
//            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
//            var emailRequest = new EmailRequest()
//            {
//                Body = $"You reset token is - {code}",
//                To = model.Email,
//                Subject = "Reset Password",
//            };
//            await _emailService.SendAsync(emailRequest);
//        }

//        public async Task<Response<string>> ResetPassword(ResetPasswordRequest model)
//        {
//            var account = await _userManager.FindByEmailAsync(model.Email);
//            if (account == null) throw new ApiException($"No Accounts Registered with {model.Email}.");
//            var result = await _userManager.ResetPasswordAsync(account, model.Token, model.Password);
//            if (result.Succeeded)
//            {
//                return new Response<string>(model.Email, message: $"Password Reset.");
//            }
//            else
//            {
//                throw new ApiException($"Error occured while reseting the password.");
//            }
//        }

//        // refresh refresh token 
//        public async Task<Response<AuthenticationResponse>> RefreshTokenAsync(string token)
//        {
//            var authenticationModel = new AuthenticationResponse();

//            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

//            if (user == null)
//            {
//                //authenticationModel.IsVerified = false;
//                //authenticationModel.Message = $"Token did not match any users.";
//                //return new Response<AuthenticationResponse>(authenticationModel, $"Token did not match any users.");

//                throw new ApiException($"Token did not match any users");
//            }

//            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

//            if (!refreshToken.IsActive)
//            {
//                throw new ApiException($"Refresh Token is Inactive.");
//            }

//            //Revoke Current Refresh Token
//            refreshToken.Revoked = DateTime.UtcNow;

//            //Generate new Refresh Token and save to Database
//            var newRefreshToken = GenerateRefreshToken();
//            user.RefreshTokens.Add(newRefreshToken);

//            _context.Update(user);
//            _context.SaveChanges();

//            //Generates new jwt
//            authenticationModel.IsVerified = true;
//            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
//            authenticationModel.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
//            authenticationModel.Email = user.Email;
//            authenticationModel.UserName = user.UserName;
//            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
//            authenticationModel.Roles = rolesList.ToList();
//            authenticationModel.RefreshToken = newRefreshToken.Token;
//            authenticationModel.RefreshTokenExpiration = newRefreshToken.Expires;
//            return new Response<AuthenticationResponse>(authenticationModel, $"New refresh token generated.");
//        }

//        //revoke token
//        public bool RevokeToken(string token)
//        {
//            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

//            // return false if no user found with token
//            if (user == null) return false;

//            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

//            // return false if token is not active
//            if (!refreshToken.IsActive) return false;

//            // revoke token and save
//            refreshToken.Revoked = DateTime.UtcNow;

//            //_context.Update(user);
//            //_context.SaveChanges();

//            return true;
//        }

//        // add-role

//        //public async Task<string> AddRoleAsync(AddRoleModel model)
//        //{
//        //    var user = await _userManager.FindByEmailAsync(model.Email);

//        //    if (user == null)
//        //    {
//        //        return $"No Accounts Registered with {model.Email}.";
//        //    }

//        //    var roleExists = Enum.GetNames(typeof(Roles)).Any(x => x.ToLower() == model.Role.ToLower());
//        //    if (roleExists)
//        //    {
//        //        var validRole = Enum.GetValues(typeof(Roles)).Cast<Roles>().Where(x => x.ToString().ToLower() == model.Role.ToLower()).FirstOrDefault();
//        //        await _userManager.AddToRoleAsync(user, validRole.ToString());
//        //        return $"Added {model.Role} to user {model.Email}.";
//        //    }
//        //    return $"Role {model.Role} not found.";

//        //}





//        public ApplicationUser GetById(string id)
//        {
//            return _context.Users.Find(id);
//        }

//    }

//}
