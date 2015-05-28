using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace EventPlanner
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            RegisterWebApiRoutes(routes);

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        private static void RegisterWebApiRoutes(RouteCollection routes)
        {
            routes.MapHttpRoute(
                "ApiControllerOnly",
                "api/{controller}");

            routes.MapHttpRoute(
                "ApiControllerAndIntegerId",
                "api/{controller}/{id}",
                null,
                new { id = @"/^\d+$|\b[A-F0-9]{8}(?:-[A-F0-9]{4}){3}-[A-F0-9]{12}\b/i" }); // INT's of GUID's

            routes.MapHttpRoute(
                "ApiControllerAction",
                "api/{controller}/{action}"
            );
        }
    }
}
