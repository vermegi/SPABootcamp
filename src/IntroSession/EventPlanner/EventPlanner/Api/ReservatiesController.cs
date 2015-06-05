using System;
using System.Linq;
using System.Web.Http;
using EventPlanner.Code.Extensions;
using EventPlanner.Contracts;
using EventPlanner.Models;

namespace EventPlanner.Api
{
    public class ReservatiesController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(GetReservaties(DateTime.Now, DateTime.Now.AddDays(7), 0, 10, string.Empty));
        }

        public IHttpActionResult GetForDate(DateTime datumVan, DateTime datumTot, int start, int number, string searchTerm)
        {
            return Ok(GetReservaties(datumVan, datumTot, start, number, searchTerm));
        }

        private ReservatieListResult GetReservaties(DateTime datumVan, DateTime datumTot, int start, int number,
            string searchTerm)
        {
            using (var ctx = new EvenementEntities())
            {
                var query = ctx.Dagen
                    .Where(d => d.Datum >= datumVan && d.Datum <= datumTot)
                    .Join(
                        ctx.Periodes,
                        d => d.PeriodeId,
                        p => p.Id,
                        (d, p) => new {Dag = d, Periode = p})
                    .Join(
                        ctx.Evenementen,
                        dp => dp.Periode.EvenementId,
                        v => v.Id,
                        (dp, v) =>
                            new {Dag = dp.Dag, Periode = dp.Periode, Vergunning = v, Straten = dp.Periode.Straten});

                if (!String.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query
                        .Where(v => v.Vergunning.Titel.Contains(searchTerm)
                                    || v.Vergunning.Omschrijving.Contains(searchTerm)
                                    || v.Straten.Any(s => s.Straatnaam.Contains(searchTerm))
                        );
                }

                var vergunningen = query
                    .OrderBy(v => v.Dag.Datum)
                    .Skip(start)
                    .Take(number)
                    .ToList();

                var result = new ReservatieListResult
                {
                    Rows = vergunningen.Select(rrd => new ReservatieDataRow
                    {
                        Id = rrd.Vergunning.Id,
                        Reservatiedatum = rrd.Dag.Datum,
                        Titel = rrd.Vergunning.Titel,
                        Eigenaar = rrd.Vergunning.Eigenaar,
                        DatumBeslissing = rrd.Vergunning.DatumBeslissing,
                        Straten = rrd.Straten.OrderBy(s => s.Straatnaam).ToList().ToKommaSeparatedString(),
                    }).ToList(),
                    NumberOfPages = query.Count()/number
                };

                return result;
            }
        }
    }
}