using System.Web.Optimization;

namespace DailyEZ.Web.Code
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            BundleTable.EnableOptimizations = true;
            bundles.UseCdn = true;
            //jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/js/vendor/jquery-{version}.js"));

            //jquery ui cdn path
            const string jqueryUiCdn = "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js";
            const string jqueryUiCssCdn = "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/themes/redmond/jquery-ui.min.css";
            bundles.Add(new ScriptBundle("~/bundles/jqueryui",jqueryUiCdn)
                .Include(
                "~/js/vendor/jquery.ui.core.js",
                "~/js/vendor/jquery.ui.widget.js",
                "~/js/vendor/jquery.ui.dialog.js",
                "~/js/vendor/jquery.ui.position.js"
                )
                );
            //third party libraries and plugins
            bundles.Add(new ScriptBundle("~/bundles/jsextlibs")
                .Include(
                "~/js/vendor/bootstrap.js", //bootstrap
                //jquery plugins
                "~/js/vendor/jquery.ba-bbq.js",
                //toastr
                "~/js/vendor/toastr.js"
                ));

            //all application js
            bundles.Add(new ScriptBundle("~/bundles/jsapplibs")
                .IncludeDirectory("~/js/app/", "*.js", searchSubdirectories: false));

            //modernizr in seperate bundles since it's loaded first
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/js/vendor/modernizr-{version}.js"));


            //css bundles
            bundles.Add(new StyleBundle("~/Content/css").IncludeDirectory("~/css/", "*.css", searchSubdirectories: false));

            bundles.Add(new StyleBundle("~/Content/css/jqueryui",jqueryUiCssCdn).Include("~/css/vendor/jquery.ui.all.css"));
            


            //for default aspx
            bundles.Add(new ScriptBundle("~/bundles/stackInitializer").Include());
         
        }
    }
}