using System.Web.Mvc;

namespace MusicR8r.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_Albums_Songs",
                "Admin/Albums/{albumId}/Songs/{action}/{id}",
                new { controller = "Songs", id = UrlParameter.Optional},
                namespaces: new string[] { "MusicR8r.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_Artist_Albums",
                "Admin/Artists/{artistId}/Albums/{action}/{id}",
                new { controller = "Albums", id = UrlParameter.Optional},
                namespaces: new string[] { "MusicR8r.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "MusicR8r.Areas.Admin.Controllers" }
            );


        }
    }
}