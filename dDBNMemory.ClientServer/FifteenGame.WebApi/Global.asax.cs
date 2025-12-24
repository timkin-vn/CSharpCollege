using System.Web.Http;

namespace FifteenGame.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // НИЧЕГО НЕ ДЕЛАЕМ С NinjectKernel
            // Он сам создаётся при первом обращении:
            // NinjectKernel.Instance.Get<...>()
        }
    }
}
