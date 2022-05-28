using Newtonsoft.Json.Serialization;
using SimpleWebApplication.BasicAuthentication;
using SimpleWebApplication.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SimpleWebApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //To enable Basic Authentication for entire web application
            //config.Filters.Add(new BasicAuthenticationAttribute());

            //Enable RequireHTTPSAttribute filter
            config.Filters.Add(new CustomRequireHttpsAttribute());
            config.Filters.Add(new CustomExceptionFilter());
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
