using System.Web;
using System.Web.Http;
using Minesweeper.DataAccess.EF;

namespace Minesweeper.WebApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            DatabaseInitializer.EnsureCreated();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
