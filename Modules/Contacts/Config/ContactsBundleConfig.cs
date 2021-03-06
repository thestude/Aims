﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.Contacts.Config
{
    public class ContactsBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/Contacts")
                .Include("~/Modules/Contacts/Scripts/example.js",
                         "~/Modules/Contacts/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/Contacts")
                .Include("~/Modules/Contacts/Styles/example.css",
                         "~/Modules/Contacts/Styles/example-index.css"));
        }
    }
}