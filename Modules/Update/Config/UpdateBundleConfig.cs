using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.Update.Config
{
    public class UpdateBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/Update")
                .Include("~/Modules/Update/Scripts/example.js",
                         "~/Modules/Update/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/Update")
                .Include("~/Modules/Update/Styles/example.css",
                         "~/Modules/Update/Styles/example-index.css"));
        }
    }
}