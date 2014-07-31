using AIMS.Helpers;
using AIMS.Modules.OrganizationSetup.Services;
using AIMS.Infrastructure.Logging;
using AIMS.Modules.StatusUpdate.Services;
using Autofac;

namespace AIMS.Infrastructure.AutoFac
{
    public class SiteModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NLogLogger>().As<ILogger>().SingleInstance();
            builder.RegisterType<UserActivity>().As<IUserActivity>().InstancePerRequest();
            builder.RegisterType<UserInfoHelper>().As<IUserInfoHelper>().InstancePerDependency();
            builder.RegisterType<OrganizationSetupService>().As<IOrganizationSetupService>().InstancePerDependency();
            builder.RegisterType<StatusUpdateService>().As<IStatusUpdateService>().InstancePerDependency();
        }
    }
}