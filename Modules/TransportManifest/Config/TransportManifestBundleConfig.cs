using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.TransportManifest.Config
{
    public class TransportManifestBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/TransportManifest")
                .Include("~/Modules/TransportManifest/Scripts/example.js",
                         "~/Modules/TransportManifest/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/TransportManifest")
                .Include("~/Modules/TransportManifest/Styles/example.css",
                         "~/Modules/TransportManifest/Styles/example-index.css"));
        }
    }
}