using Nonogram.Server.App_Start;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Nonogram.Server
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Указываем явно адрес
            string baseUrl = "http://localhost:5001/";
            Console.WriteLine($"Server starting on {baseUrl}");

            GlobalConfiguration.Configure(WebApiConfig.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Убедимся, что Ninject инициализирован
            NinjectWebCommon.Start();
        }
    }
}