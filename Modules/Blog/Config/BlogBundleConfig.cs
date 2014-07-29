using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.Blog.Config
{
    public class BlogBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/Blog")
                .Include("~/Modules/Blog/Scripts/example.js",
                         "~/Modules/Blog/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/Blog")
                .Include("~/Modules/Blog/Styles/example.css",
                         "~/Modules/Blog/Styles/example-index.css"));
        }
    }
}