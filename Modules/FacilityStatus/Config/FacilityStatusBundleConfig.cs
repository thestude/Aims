using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.FacilityStatus.Config
{
    public class FacilityStatusBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/FacilityStatus")
                .Include("~/Modules/FacilityStatus/Scripts/example.js",
                         "~/Modules/FacilityStatus/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/FacilityStatus")
                .Include("~/Modules/FacilityStatus/Styles/example.css",
                         "~/Modules/FacilityStatus/Styles/example-index.css"));
        }
    }
}