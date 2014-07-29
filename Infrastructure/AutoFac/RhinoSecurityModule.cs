using Autofac;
using Rhino.Security.Interfaces;
using Rhino.Security.Services;

namespace AIMS.Infrastructure.AutoFac
{
    public class RhinoSecurityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthorizationService>().As<IAuthorizationService>().InstancePerDependency();
            builder.RegisterType<AuthorizationRepository>().As<IAuthorizationRepository>().InstancePerDependency();
            builder.RegisterType<PermissionsBuilderService>().As<IPermissionsBuilderService>().InstancePerDependency();
            builder.RegisterType<PermissionsService>().As<IPermissionsService>().InstancePerDependency();
        }
    }
}