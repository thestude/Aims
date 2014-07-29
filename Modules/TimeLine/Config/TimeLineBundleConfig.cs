using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.Timeline.Config
{
    public class TimeLineBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/TimeLine")
                .Include("~/Modules/TimeLine/Scripts/example.js",
                         "~/Modules/TimeLine/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/TimeLine")
                .Include("~/Modules/TimeLine/Styles/example.css",
                         "~/Modules/TimeLine/Styles/example-index.css"));
        }
    }
}