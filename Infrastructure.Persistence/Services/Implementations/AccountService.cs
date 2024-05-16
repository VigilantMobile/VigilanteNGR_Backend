using Application.DTOs.Account;
using Application.DTOs.Email;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Common.Enums;
using Domain.Entities.AppTroopers.Subscription;
using Domain.Entities.Identity;
using Domain.Settings;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Helpers;
using Infrastructure.Shared.Services;
using Infrastructure.Shared.Services.Notification.SMSHelperClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace Infrastructure.Persistence.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly ISMSService _smsService;
        private readonly JWTSettings _jwtSettings;
        private readonly VGNGAEmailSenders _emailSenderAddresses;
        private readonly IDateTimeService _dateTimeService;
        private readonly IRandomNumberGeneratorInterface _randomNumberGenerator;
        private IHttpContextAccessor _accessor;
        //private readonly IMailgunEmailService _mailgunEmailService;
        private readonly ILogger _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly AppConfig _appConfig;




        public AccountService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JWTSettings> jwtSettings,
            IOptions<VGNGAEmailSenders> emailSenderAddresses,
            IDateTimeService dateTimeService,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,
            ISMSService smsService,
            IRandomNumberGeneratorInterface randomNumberGenerator,
            ApplicationDbContext context,
            IHttpContextAccessor accessor,
            IMemoryCache memoryCache,
        //IMailgunEmailService mailgunEmailService,
        ILogger logger,
        IOptions<AppConfig> appConfig)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _emailSenderAddresses = emailSenderAddresses.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
            this._emailService = emailService;
            this._smsService = smsService;
            _randomNumberGenerator = randomNumberGenerator;
            _accessor = accessor;
            //_mailgunEmailService = mailgunEmailService;
            _logger = logger;
            _memoryCache = memoryCache;
            _appConfig = appConfig.Value;
        }

        public ClaimsPrincipal User => _accessor.HttpContext.User;

        //login
        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress = null)
        {
            try
            {
                //verify user

                //var user = await _userManager.FindByEmailAsync(request.Username);
                //var userName = await _context.Users.Where(x => x.UserName == request.Username).FirstOrDefaultAsync();
                var user = await _userManager.FindByNameAsync(request.Username);
                if (user == null)
                {
                    throw new ApiException($"No Accounts Registered with {request.Username}.");

                }

                if (!user.isActive)
                {
                    throw new ApiException($"Account {request.Username} is presently disabled. Please contact support@vigilantng.com for assistance.");
                }

                if (!user.PhoneNumberConfirmed)
                {
                    throw new ApiException($"Account {request.Username} has not been verified.");
                }

                //very credentials
                var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: false);

                if (!result.Succeeded)
                {
                    throw new ApiException($"Invalid Credentials for '{request.Username}'.");
                }

                //if (!user.EmailConfirmed)
                //{
                //    throw new ApiException($"Account Not Confirmed for '{request.Username}'.");
                //}

                JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
                AuthenticationResponse response = new AuthenticationResponse();
                response.Id = user.Id;
                response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                response.Email = user.Email;
                response.UserName = user.UserName;

                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                response.Roles = rolesList.ToList();
                response.IsVerified = user.EmailConfirmed;

                //Generate Refresh Token
                var refreshToken = GenerateRefreshToken(ipAddress);

                user.RefreshToken = refreshToken.Token;
                user.RefreshTokenExpiry = refreshToken.Expires;
                user.RefreshToken = refreshToken.Token;
                _context.Update(user);
                _context.SaveChanges();

                response.RefreshToken = refreshToken.Token;
                response.RefreshTokenExpiration = refreshToken.Expires;

                //return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}");
                return new Response<AuthenticationResponse>(response, responsestatus: ResponseStatus.success.ToString(), message: $"Authenticated {user.UserName}");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }

        //For customer:
        public async Task<Response<RegisterResponse>> RegisterCustomerAsync(CustomerRegisterRequest request, string origin)
        {
            try
            {
                var userWithSameUserName = await _userManager.FindByNameAsync(request.PhoneNumber);
                if (userWithSameUserName != null)
                {
                    RegisterResponse response = new RegisterResponse { Message = "Account already exists."};
                    return new Response<RegisterResponse>(response, responsestatus:ResponseStatus.success.ToString(), message: $"Phone number is already registered, proceed to login. Thanks.");
                    //throw new ApiException($"User already exists.");
                }

                var user = new ApplicationUser
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserName = request.PhoneNumber,
                    PhoneNumber = request.PhoneNumber,
                    TownId = Guid.Parse(request.TownId),
                    EmailConfirmed = false,      // set email and phone confirmed automatically after configuring twilio sendgrid for Otps
                    PhoneNumberConfirmed = false,
                    isActive = true,
                    SubscriptionId = Guid.Parse("5F767D57-A64C-4B6F-96A9-CE81EF8A9F66")

                };

                var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);

                if (userWithSameEmail == null)
                {
                    var result = await _userManager.CreateAsync(user, request.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, Roles.Customer.ToString());

                        //Create Customer Wallet
                        Wallet wallet = new Wallet
                        {
                            Id = Guid.NewGuid(),
                            ApplicationUserId = user.Id,
                            WalletBalance = 0.00M,
                            CreatedBy = user.Id,
                        };

                        _context.Wallets.Add(wallet);
                        _context.SaveChanges();


                        var verificationUri = await GetVerificationUri(user, _appConfig.AppOrigin);
                        //TODO: Attach Email Service here and configure it via appsettings
                        //await _emailService.SendAsync(new Application.DTOs.Email.EmailRequest() 
                        //{ 
                        //    From = "info@vigilanteng.com", 
                        //    To = user.Email,
                        //    Username = user.FirstName,
                        //    BodyParagraph1 = $"Thank you for joining Vigilante NG",
                        //    BodyParagraph2 = $"Please confirm your account by visiting this URL {verificationUri}",
                        //    Subject = "Confirm your Vigilante NG registration"
                        //});

                        //Store email body in db (Cache 
                        await _emailService.SendAsync(new Application.DTOs.Email.EmailRequest()
                        {
                            From = "info@vigilanteng.com",
                            To = user.Email,
                            Username = user.FirstName,
                            BodyParagraph1 = $"Welcome to the Vigilant NG community. Please click the link below to verify your email address.",
                            ButtonLabel = "Confirm Account",
                            ButtonUrl = $"{verificationUri}",
                            BodyParagraph2 = $"Please confirm your account by visiting clicking the button below",
                            Subject = "Confirm your Vigilant NG registration"
                        });

                        //store otp in cache
                        string OTP = _randomNumberGenerator.GenerateRandomNumber(6, Mode.Numeric);
                        request.PhoneNumber = $"+{request.PhoneNumber}";
                        var cacheExpiryOptions = new MemoryCacheEntryOptions
                        {
                            AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                            Priority = CacheItemPriority.Normal,
                            SlidingExpiration = TimeSpan.FromMinutes(5),
                            Size = 1024,
                        };

                        _memoryCache.Set(request.PhoneNumber, OTP, cacheExpiryOptions);

                        //sms
                        await _smsService.SendSMSAsync(new SMSRequest()
                        {
                            messages= new List<SMSMessage>()
                            { 
                                new SMSMessage 
                                { 
                                    channel="sms",
                                    content=$"Welcome to the Vigilant community, Your verification code is {OTP}. Your code expires in 6 minutes. Do not share this code with anyone.",
                                    data_coding = "text",
                                    originator = "Vigilant",
                                    recipients = new List<string>(){ request.PhoneNumber }
                                }
                            }
                        });

                        RegisterResponse response = new RegisterResponse { Message = "Account Created Successfully." };
                        //return new Response<RegisterResponse>(response, message: $"Your account has been created successfuly. ", success: true);
                        return new Response<RegisterResponse>(response, responsestatus: ResponseStatus.success.ToString(), message: $"Your account has been created successfuly.");
                    }
                    else
                    {

                        //throw new ApiException($"{result.Errors}"); (Original)

                        // throw new ApiException($"{string.Join(", ", result.Errors.Select(x => "Code " + x.Code + " Description" + x.Description))}"); 

                        throw new ApiException($"{string.Join(", ", result.Errors.Select(x => x.Description))}");
                    }
                }
                else
                {
                    RegisterResponse response = new RegisterResponse { Message = "Account already exists." };
                    return new Response<RegisterResponse>(response, responsestatus: ResponseStatus.success.ToString(), message: $"Email is already registered, Kindly login with the associated details. Thanks.");

                    //throw new ApiException($"Email {request.Email } is already registered.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }

        //Resend OTP
        public async Task<Response<string>> ResendOTPAsync(ResendOTPRequest request, string ipAddress = null)
        {
            try
            {
                //verify user

                //var user = await _userManager.FindByEmailAsync(request.Username);
                //var userName = await _context.Users.Where(x => x.UserName == request.Username).FirstOrDefaultAsync();

                var user = await _userManager.FindByNameAsync(request.PhoneNumber);

                if (user == null)
                {
                    throw new ApiException($"Invalid Credentials for '{request.PhoneNumber}'.");
                }

                //very credentials
                string value = string.Empty;
                _memoryCache.TryGetValue($"+{request.PhoneNumber}", out value);

                //Remove existing cache
                if (!value.IsNullOrEmpty())
                {
                    _memoryCache.Remove(request.PhoneNumber);
                }

                //Create new cache entry

                //store otp in cache
                string OTP = _randomNumberGenerator.GenerateRandomNumber(6, Mode.Numeric);
                request.PhoneNumber = $"+{request.PhoneNumber}";
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.Normal,
                    SlidingExpiration = TimeSpan.FromMinutes(5),
                    Size = 1024,
                };
                _memoryCache.Set(request.PhoneNumber, OTP, cacheExpiryOptions);

                //sms
                await _smsService.SendSMSAsync(new SMSRequest()
                {
                    messages = new List<SMSMessage>()
                    {
                        new SMSMessage
                        {
                            channel="sms",
                            content=$"Welcome to the Vigilant community, Your verification code is {OTP}. Your code expires in 6 minutes. Do not share this code with anyone.",
                            data_coding = "text",
                            originator = "Vigilant",
                            recipients = new List<string>(){ request.PhoneNumber }
                        }
                    }
                });
               
                return new Response<string>(responsestatus: ResponseStatus.success.ToString(), $"Resent OTP for Phone Number: {user.UserName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }

        //Verify OTP Async

        public async Task<Response<AuthenticationResponse>> VerifyOTPandSignInAsync(VerifyOTPRequest request, string ipAddress = null)
        {
            try
            {
                //verify user

                //var user = await _userManager.FindByEmailAsync(request.Username);
                //var userName = await _context.Users.Where(x => x.UserName == request.Username).FirstOrDefaultAsync();

                var user = await _userManager.FindByNameAsync(request.PhoneNumber);

                if (user == null)
                {
                   throw new ApiException($"Invalid Credentials for '{ request.PhoneNumber }'.");
                }

                if (!user.isActive)
                {
                    throw new ApiException($"Account {request.PhoneNumber} is presently disabled. Please contact support@vigilantng.com for assistance.");
                }
       
                //very credentials
                string value = string.Empty;
                _memoryCache.TryGetValue($"+{request.PhoneNumber}", out value);

                if (!(value == request.OTP))
                {
                    throw new ApiException($"Invalid or Expired OTP.");
                }

                user.isActive = true;
                user.PhoneNumberConfirmed= true;

                JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
                AuthenticationResponse response = new AuthenticationResponse();
                response.Id = user.Id;
                response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                response.Email = user.Email;
                response.UserName = user.UserName;

                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                response.Roles = rolesList.ToList();
                response.IsVerified = user.EmailConfirmed;

                //Generate Refresh Token
                var refreshToken = GenerateRefreshToken(ipAddress);

                user.RefreshToken = refreshToken.Token;
                user.RefreshTokenExpiry = refreshToken.Expires;
                user.RefreshToken = refreshToken.Token;
                _context.Update(user);
                _context.SaveChanges();

                response.RefreshToken = refreshToken.Token;
                response.RefreshTokenExpiration = refreshToken.Expires;

                return new Response<AuthenticationResponse>(response, $"{user.FirstName}, thank you for verifying your phone number. Welcome to the Vigilant community.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }

        //public async Task<Tuple<Response<UpdateProfileResponse>, ErrorResponse, int>> UpdateCustomerProfileAsync(UpdateProfileRequest request, string origin)
        public async Task<Response<UpdateProfileResponse>> UpdateCustomerProfileAsync(UpdateProfileRequest request, string origin)
        {
            UpdateProfileResponse response = new UpdateProfileResponse();

            try
            {
                var existingUser = await _userManager.FindByNameAsync(request.PhoneNumber);

                if (existingUser == null)
                {
                    throw new ApiException($"User not found.");
                }

                existingUser.FirstName = request.FirstName;
                existingUser.LastName = request.LastName;
                existingUser.PhoneNumber = request.PhoneNumber;
                existingUser.TownId = Guid.Parse(request.LocationLevelId);
                existingUser.UserName = request.PhoneNumber;

                var result = await _userManager.UpdateAsync(existingUser);

                //Send Email Confirmation

                if (result.Succeeded)
                {
                    return new Response<UpdateProfileResponse>(response, responsestatus: ResponseStatus.success.ToString(), message: $"User profile successfully updated.");
                }
                else
                {
                    throw new ApiException($"{string.Join(", ", result.Errors.Select(x => x.Description))}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }

        //For staff.
        public async Task<Response<StaffRegistrationResponse>> RegisterStaffAsync(StaffRegisterRequest request, string origin)
        {
            try
            {
                var userWithSameUserName = await _userManager.FindByEmailAsync(request.Email);
                if (userWithSameUserName != null)
                {
                    // throw new ApiException($"Username '{request.Email}' is already taken.");

                    StaffRegistrationResponse response = new StaffRegistrationResponse { Message = "Account already exists.", UserAlreadyExists = true };

                    return new Response<StaffRegistrationResponse>(response, responsestatus: ResponseStatus.success.ToString(), message: $"User is already registered, Kindly login with the associated details. Thanks.");
                }

                string defaultPassword = _randomNumberGenerator.GenerateRandomNumber(6, Mode.AlphaNumeric);

                var user = new ApplicationUser
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserName = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    PasswordHash = defaultPassword
                };

                var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);

                if (userWithSameEmail == null)
                {
                    var result = await _userManager.CreateAsync(user, request.Password);

                    if (result.Succeeded)
                    {
                        //add to all roles

                        foreach (var roleId in request.RoleIds)
                        {
                            var role = await _roleManager.FindByIdAsync(roleId);

                            await _userManager.AddToRoleAsync(user, role.Name);

                        }
                        var verificationUri = await GetVerificationUri(user, origin);


                        //TODO: Attach Email Service here and configure it via appsettings
                        await _emailService.SendAsync(new Application.DTOs.Email.EmailRequest()
                        {
                            From = "admin@vigilanteng.com",
                            To = user.Email,
                            BodyParagraph1 = $"Hello, {user.FirstName}.",
                            BodyParagraph2 = $"Please confirm your account by visiting this URL {verificationUri}",
                            Subject = "Email Set Up",
                            Heading = "Email Setup Successful"
                        });

                        StaffRegistrationResponse response = new StaffRegistrationResponse { Message = "Staff account created.", VerificationUrl = verificationUri, UserAlreadyExists = false };
                        return new Response<StaffRegistrationResponse>(response, responsestatus: ResponseStatus.success.ToString(), message: $"Staff account was registered successfully. {verificationUri}");

                    }
                    else
                    {
                        //throw new ApiException($"{result.Errors}"); (Original)

                        // throw new ApiException($"{string.Join(", ", result.Errors.Select(x => "Code " + x.Code + " Description" + x.Description))}"); 

                        throw new ApiException($"{string.Join(", ", result.Errors.Select(x => x.Description))}");
                    }
                }
                else
                {
                    StaffRegistrationResponse response = new StaffRegistrationResponse { Message = "Account already exists.", UserAlreadyExists = true };

                    return new Response<StaffRegistrationResponse>(response, responsestatus: ResponseStatus.fail.ToString(), message: $"User is already registered, Kindly login with the associated details. Thanks.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            try
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                var roleClaims = new List<Claim>();

                for (int i = 0; i < roles.Count; i++)
                {
                    roleClaims.Add(new Claim("roles", roles[i]));
                }

                string ipAddress = IpHelper.GetIpAddress();

                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", ipAddress)
                }
                .Union(userClaims)
                .Union(roleClaims);

                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                    signingCredentials: signingCredentials);
                return jwtSecurityToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private async Task<string> GetVerificationUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/account/confirm-email/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route), UriKind.RelativeOrAbsolute);
            var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            //Email Service Call Here
            return verificationUri;
        }

        public async Task<Response<string>> ConfirmEmailAsync(string userId, string code)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(userId);
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                var result = await _userManager.ConfirmEmailAsync(user, code);
                if (result.Succeeded)
                {
                    return new Response<string>(user.Id, message: $"{user.FirstName}, Thank you for confirming your email address, {user.Email}.");
                }
                else
                {
                    throw new ApiException($"An error occured while confirming {user.Email}.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }

        }

        private RefreshToken GenerateRefreshToken(string ipAddress = null)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress == null ? null : ipAddress
            };
        }

        public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            try
            {
                var account = await _userManager.FindByEmailAsync(model.Email);

                // always return ok response to prevent email enumeration
                if (account == null) return;

                var code = await _userManager.GeneratePasswordResetTokenAsync(account);
                var route = "api/account/reset-password/";
                var _enpointUri = new Uri(string.Concat($"{origin}/", route));
                var emailRequest = new EmailRequest()
                {
                    Body = $"You reset token is - {code}",
                    To = model.Email,
                    Subject = "Reset Password",
                };
                await _emailService.SendAsync(emailRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }

        public async Task<Response<string>> ResetPassword(ResetPasswordRequest model)
        {
            try
            {
                var account = await _userManager.FindByEmailAsync(model.Email);
                if (account == null) throw new ApiException($"No Accounts Registered with {model.Email}.");
                var result = await _userManager.ResetPasswordAsync(account, model.Token, model.Password);
                if (result.Succeeded)
                {
                    return new Response<string>(model.Email, message: $"Password Reset.");
                }
                else
                {
                    throw new ApiException($"Error occured while reseting the password.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }

        // refresh refresh token 
        public async Task<Response<AuthenticationResponse>> RefreshTokenAsync(string token)
        {
            try
            {
                var authenticationModel = new AuthenticationResponse();

                var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == token);

                if (user == null)
                {
                    //authenticationModel.IsVerified = false;
                    //authenticationModel.Message = $"Token did not match any users.";
                    //return new Response<AuthenticationResponse>(authenticationModel, $"Token did not match any users.");

                    throw new ApiException($"Token did not match any users");
                }

                var refreshToken = user.RefreshToken;

                if (user.RefreshTokenExpiry < DateTime.UtcNow.AddHours(1))
                {
                    throw new ApiException($"Refresh Token is Inactive. Please login again.");
                }

                //Revoke Current Refresh Token

                //Generate new Refresh Token and save to Database
                var newRefreshToken = GenerateRefreshToken();
                user.RefreshToken = newRefreshToken.Token;
                user.RefreshTokenExpiry = newRefreshToken.Expires;

                _context.Update(user);
                _context.SaveChanges();

                //Generates new jwt
                authenticationModel.IsVerified = true;
                JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
                authenticationModel.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();
                authenticationModel.RefreshToken = newRefreshToken.Token;
                authenticationModel.RefreshTokenExpiration = newRefreshToken.Expires;
                return new Response<AuthenticationResponse>(authenticationModel, $"New refresh token generated.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }

        //revoke token
        public bool RevokeToken(string token)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(u => u.RefreshToken == token);

                // return false if no user found with token
                if (user == null) return false;

                var refreshToken = user.RefreshToken;

                // return false if token is not active
                if (user.RefreshTokenExpiry < DateTime.UtcNow.AddHours(1)) return false;

                // revoke token and save
                user.RefreshTokenExpiry = DateTime.UtcNow;

                _context.Update(user);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // add-role

        //public async Task<string> AddRoleAsync(AddRoleModel model)
        //{
        //    var user = await _userManager.FindByEmailAsync(model.Email);

        //    if (user == null)
        //    {
        //        return $"No Accounts Registered with {model.Email}.";
        //    }

        //    var roleExists = Enum.GetNames(typeof(Roles)).Any(x => x.ToLower() == model.Role.ToLower());
        //    if (roleExists)
        //    {
        //        var validRole = Enum.GetValues(typeof(Roles)).Cast<Roles>().Where(x => x.ToString().ToLower() == model.Role.ToLower()).FirstOrDefault();
        //        await _userManager.AddToRoleAsync(user, validRole.ToString());
        //        return $"Added {model.Role} to user {model.Email}.";
        //    }
        //    return $"Role {model.Role} not found.";

        //}

        public ApplicationUser GetById(string id)
        {
            try
            {
                return _context.Users.Find(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }

    }

}
