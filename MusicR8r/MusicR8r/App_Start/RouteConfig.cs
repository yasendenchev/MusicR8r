using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MusicR8r
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "Albums_Songs",
                "Albums/{albumId}/Songs/{action}/{id}",
                new { controller = "Songs", action = "All", id = UrlParameter.Optional },
                namespaces: new string[] { "MusicR8r.Controllers" }
                );
            //routes.MapRoute(
            //    name: "Profile",
            //    url: "Profile/Details",
            //    defaults: new { controller = "Profile", action = "Details" }
            //    );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "MusicR8r.Controllers" }
            );
            routes.MapRoute(
                name: "EditProfile",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Profile", action = "Edit", id = UrlParameter.Optional },
                namespaces: new string[] { "MusicR8r.Controllers" }
            );
        }
    }
}
