using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.AppTroopers.Panic;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Application.Interfaces.Repositories.Location;
using Application.Wrappers;
using Domain.Entities.Identity;
using Domain.Settings;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Location;
using Infrastructure.Persistence.Repositories.Panic;
using Infrastructure.Persistence.Repositories.SecurityTips;
using Infrastructure.Persistence.Repository;
using Infrastructure.Persistence.Services;
using Infrastructure.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Infrastructure.Persistence
{
    public static class ServiceRegistration
    {

        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            //{
            //    services.AddDbContext<IdentityContext>(options =>
            //        options.UseInMemoryDatabase("IdentityDb"));
            //}
            //else
            //{
            //    services.AddDbContext<IdentityContext>(options =>
            //    options.UseSqlServer(
            //        configuration.GetConnectionString("IdentityConnection"),
            //        b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
            //}
            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            {

                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
                opt.SignIn.RequireConfirmedPhoneNumber = false;

            })
            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            #region Services
            services.AddTransient<IAccountService, AccountService>();
            #endregion


            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.Configure<VGNGAEmailSenders>(configuration.GetSection("VGNGAEmailSenders"));
            services.Configure<APIURLs>(configuration.GetSection("APIURLs"));
            services.Configure<AppConfig>(configuration.GetSection("AppConfig"));

            //authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JWTSettings:Issuer"],
                        ValidAudience = configuration["JWTSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                    };
                    o.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = c =>
                        {
                            c.NoResult();
                            c.Response.StatusCode = 500;
                            c.Response.ContentType = "text/plain";
                            return c.Response.WriteAsync(c.Exception.ToString());
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new Response<string>("You are not Authorized"));
                            return context.Response.WriteAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new Response<string>("You are not authorized to access this resource"));
                            return context.Response.WriteAsync(result);
                        },
                    };
                });

            services.AddHttpContextAccessor();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));



            //Authorization
            //services.AddAuthorization(options =>
            //{
            //    // VGNGA
            //    // ROLE POLICIES
            //    //options.AddPolicy("SuperAdminAccess", policy => policy.RequireRole("SuperAdmin"));
            //    options.AddPolicy("AdminAccess", policy => policy.RequireRole("Admin"));


            //    options.AddPolicy("AllAdminsAccess", policy =>
            //        policy.RequireAssertion(context =>
            //                    context.User.IsInRole("Admin")
            //                    || context.User.IsInRole("SuperAdmin")));


            //    options.AddPolicy("ModeratorAccess", policy =>
            //        policy.RequireAssertion(context =>
            //                    context.User.IsInRole("Moderator")
            //                    || context.User.IsInRole("Admin")
            //                    || context.User.IsInRole("SuperAdmin")));

            //    options.AddPolicy("VGNGAOperatorAccess", policy => policy.RequireRole("VGNGAOperator"));


            //    //NPF
            //    options.AddPolicy("NPFAdminAccess", policy => policy.RequireRole("NPFAdmin"));

            //    options.AddPolicy("NPFOperatorAccess", policy => policy.RequireRole("NPFOperator"));


            //    options.AddPolicy("BasicUserAccess", policy => policy.RequireRole("Basic"));

            //    //CLAIM POLICIES
            //    options.AddPolicy("SupereriorAdminAccess", policy => policy.RequireClaim("SuperAccess"));
            //    options.AddPolicy("CreateStaffAccess", policy => policy.RequireClaim("CanCreateStaff"));

            //});

        }


        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                     b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName).UseNetTopologySuite()).EnableSensitiveDataLogging(true));

            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IProductRepositoryAsync, ProductRepositoryAsync>();
            //User Profile

            //App Troopers
            services.AddTransient<ISecurityTipCategoryRepositorysync, SecurityTipCategoryRepositoryAsync>();

            //Location
            services.AddTransient<ITownRepositoryAsync, TownRepositoryAsync>();
            services.AddTransient<ILGARepositoryAsync, LGARepositoryAsync>();
            services.AddTransient<IStateRepositoryAsync, StateRepositoryAsync>();

            //Panic
            services.AddTransient<ITrustedPersonRepositoryAsync, TrustedPersonRepositoryAsync>();

            #endregion

            #region DbInitializer
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddSingleton<IDateTimeService, DateTimeService>();
            #endregion
        }
    }
}
