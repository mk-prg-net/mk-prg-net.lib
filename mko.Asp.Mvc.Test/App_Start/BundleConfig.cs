using System.Web;
using System.Web.Optimization;

namespace mko.Asp.Mvc.Test
{
    public class BundleConfig
    {
        // Weitere Informationen zu Bundling finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=254725".
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/methods_de.js"));

            // Verwenden Sie die Entwicklungsversion von Modernizr zum Entwickeln und Erweitern Ihrer Kenntnisse. Wenn Sie dann
            // für die Produktion bereit sind, verwenden Sie das Buildtool unter "http://modernizr.com", um nur die benötigten Tests auszuwählen.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // Zusätzliches Bundle für die Scripte des Stat2- Controllers
            bundles.Add(new ScriptBundle("~/bundles/Stat2").IncludeDirectory("~/Scripts/Stat2", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/RomToArab").IncludeDirectory("~/Scripts/JavaScriptLernen/RomToArab", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/ru").IncludeDirectory("~/Scripts/JavaScriptLernen/ru", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/Basics").IncludeDirectory("~/Scripts/JavaScriptLernen/Basics", "*.js"));


            //-----------------------------------------------------------------------------------------
            // Styles

            bundles.Add(new StyleBundle("~/Content/css").IncludeDirectory("~/CSS", "*.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            // Styles von Statcalc
            bundles.Add(new StyleBundle("~/Content/StatCalc/css").IncludeDirectory("~/Content/StatCalc", "*.css"));

            // Styles von Russland.cshtml
            bundles.Add(new StyleBundle("~/Content/ru/css").IncludeDirectory("~/Content/ru", "*.css"));

                
        }
    }
}