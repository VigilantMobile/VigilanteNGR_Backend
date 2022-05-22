using Application.Services.Interfaces;
using Autofac;

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

            //builder.RegisterType<ICustomerProfileService>().As<CustomerProfileService>()
            //   .InstancePerLifetimeScope();
        }
    }
}
