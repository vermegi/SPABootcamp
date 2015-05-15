using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EventPlanner.Contracts;
using EventPlanner.Models;

namespace EventPlanner.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(GetReservaties(DateTime.Now, DateTime.Now.AddDays(7)));
        }

        public ActionResult PeriodeList(DateTime DatumVan, DateTime DatumTot)
        {
            return View("Index", GetReservaties(DatumVan, DatumTot));
        }

        private IEnumerable<ReservatieResdata> GetReservaties(DateTime datumVan, DateTime datumTot)
        {
            using (var ctx = new EvenementEntities())
            {
                var vergunningen = ctx.Dagen
                    .Where(d => d.Datum >= datumVan && d.Datum <= datumTot)
                    .Join(
                        ctx.Periodes,
                        d => d.PeriodeId,
                        p => p.Id,
                        (d, p) => new { Dag = d, Periode = p })
                    .Join(
                        ctx.Evenementen,
                        dp => dp.Periode.VergunningId,
                        v => v.Id,
                        (dp, v) => new { Dag = dp.Dag, Periode = dp.Periode, Vergunning = v, Straten = dp.Periode.Straten })
                    .ToList();
                return vergunningen.Select(rrd => new ReservatieResdata
                {
                    Evenement = rrd.Vergunning,
                    Reservatiedatum = rrd.Dag.Datum,
                    Straten = rrd.Straten.OrderBy(s => s.Straatnaam).ToList()
                }).ToList();
            }
        }

    }
}