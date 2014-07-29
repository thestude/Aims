using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.HAvBED.Config
{
    public class HAvBEDBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/HAvBED")
                .Include("~/Modules/HAvBED/Scripts/example.js",
                         "~/Modules/HAvBED/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/HAvBED")
                .Include("~/Modules/HAvBED/Styles/example.css",
                         "~/Modules/HAvBED/Styles/example-index.css"));
        }
    }
}