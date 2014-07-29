using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.FormResponse.Config
{
    public class FormResponseBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/FormResponse")
                .Include("~/Modules/FormResponse/Scripts/example.js",
                         "~/Modules/FormResponse/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/FormResponse")
                .Include("~/Modules/FormResponse/Styles/example.css",
                         "~/Modules/FormResponse/Styles/example-index.css"));
        }
    }
}