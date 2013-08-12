using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace WebPortfolio
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: null,
                constraints: new { id = @"\d+" }
            );



            config.Routes.MapHttpRoute(
                name: "ActionIdApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: null,
                constraints: new { id = @"\d+" }
            );

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}"
            );

            

            config.Routes.MapHttpRoute(
                "DefaultApiGet",
                "api/{controller}",
                new { action = "GetValues" },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute(
                "DefaultApiPost",
                "api/{controller}",
                new { action = "Post" },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });


            config.Routes.MapHttpRoute(
                "DefaultApiPut",
                "api/{controller}",
                new { action = "Put" },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) });

            config.Routes.MapHttpRoute(
                "DefaultApiDelete",
                "api/{controller}",
                new { action = "Delete" },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) });

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
        }
    }
}