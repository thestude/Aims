using AIMS.Infrastructure.NHibernate;
using Autofac;
using NHibernate;

namespace AIMS.Infrastructure.AutoFac
{
    public class NHibernateModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new SessionProvider().CreateInstance(c)).As<ISession>().InstancePerRequest();
            builder.Register(c => new SessionFactoryProvider().CreateInstance(c)).As<ISessionFactory>().SingleInstance();
            builder.Register(c => new StatelessSessionProvider().CreateInstance(c)).As<IStatelessSession>().InstancePerRequest();
        }
    }

    public class SessionProvider
    {
        public ISession CreateInstance(IComponentContext context)
        {
            var sf = context.Resolve<ISessionFactory>();
            var sn = sf.GetCurrentSession();// ?? sf.GetCurrentSession();
            return sn;
        }
    }

    public class StatelessSessionProvider
    {
        public IStatelessSession CreateInstance(IComponentContext context)
        {
            var sf = context.Resolve<ISessionFactory>();
            return sf.OpenStatelessSession();
        }
    }

    public class SessionFactoryProvider
    {
        public ISessionFactory CreateInstance(IComponentContext context)
        {
            var sf = new FluentConfigurator().BuildNhConfiguration().Configuration.BuildSessionFactory();
            return sf;
        }
    }
}