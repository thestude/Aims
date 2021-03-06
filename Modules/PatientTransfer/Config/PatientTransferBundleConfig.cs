﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using AIMS.Config.Bundles;

namespace AIMS.Modules.PatientTransfer.Config
{
    public class PatientTransferBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
           //Module-wide script bundle
            bundles.Add(new ScriptBundle("~/bundles/js/modules/PatientTransfer")
                .Include("~/Modules/PatientTransfer/Scripts/example.js",
                         "~/Modules/PatientTransfer/Scripts/example-index.js"));

            //Module-wide style bundle
            bundles.Add(new StyleBundle("~/bundles/css/modules/PatientTransfer")
                .Include("~/Modules/PatientTransfer/Styles/example.css",
                         "~/Modules/PatientTransfer/Styles/example-index.css"));
        }
    }
}