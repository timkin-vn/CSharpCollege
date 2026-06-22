using System.Web;
using System.Web.Http;
using Game2048.DataAccess.EF;

namespace Game2048.WebApi
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
