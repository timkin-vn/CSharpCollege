using System;
using System.Data.Entity;
using System.Web.Http;
using Pacman.DataAccess.EF;

namespace Pacman.Server
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Отключаем автоматические миграции - используем существующую БД
            Database.SetInitializer<PacmanDbContext>(null);

            // Регистрируем Web API маршруты
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
