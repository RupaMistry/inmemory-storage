using System.Web;
using System.Web.Optimization;

namespace Tranquillity.InMemory.Storage.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            RegisterStyleBundles(bundles);
            RegisterScriptBundles(bundles);
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/bootstrap.css",
                     "~/Content/site.css"));
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/AngularJS").Include(
               "~/Scripts/angular.min.js",
               "~/Scripts/angular-route.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/KeyValueStoreDashboard").Include(

                "~/Scripts/KeyValueStore/KeyValueStoreModule.js",
                 "~/Scripts/KeyValueStore/ApiConstants.js",
                 "~/Scripts/KeyValueStore/KeyValueStoreService.js",
                "~/Scripts/KeyValueStore/KeyValueStoreController.js"
         ));
        }
    }
}