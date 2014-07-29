using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.MessageInbox.Config
{
    public class MessageInboxBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/MessageInbox")
                .Include("~/Modules/MessageInbox/Scripts/example.js",
                         "~/Modules/MessageInbox/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/MessageInbox")
                .Include("~/Modules/MessageInbox/Styles/example.css",
                         "~/Modules/MessageInbox/Styles/example-index.css"));
        }
    }
}