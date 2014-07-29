using AIMS.Models;
using Autofac;
using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Nh;
using BrockAllen.MembershipReboot.Nh.Repository;
using BrockAllen.MembershipReboot.WebHost;

namespace AIMS.Infrastructure.AutoFac
{
    public class MembershipRebootModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(UserAccountService<>)).AsSelf();
            builder.RegisterGeneric(typeof(NhRepository<>)).As(typeof(IRepository<>));
            builder.RegisterGeneric(typeof(NhUserAccountRepository<>)).As(typeof(IUserAccountRepository<>));
            builder.RegisterType(typeof(SamAuthenticationService<AimsUser>)).AsSelf().As(typeof(AuthenticationService<AimsUser>));
            builder.RegisterGeneric(typeof(NhGroupRepository<>)).As(typeof(IGroupRepository<>), typeof(QueryableGroupRepository<>));
            builder.RegisterInstance(MembershipRebootConfig.Create()).As<MembershipRebootConfiguration<AimsUser>>().SingleInstance();
            builder.Register(context => new GroupService<NhGroup>("default", context.Resolve<IGroupRepository<NhGroup>>())).AsSelf();

            //builder.Register(c => MembershipRebootConfig.Create()).AsSelf().As(typeof(MembershipRebootConfiguration<AimsUser>)).SingleInstance();
            //builder.RegisterInstance(MembershipRebootConfig.Create()).As<MembershipRebootConfiguration<AimsUser>>().ExternallyOwned().SingleInstance();
        }
    }
}