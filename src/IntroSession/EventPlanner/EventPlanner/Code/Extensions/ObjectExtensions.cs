using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EventPlanner.Code.Extensions
{
    public static class ObjectExtensions
    {
        public static ActionResult ToJsonResult(this object theObject)
        {
            return new ContentResult
            {
                ContentType = "application/json",
                Content =
                    JsonConvert.SerializeObject(theObject,
                        new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()}),
                ContentEncoding = Encoding.UTF8

            };
        }
    }
}