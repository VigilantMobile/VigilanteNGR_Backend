using Application.Behaviours;
using Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
//using AutoMapper;
//using System.Reflection;

namespace Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());
            //services.AddAutoMapper();
            services.AddValidatorsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
            //services.AddMediatR(System.Reflection.Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient<IUserAccessor, UserAccessor>();
        }
    }
}
