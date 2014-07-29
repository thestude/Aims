using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.DashBoard.Config
{
    public class FacilitySetupBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/FacilitySetup"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/FacilitySetup"));
        }
    }
}