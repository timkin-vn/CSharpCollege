using Ninject;
using Nonogram.Common.Infrastructure;
using Nonogram.Server.Infrastructure;
using System.Web.Http;

namespace Nonogram.Server
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Настройка Ninject
            var kernel = new StandardKernel(new NonogramServerModule());
            NinjectKernel.Instance = kernel;

            // Настройка Web API
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Настройка JSON сериализации
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}