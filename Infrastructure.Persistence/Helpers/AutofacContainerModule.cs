using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Application.Services.Interfaces;
using Application.Services.Interfaces.AppTroopers.SecurityTips;
using Application.Services.Interfaces.Location;
using Application.Services.Interfaces.UserProfile;
using Autofac;
using Infrastructure.Persistence.Services;
using Infrastructure.Persistence.Services.Implementations.AppTroopers.SecurityTips;
using Infrastructure.Persistence.Services.Implementations.Location;
using Infrastructure.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Helpers
{
    public class AutofacContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(typeof(IAutoDependencyService).Assembly)
            .AssignableTo<IAutoDependencyService>()
            .As<IAutoDependencyService>()
            .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<MailgunEmailService>().As<IMailgunEmailService>();
            //builder.RegisterType<MailgunEmailService>().As<IMailgunEmailService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
            builder.RegisterType<SecurityTipEligibilityService>().As<ISecurityTipEligibilityService>().InstancePerLifetimeScope();
            builder.RegisterType<SecurityTipService>().As<ISecurityTipService>().InstancePerLifetimeScope();
            builder.RegisterType<GeocodingService>().As<IGeoCodingService>().InstancePerLifetimeScope();
            builder.RegisterType<Utilities>().As<IUtilities>().InstancePerLifetimeScope();
            
        }
    }
}
