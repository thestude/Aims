using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.PeopleSearch.Config
{
    public class PeopleSearchBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/PeopleSearch")
                .Include("~/Modules/PeopleSearch/Scripts/example.js",
                         "~/Modules/PeopleSearch/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/PeopleSearch")
                .Include("~/Modules/PeopleSearch/Styles/example.css",
                         "~/Modules/PeopleSearch/Styles/example-index.css"));
        }
    }
}