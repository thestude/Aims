using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.GeneralRequest.Config
{
    public class GeneralRequestBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/GeneralRequest")
                .Include("~/Modules/GeneralRequest/Scripts/example.js",
                         "~/Modules/GeneralRequest/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/GeneralRequest")
                .Include("~/Modules/GeneralRequest/Styles/example.css",
                         "~/Modules/GeneralRequest/Styles/example-index.css"));
        }
    }
}