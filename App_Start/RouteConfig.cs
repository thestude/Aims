using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using AIMS.Config.Routes;

namespace AIMS
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();

            var instances = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                            from t in assembly.GetTypes()
                            //from t in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                            where t.GetInterfaces().Contains(typeof(ICustomRouteConfig))
                                     && t.GetConstructor(Type.EmptyTypes) != null
                            select Activator.CreateInstance(t) as ICustomRouteConfig;

            foreach (var instance in instances)
            {
                instance.RegisterRoutes(routes);
            }

            var defaultRoute = new DefaultRouteConfig();

            defaultRoute.RegisterRoutes(routes);
        }
        
    }
}
