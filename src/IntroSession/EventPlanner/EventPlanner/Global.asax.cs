using System.Data.Entity;
using System.Data.SqlClient;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using EventPlanner.Models;
using Newtonsoft.Json.Serialization;

namespace EventPlanner
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            SqlConnection.ClearAllPools();
            Database.SetInitializer(new EvenementEntitiesInitializer());
        }
    }
}
