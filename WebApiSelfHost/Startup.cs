using Owin;
using System.Web.Http;

namespace WebApiSelfHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.ConfigureAuth();

            app.UseLogging();
            app.UseWebApi(config);
            
        }
    }
}
