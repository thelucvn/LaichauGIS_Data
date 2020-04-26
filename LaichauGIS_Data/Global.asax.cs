using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;

namespace LaichauGIS_Data
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            InitializeBundles();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitializeDisplayModeProviders();
        }
        protected void InitializeBundles()
        {
            var phoneScripts = new ScriptBundle("~/bundles/MobileJS")
                                .Include("~/Scripts/jquery.mobile-*", "~/Scripts/jquery-*");
            var phoneStyles = new StyleBundle("~/bundles/MobileCSS")
                                .Include("~/Content/jquery.mobile-1.4.5.min.css",
                                         "~/Content/jquery.mobile.structure-1.4.5.min.css",
                                         "~/Content/jquery.mobile.theme-1.4.5.min.css");
            BundleTable.Bundles.IgnoreList.Clear();
            BundleTable.Bundles.Add(phoneScripts);
            BundleTable.Bundles.Add(phoneStyles);
        }
        protected void InitializeDisplayModeProviders()
        {
            var phone = new DefaultDisplayMode("Phone")
            {
                ContextCondition = ctx => ctx.GetOverriddenUserAgent() != null && (ctx.GetOverriddenUserAgent().Contains("iPhone") || ctx.GetOverriddenUserAgent().Contains("Android"))
            };
            var tablet = new DefaultDisplayMode("Tablet")
            {
                ContextCondition = ctx => ctx.GetOverriddenUserAgent() != null && ctx.GetOverriddenUserAgent().Contains("iPad")
            };
            DisplayModeProvider.Instance.Modes.Insert(0, phone);
            DisplayModeProvider.Instance.Modes.Insert(1, tablet);
        }
    }
}
