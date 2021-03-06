﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebPortfolio
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Register",
                url: "Register",
                defaults: new { controller = "Account", action = "Register" }
            );

            routes.MapRoute(
                name: "Login",
                url: "Login",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
               name: "File",
               url: "File/{id}/{name}",
               defaults: new { controller = "File", action = "Get" },
               constraints: new { id = @"\d+", name = @"\w+" }

           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index" },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: "ControllerAndAction",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );

           
        }
    }
}