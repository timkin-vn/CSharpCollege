using System.Web.Http;
using MinesweeperGame.WebApi.App_Start;

namespace MinesweeperGame.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
