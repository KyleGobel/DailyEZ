using System.Web.Optimization;

namespace DailyEZ.Web.Code
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            //BundleTable.EnableOptimizations = true;

            //jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/js/vendor/jquery-{version}.js"));

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


            //for default aspx
            bundles.Add(new ScriptBundle("~/bundles/stackInitializer").Include());
         
        }
    }
}