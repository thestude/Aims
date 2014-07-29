using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.DashBoard.Config
{
    public class DashBoardBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/DashBoard")
                .Include("~/Modules/DashBoard/Scripts/mapping.js",
                        "~/Modules/DashBoard/Scripts/example.js",
                         "~/Modules/DashBoard/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/DashBoard")
                .Include("~/Modules/DashBoard/Styles/example.css",
                         "~/Modules/DashBoard/Styles/example-index.css"));
        }
    }
}