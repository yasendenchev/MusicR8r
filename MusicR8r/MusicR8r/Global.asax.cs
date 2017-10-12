using MusicR8r.App_Start;
using MusicR8r.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MusicR8r
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<MsSqlDbContext>(null);

            using (var db = new MsSqlDbContext())
            {
                db.Database.Initialize(false);
            }

            var migrator = new DbMigrator(new Configuration());

            if (migrator.GetPendingMigrations().Any())
            {
                migrator.Update();
            }

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MsSqlDbContext, Configuration>());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var mapper = new AutoMapperConfig();
            mapper.Execute(Assembly.GetExecutingAssembly());
        }
    }
}
