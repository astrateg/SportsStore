using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SportsStore.WebUI.Infrastructure;
using SportsStore.Domain.Entities;
using System.Web.UI;

namespace SportsStore.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
                "",                     // Only matches the empty URL (i.e. ~/)
                new { controller = "Products", action = "List", category = (string)null, page = 1}
            );

            routes.MapRoute(null,
                "Page{page}",           // Matches ~/Page2, ~/Page123, but not ~/PageXYZ 
                new { controller = "Products", action = "List", category = (string)null },
                new { page = @"\d+" }   // Constraints: page must be numerical
            );

            routes.MapRoute(null, 
                "{category}",           // Matches ~/Football or ~/AnythingWithNoSlash
                new { controller = "Products", action = "List", page = 1 }
            );

            routes.MapRoute(null,
                "{category}/Page{page}",// Matches ~/Football/Page567 
                new { controller = "Products", action = "List" },
                new { page = @"\d+" }   // Constraints: page must be numerical
            );

            routes.MapRoute(null, "{controller}/{action}");
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }

protected void Application_BeginRequest(object sender, EventArgs e)
{
    // Uses Web Forms code to apply "auto" culture to current thread and deal with 
    // invalid culture requests automatically 
    using (var fakePage = new Page())
    {
        var ignored = fakePage.Server;     // Work around a Web Forms quirk 
        fakePage.Culture = "auto";         // Apply local formatting to this thread 
        fakePage.UICulture = "auto";       // Apply local language to this thread 
    }
} 
    }
}