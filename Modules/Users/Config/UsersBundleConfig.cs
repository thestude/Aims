using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.Users.Config
{
    public class UsersBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/Users")
                .Include("~/Modules/Users/Scripts/example.js",
                         "~/Modules/Users/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/Users")
                .Include("~/Modules/Users/Styles/example.css",
                         "~/Modules/Users/Styles/example-index.css"));
        }
    }
}