using System.Web.Mvc;
using System.Web.Routing;

namespace AIMS.Config.Routes
{
    public class DefaultRouteConfig 
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

          // routes.MapMvcAttributeRoutes();

            var route = routes.MapRoute(
                            name: "Default",
                            url: "{controller}/{action}/{id}",
                            defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional },
                            namespaces: new[] { "AIMS.Controllers" }
                        );

            route.DataTokens["UseNamespaceFallback"] = false;

        }
    }
}