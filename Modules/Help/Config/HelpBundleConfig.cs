using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.Help.Config
{
    public class HelpBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/Help")
                .Include("~/Modules/Help/Scripts/example.js",
                         "~/Modules/Help/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/Help")
                .Include("~/Modules/Help/Styles/example.css",
                         "~/Modules/Help/Styles/example-index.css"));
        }
    }
}