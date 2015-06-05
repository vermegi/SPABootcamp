using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using EventPlanner.Code.Extensions;
using EventPlanner.Contracts;
using EventPlanner.Models;

namespace EventPlanner.Api
{
    public class ReservatieController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetReservatieData([FromUri]GetEvenementDataRequest request)
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

                return Ok(reservatie);
            }
        }

        [HttpPost]
        public IHttpActionResult OmschrijvingAanpassen(BasisDataReservatieAanpassenCommand command)
        {
            using (var ctx = new EvenementEntities())
            {
                var evenement = (from e in ctx.Evenementen
                                 where e.Id == command.Id
                                 select e).Single();

                if (!ModelState.IsValid)
                {
                    return StatusCode(HttpStatusCode.NotAcceptable);
                }

                evenement.Omschrijving = command.Omschrijving;
                evenement.MuziekVergunning = command.MuziekVergunning;

                ctx.SaveChanges();

                return Ok();
            }
        }

        [HttpPost]
        public IHttpActionResult PeriodeToevoegen(PeriodeToevoegenCommand command)
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

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetOverlap(GetOverlapRequest request)
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

                return Ok(all);
            }
        }
    }
}