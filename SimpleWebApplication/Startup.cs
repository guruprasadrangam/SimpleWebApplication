using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleWebApplication.BasicAuthentication;
using System;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(SimpleWebApplication.Startup))]

namespace SimpleWebApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,

                //The path for generating token
                TokenEndpointPath = new PathString("/token"),

                //setting token expire time
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),

                Provider = new MyAuthorizationServerProvider()
            };

            //Token Generation
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}
