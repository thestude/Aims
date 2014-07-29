using System.Web.Routing;

namespace AIMS.Config.Routes
{
    public interface ICustomRouteConfig
    {
        void RegisterRoutes(RouteCollection routes);
    }
}