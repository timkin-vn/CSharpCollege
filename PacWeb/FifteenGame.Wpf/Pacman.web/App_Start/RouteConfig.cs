using System.Web.Mvc;
using System.Web.Routing;

namespace Pacman.web   // ← твой namespace
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Game", action = "Index", id = UrlParameter.Optional }  // ← ВОТ ЭТО ГЛАВНОЕ!
            );
        }
    }
}