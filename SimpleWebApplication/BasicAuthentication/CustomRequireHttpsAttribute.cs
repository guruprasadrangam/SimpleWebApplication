using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SimpleWebApplication.BasicAuthentication
{
    public class CustomRequireHttpsAttribute: AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if(actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Found);
                actionContext.Response.Content = new StringContent("<p>use https instead of http</p>", Encoding.UTF8, "text/htmll");

                //create the request uri
                UriBuilder uriBuilder = new UriBuilder(actionContext.Request.RequestUri);

                //Sets the request scheme as HTTPS
                uriBuilder.Scheme = Uri.UriSchemeHttps;

                //Set the HTTPS port number as 44399
                uriBuilder.Port = actionContext.Request.RequestUri.Port;
                actionContext.Response.Headers.Location = uriBuilder.Uri;
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
        }
    }
}