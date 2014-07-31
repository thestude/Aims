using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.StatusUpdate.Config
{
    public class StatusUpdateBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/StatusUpdate")
                .Include(
                        "~/Scripts/knockout-{version}.js",
                        "~/Modules/StatusUpdate/Scripts/statusupdate.debug.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/StatusUpdate"));
        }
    }
}