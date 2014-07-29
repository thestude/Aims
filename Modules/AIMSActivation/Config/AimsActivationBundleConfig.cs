using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.AIMSActivation.Config
{
    public class AimsActivationBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/AimsActivation")
                .Include("~/Modules/AimsActivation/Scripts/example.js",
                         "~/Modules/AimsActivation/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/AimsActivation")
                .Include("~/Modules/AimsActivation/Styles/example.css",
                         "~/Modules/AimsActivation/Styles/example-index.css"));
        }
    }
}