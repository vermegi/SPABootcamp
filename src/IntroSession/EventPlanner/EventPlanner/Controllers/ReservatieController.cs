using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using EventPlanner.Code.Extensions;
using EventPlanner.Contracts;
using EventPlanner.Models;

namespace EventPlanner.Controllers
{
    public class ReservatieController : Controller
    {
        public ActionResult Detail(int Id)
        {
            return View(Id);
        }

        [HttpPost]
        public ActionResult OmschrijvingAanpassen(BasisDataReservatieAanpassenCommand command)
        {
            using (var ctx = new EvenementEntities())
            {
                var evenement = (from e in ctx.Evenementen
                                  where e.Id == command.Id
                                  select e).Single();
                var reservatieData =
                    ctx.Periodes.Where(p => p.EvenementId == evenement.Id).SelectMany(p => p.Dagen).Select(d => d.Datum).ToList();

                if (!ModelState.IsValid)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
                }

                evenement.Omschrijving = command.Omschrijving;
                evenement.MuziekVergunning = command.MuziekVergunning;

                ctx.SaveChanges();

                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }
        }

        [HttpPost]
        public ActionResult PeriodeToevoegen(PeriodeToevoegenCommand command)
        {
            using (var ctx = new EvenementEntities())
            {
                var periode = new Periode();
                periode.BeginPeriode = command.BeginDatumPeriode;
                periode.EindePeriode = command.EindDatumPeriode;

                var straten = (from s in ctx.Straten
                               where command.GeselecteerdeStraten.Contains(s.Id)
                               select s);

                periode.Straten = new List<Straat>();
                foreach (var straat in straten)
                {
                    periode.Straten.Add(straat);
                }

                var dagen = command.GenereerDagen();

                periode.Dagen = new List<Dag>();
                foreach (var datum in dagen.Where(d => command.DagenToSkip == null || !command.DagenToSkip.Contains(d)))
                {
                    var dag = new Dag();
                    dag.Datum = datum;
                    periode.Dagen.Add(dag);
                }

                periode.EvenementId = command.EvenementId;

                ctx.Periodes.Add(periode);

                ctx.SaveChanges();

            }

            return Json(0);
        }

        public ActionResult GetOverlap(GetOverlapRequest request)
        {
            var dagen = request.GenereerDagen();
            var straten = request.GeselecteerdeStraten;

            using (var ctx = new EvenementEntities())
            {
                var overlappendeReservaties = (from v in ctx.Evenementen
                                               join p in ctx.Periodes on v.Id equals p.EvenementId
                                               where p.Straten.Any(s => request.GeselecteerdeStraten.Contains(s.Id))
                                               join d in ctx.Dagen on p.Id equals d.PeriodeId
                                               where dagen.Contains(d.Datum)
                                               select new { Dag = d.Datum, Reservatie = v, Straten = p.Straten }).ToList();

                var all = (from dag in dagen
                           join overlappendeReservatie in overlappendeReservaties on dag equals overlappendeReservatie.Dag into reservatie
                           select new { Dag = dag, Overlappingen = reservatie.Select(res => new Overlapping { Omschrijving = res.Reservatie.Omschrijving, Straat = res.Straten.ToKommaSeparatedString() }) }).ToList();

                return Json(all, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetReservatieData(GetEvenementDataRequest request)
        {
            using (var ctx = new EvenementEntities())
            {
                var reservatie = (from r in ctx.Evenementen
                                  where r.Id == request.EvenementId
                                  select new EvenementDetail
                                  {
                                      Evenement = r,
                                      ReservatieData = r.Periodes.SelectMany(p => p.Dagen.Select(d => d.Datum))

                                  }).SingleOrDefault();

                return reservatie.ToJsonResult();
            }
        }
    }
}