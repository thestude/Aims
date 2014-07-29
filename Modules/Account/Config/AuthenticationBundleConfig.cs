using AIMS.Config.Bundles;
using System.Web.Optimization;

namespace AIMS.Modules.Account.Config
{
    public class AuthenticationBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/Authentication")
                .Include("~/Modules/Authentication/Scripts/example.js",
                         "~/Modules/Authentication/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/Authentication")
                .Include("~/Modules/Authentication/Styles/example.css",
                         "~/Modules/Authentication/Styles/example-index.css"));
        }
    }
}