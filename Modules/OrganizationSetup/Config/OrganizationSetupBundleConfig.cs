using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.OrganizationSetup.Config
{
    public class OrganizationSetupBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/OrganizationSetup").Include(
                        "~/Modules/OrganizationSetup/Scripts/organizationsetup.debug.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/OrganizationSetup"));
        }
    }
}