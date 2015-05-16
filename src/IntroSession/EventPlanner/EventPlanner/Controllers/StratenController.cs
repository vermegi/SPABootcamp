using System.Linq;
using System.Web.Mvc;
using EventPlanner.Contracts;
using EventPlanner.Models;

namespace EventPlanner.Controllers
{
    public class StratenController : Controller
    {
        [HttpGet]
        public ActionResult Straat(string zoekStraat)
        {
            using (var ctx = new EvenementEntities())
            {

                var straten = (from s in ctx.Straten
                    where s.Straatnaam.Contains(zoekStraat)
                    select new StraatDetail
                    {
                        Straatnaam = s.Straatnaam,
                        Id = s.Id,
                        Gemeente = s.Gemeente
                    }).ToList();
                return Json(straten, JsonRequestBehavior.AllowGet);
            }
        }
    }
}