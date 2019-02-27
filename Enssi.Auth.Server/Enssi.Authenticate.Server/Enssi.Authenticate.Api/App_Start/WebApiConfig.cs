using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Enssi.Authenticate.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}"//,
                                                          //defaults: new { id = RouteParameter.Optional }
            );

            config.MessageHandlers.Add(new DecompressionHandler());
            config.MessageHandlers.Add(new ApiMessageHandler());
            // config.Filters.Add(new AuthorizeAttribute());
            config.Filters.Add(new CompressionAttribute());
            config.Formatters.Add(new FormMultipartEncodedMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}
