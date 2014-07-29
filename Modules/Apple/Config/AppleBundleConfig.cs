using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.DashBoard.Config
{
    public class AppleBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/Apple")
                .Include("~/Modules/Apple/Scripts/example.js",
                         "~/Modules/Apple/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/Apple")
                .Include("~/Modules/Apple/Styles/example.css",
                         "~/Modules/Apple/Styles/example-index.css"));
        }
    }
}