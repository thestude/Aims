using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Web;
using NHibernate;
using NHibernate.Context;
using NHibernate.Engine;

namespace AIMS.Infrastructure.NHibernate
{
    public class LazySessionContext : ICurrentSessionContext
    {
        private readonly ISessionFactoryImplementor _factory;
        private const string CurrentSessionContextKey = "NHibernateCurrentSession";

        public LazySessionContext(ISessionFactoryImplementor factory)
        {
            this._factory = factory;
        }

        /// <summary>
        /// Retrieve the current session for the session factory.
        /// </summary>
        /// <returns></returns>
        public ISession CurrentSession()
        {
            Lazy<ISession> initializer;
            var currentSessionFactoryMap = GetCurrentFactoryMap();
            if (currentSessionFactoryMap == null ||
                !currentSessionFactoryMap.TryGetValue(this._factory, out initializer))
            {
                return null;
            }
            return initializer.Value;
        }

        /// <summary>
        /// Bind a new sessionInitializer to the context of the sessionFactory.
        /// </summary>
        /// <param name="sessionInitializer"></param>
        /// <param name="sessionFactory"></param>
        public static void Bind(Lazy<ISession> sessionInitializer, ISessionFactory sessionFactory)
        {
            var map = GetCurrentFactoryMap();
            map[sessionFactory] = sessionInitializer;
        }

        /// <summary>
        /// Unbind the current session of the session factory.
        /// </summary>
        /// <param name="sessionFactory"></param>
        /// <returns></returns>
        public static ISession UnBind(ISessionFactory sessionFactory)
        {
            Lazy<ISession> sessionInitializer;
            var map = GetCurrentFactoryMap();

            if (!map.TryGetValue(sessionFactory, out sessionInitializer))
            {
                sessionInitializer = null;
            }

            map[sessionFactory] = null;

            if (sessionInitializer == null || !sessionInitializer.IsValueCreated) return null;
            return sessionInitializer.Value;
        }

        /// <summary>
        /// Provides the CurrentMap of SessionFactories.
        /// If there is no map create/store and return a new one.
        /// </summary>
        /// <returns></returns>
        private static IDictionary<ISessionFactory, Lazy<ISession>> GetCurrentFactoryMap()
        {
            var currentFactoryMap = FactoryMapInContext;
            if (currentFactoryMap == null)
            {
                currentFactoryMap = new Dictionary<ISessionFactory, Lazy<ISession>>();
                FactoryMapInContext = currentFactoryMap;
            }
            return currentFactoryMap;
        }

        private static IDictionary<ISessionFactory, Lazy<ISession>> FactoryMapInContext
        {
            get
            {
                if (IsInWebContext())
                {
                    return HttpContext.Current.Items[CurrentSessionContextKey] as IDictionary<ISessionFactory, Lazy<ISession>>;
                }
                return CallContext.GetData(CurrentSessionContextKey) as IDictionary<ISessionFactory, Lazy<ISession>>;
            }
            set
            {
                if (IsInWebContext())
                {
                    HttpContext.Current.Items[CurrentSessionContextKey] = value;
                }
                else
                {
                    CallContext.SetData(CurrentSessionContextKey, value);
                }
            }
        }

        private static bool IsInWebContext()
        {
            return HttpContext.Current != null;
        }
    }
}