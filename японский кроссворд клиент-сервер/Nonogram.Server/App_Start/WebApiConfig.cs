using System.Web.Http;

namespace Nonogram.Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Маршрутизация атрибутов
            config.MapHttpAttributeRoutes();

            // Маршрут по умолчанию
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Удаляем XML форматтер, оставляем только JSON
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Включаем подробные ошибки для отладки
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        }
    }
}