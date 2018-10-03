using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using MySportsBook.Api.Authentication;
using Owin;
using System;
using System.Web.Http;

[assembly: OwinStartup(typeof(MySportsBook.Api.Startup))]
namespace MySportsBook.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            var Providers = new AuthorizationServerProvider();
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/gettoken"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = Providers,
                RefreshTokenProvider = new RefreshTokenProvider()

            };

            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}
