using System.Web;
using System.Web.Mvc;

namespace AIMS
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // TODO: Remove on release FOR TESTING PURPOSES ONLY. Only allow authorize when in debug mode.
#if DEBUG
#else
            filters.Add(new AuthorizeAttribute());
#endif
            filters.Add(new HandleErrorAttribute());
        }
    }
}
