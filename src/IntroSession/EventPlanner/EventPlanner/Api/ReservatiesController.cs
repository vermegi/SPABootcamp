using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using EventPlanner.Contracts;
using EventPlanner.Models;

namespace EventPlanner.Api
{
    public class ReservatiesController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(GetReservaties(DateTime.Now, DateTime.Now.AddDays(7)));
        }

        public IHttpActionResult GetForDate(DateTime datumVan, DateTime datumTot)
        {
            return Ok(GetReservaties(datumVan, datumTot));
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
                        dp => dp.Periode.EvenementId,
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