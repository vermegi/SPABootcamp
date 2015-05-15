using System.Linq;
using System.Web.Mvc;
using EventPlanner.Contracts;
using EventPlanner.Models;

namespace EventPlanner.Controllers
{
    public class ReservatieController : Controller
    {
        public ActionResult Detail(int Id)
        {
            using (var ctx = new EvenementEntities())
            {
                var evenement = (from e in ctx.Evenementen
                                  where e.Id == Id
                                  select new EvenementWithMessage
                                  {
                                      Evenement = e
                                  }).SingleOrDefault();

                if (evenement == null)
                    evenement = new EvenementWithMessage();

                return View(evenement);
            }
        }

        public ActionResult OmschrijvingAanpassen(BasisDataReservatieAanpassenCommand command)
        {
            using (var ctx = new EvenementEntities())
            {
                var evenement = (from e in ctx.Evenementen
                                  where e.Id == command.Id
                                  select e).Single();

                if (!ModelState.IsValid)
                {
                    return View("detail", new EvenementWithMessage { Evenement = evenement, Message = "'t is nie just" });
                }

                evenement.Omschrijving = command.Omschrijving;
                evenement.MuziekVergunning = command.Muziek;

                ctx.SaveChanges();

                return View("detail", new EvenementWithMessage { Evenement = evenement, Message = "De wijzigingen werden succesvol opgeslagen" });
            }
        }
    }
}