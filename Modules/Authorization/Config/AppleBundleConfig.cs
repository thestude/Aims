using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.Authorization.Config
{
    public class AuthorizationBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/Apple")
                .Include("~/Modules/Authorization/Scripts/example.js",
                         "~/Modules/Authorization/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/Apple")
                .Include("~/Modules/Authorization/Styles/example.css",
                         "~/Modules/Authorization/Styles/example-index.css"));
        }
    }
}