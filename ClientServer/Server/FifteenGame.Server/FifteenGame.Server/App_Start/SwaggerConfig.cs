using System.Web.Http;
using WebActivatorEx;
using FifteenGame.Server;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace FifteenGame.Server
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    
                    c.SingleApiVersion("v1", "FifteenGame.Server");

                })
                .EnableSwaggerUi(c =>
                {
                    
                    c.DocumentTitle("Морской Бой API");
                });
        }

        
    }
}