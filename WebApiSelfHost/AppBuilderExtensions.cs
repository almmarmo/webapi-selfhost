using Microsoft.Owin.Security.OAuth;
using Owin;
using System;

namespace WebApiSelfHost
{
    public static class AppBuilderExtensions
    {
        public static void UseLogging(this IAppBuilder app)
        {
            app.Use<LoggingComponent>();
        }

        public static void ConfigureAuth(this IAppBuilder app)
        {
            var OAuthOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new Microsoft.Owin.PathString("/Token"),
                Provider = new ApplicationOAuthServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
