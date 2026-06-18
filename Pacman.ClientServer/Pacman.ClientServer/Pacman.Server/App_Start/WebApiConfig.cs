using System.Web.Http;
using System.Web.Http.Validation;
using Pacman.Common;

namespace Pacman.Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Настраиваем Ninject для Web API
            var kernel = NinjectKernel.Instance;

            if (kernel != null)
            {
                config.DependencyResolver = new NinjectDependencyResolver(kernel);
            }

            // Отключаем ModelValidatorProvider через Ninject
            config.Services.Clear(typeof(ModelValidatorProvider));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Настройка JSON форматирования (опционально)
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}
