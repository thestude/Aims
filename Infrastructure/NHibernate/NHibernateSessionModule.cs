using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;

namespace AIMS.Infrastructure.NHibernate
{
    public class NHibernateSessionModule : IHttpModule
    {
        private HttpApplication _httpApp;

        public void Init(HttpApplication application)
        {
            _httpApp = application;
            _httpApp.BeginRequest += ContextBeginRequest;
            _httpApp.EndRequest += ContextEndRequest;
            _httpApp.Error += ContextError;
        }

        private void ContextBeginRequest(object sender, EventArgs e)
        {
            foreach (var sf in GetSessionFactories())
            {
                var localFactory = sf;
                LazySessionContext.Bind(new Lazy<ISession>(() => BeginSession(localFactory)), sf);
            }
        }

        private static ISession BeginSession(ISessionFactory sf)
        {
            var session = sf.OpenSession();
            session.BeginTransaction();
            return session;
        }

        private void ContextEndRequest(object sender, EventArgs e)
        {
            var sessionsToEnd = GetSessionFactories()
                .Select(LazySessionContext.UnBind)
                .Where(session => session != null);

            foreach (var session in sessionsToEnd)
            {
                EndSession(session);
            }
        }

        private void ContextError(object sender, EventArgs e)
        {
            var sessionstoAbort = GetSessionFactories()
                .Select(LazySessionContext.UnBind)
                .Where(session => session != null);

            foreach (var session in sessionstoAbort)
            {
                EndSession(session, true);
            }
        }

        private static void EndSession(ISession session, bool abort = false)
        {
            if (session.Transaction != null && session.Transaction.IsActive)
            {
                if (abort)
                {
                    session.Transaction.Rollback();
                }
                else
                {
                    session.Transaction.Commit();
                }
            }
            session.Dispose();
        }

        public void Dispose()
        {
            //_httpApp.BeginRequest -= ContextBeginRequest;
            //_httpApp.EndRequest -= ContextEndRequest;
            //_httpApp.Error -= ContextError;
        }

        /// <summary>
        /// Retrieves all ISessionFactory instances via IoC
        /// </summary>
        private IEnumerable<ISessionFactory> GetSessionFactories()
        {
            //Get all ISessionFactory instances using Autofac
            var sessionFactories = DependencyResolver.Current.GetServices<ISessionFactory>();
            //var sessionFactories = NinjectWebCommon.Container.GetAll<ISessionFactory>();

            var sFactories = sessionFactories as ISessionFactory[] ?? sessionFactories.ToArray();
            if (sessionFactories == null || !sFactories.Any())
                throw new TypeLoadException("No ISessionFactory has been registered with IoC");

            return sFactories;
        }
    }
}