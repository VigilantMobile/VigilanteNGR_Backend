using Application.Interfaces;
using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repository;
using Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Geometries;
using Microsoft.AspNetCore.Identity;
using Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Application.Wrappers;

namespace Infrastructure.Persistence
{
    public static class IdentityServiceRegistration
    {
        //public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        //{

           


        //    //Authorization
        //    //services.AddAuthorization(options =>
        //    //{
        //    //    // VGNGA
        //    //    // ROLE POLICIES
        //    //    //options.AddPolicy("SuperAdminAccess", policy => policy.RequireRole("SuperAdmin"));
        //    //    options.AddPolicy("AdminAccess", policy => policy.RequireRole("Admin"));


        //    //    options.AddPolicy("AllAdminsAccess", policy =>
        //    //        policy.RequireAssertion(context =>
        //    //                    context.User.IsInRole("Admin")
        //    //                    || context.User.IsInRole("SuperAdmin")));


        //    //    options.AddPolicy("ModeratorAccess", policy =>
        //    //        policy.RequireAssertion(context =>
        //    //                    context.User.IsInRole("Moderator")
        //    //                    || context.User.IsInRole("Admin")
        //    //                    || context.User.IsInRole("SuperAdmin")));

        //    //    options.AddPolicy("VGNGAOperatorAccess", policy => policy.RequireRole("VGNGAOperator"));


        //    //    //NPF
        //    //    options.AddPolicy("NPFAdminAccess", policy => policy.RequireRole("NPFAdmin"));

        //    //    options.AddPolicy("NPFOperatorAccess", policy => policy.RequireRole("NPFOperator"));


        //    //    options.AddPolicy("BasicUserAccess", policy => policy.RequireRole("Basic"));

        //    //    //CLAIM POLICIES
        //    //    options.AddPolicy("SupereriorAdminAccess", policy => policy.RequireClaim("SuperAccess"));
        //    //    options.AddPolicy("CreateStaffAccess", policy => policy.RequireClaim("CanCreateStaff"));

        //    //});

        //}
    }
}
