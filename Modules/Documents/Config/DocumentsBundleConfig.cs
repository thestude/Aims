using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.Documents.Config
{
    public class DocumentsBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/Documents")
                .Include("~/Modules/Documents/Scripts/example.js",
                         "~/Modules/Documents/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/Documents")
                .Include("~/Modules/Documents/Styles/example.css",
                         "~/Modules/Documents/Styles/example-index.css"));
        }
    }
}