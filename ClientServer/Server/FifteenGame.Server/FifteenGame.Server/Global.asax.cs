using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http; // <--- ќЅя«ј“≈Ћ№Ќќ ƒќЅј¬№ Ё“ќ“ USING
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FifteenGame.Server
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // 1. —Ќј„јЋј регистрируем конфигурацию Web API.
            // »менно эта строка вызывает метод Register в файле WebApiConfig.cs
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // 2. ѕотом всЄ остальное (MVC, бандлы и т.д.)
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}