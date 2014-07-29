using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.Links.Config
{
    public class LinksBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/Links")
                .Include("~/Modules/Links/Scripts/example.js",
                         "~/Modules/Links/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/Links")
                .Include("~/Modules/Links/Styles/example.css",
                         "~/Modules/Links/Styles/example-index.css"));
        }
    }
}