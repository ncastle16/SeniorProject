using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Roadtrip
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Routes", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SaveRoute",
                url: "SavedRoutes/SaveRoute",
                defaults: new { controller = "SavedRoutes", action = "SaveRoute" }
            );

            routes.MapRoute(
                name: "GetEstablishment",
                url: "Routes/GetEstablishment",
                defaults: new { controller = "Routes", action = "GetEstablishment"}
            );

            routes.MapRoute(
                name: "GetDetails",
                url: "Routes/GetDetails/{id}",
                defaults: new { controller = "Routes", action = "GetDetails", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Routes",
                url: "{controller}/{action}/{id}", 
                defaults: new { controller = "Routes", action = "Create", id = UrlParameter.Optional }
            );
        }
    }
}
