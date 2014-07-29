using System.Web.Optimization;

namespace AIMS.Config.Bundles
{
    public class DefaultBundleConfig : ICustomBundleConfig
    {
        public void RegisterBundles(BundleCollection bundles)
        {
            //Javascript Bundles
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js"
                        ));

            // TODO: break into module specific
            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/jquery.slimscroll.js",
                        "~/Scripts/jquery.cookie.js",

                        "~/Scripts/flot/jquery.flot.js",
                        "~/Scripts/flot/jquery.flot.resize.js",
                        "~/Scripts/flot/jquery.flot.pie.js",
                        "~/Scripts/flot/jquery.flot.stack.js",
                        "~/Scripts/flot/jquery.flot.crosshair.js",
                        "~/Scripts/flot.tooltip.js", //https://github.com/krzysu/flot.tooltip
                        "~/Scripts/jquery.sparkline.js", //https://github.com/gwatts/jquery.sparkline
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/daterangepicker.js",
                        "~/Scripts/wysihtml5/wysihtml5-0.3.0.js",
                        "~/Scripts/bootstrap-wysihtml5.js",
                        "~/Scripts/bootstrap-select.js",
                        "~/Scripts/bootstrap-switch.js",

                        // Flaty
                        "~/Scripts/Flaty/flaty.js",
                        "~/Scripts/Flaty/flaty-demo-codes.js",

                        // Site
                        "~/Scripts/Site/gmaps.js",
                        "~/Scripts/Site/front.js",
                        "~/Scripts/Site/nav-activate.js",
                        "~/Scripts/Site/status-update-screen-scripts.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernize")
                .Include("~/Scripts/respond.js",
                        "~/Scripts/modernizr-{version}.js"));
            //test
            //Styling Bundles
            bundles.Add(new StyleBundle("~/Content/css")
                .Include(
                        // base css styles
                        "~/Content/bootstrap.css", 
                        "~/Content/font-awesome.css",

                        // page specific css styles TODO: Later move to specific modules
                        "~/Content/bootstrap-chosen",
                        "~/Content/Site/chosen-bootstrap-3.css",
                        "~/Content/bootstrap-select.css",
                        "~/Content/bootstrap.datatables.css",
                        "~/Content/bootstrap-wysihtml5.css",

                        // flaty css styles
                        "~/Content/bootstrap-datepicker3.css",
                        "~/Content/daterangepicker-bs3.css",
                        "~/Content/bootstrap-switch/bootstrap3/bootstrap-switch.css",
                        "~/Content/Flatty/flaty.css",
                        "~/Content/Site/docs.css",

                        // aims css styles
                        "~/Content/Site/front.css",

                        // responsive overides
                        "~/Content/Flatty/flaty-responsive.css",
                        "~/Content/Site/front-responsive.css",
                        "~/Content/site.css"));

            //Module Bundles
            //Javascript
            bundles.Add(new ScriptBundle("~/bundles/js/modules"));

            //Styles
            bundles.Add(new StyleBundle("~/bundles/css/modules"));

        }
    }
}