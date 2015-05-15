using System.Data.Entity;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EventPlanner.Models;

namespace EventPlanner
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            SqlConnection.ClearAllPools();
            Database.SetInitializer(new EvenementEntitiesInitializer());
        }
    }
}
