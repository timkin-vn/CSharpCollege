using System.Web;
using System.Web.Http;
using SeaBattle.DataAccess.EF;

namespace SeaBattle.WebApi
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
