using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;


    namespace CarShop.App_Start
    {
        public class BundleConfig
        {
            public static void RegisterBundles(BundleCollection bundles)
            {
            // Бандл JS
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/js/bootstrap.bundle.min.js",
                        "~/Scripts/jquery-3.7.1.js")); 

            // Бандл CSS
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/css/bootstrap-reboot.min.css",
                        "~/Content/css/bootstrap.min.css",
                        "~/Content/site.css",
                        "~/Content/MyStyle.css"));

            BundleTable.EnableOptimizations = true;
        }
        }
    }