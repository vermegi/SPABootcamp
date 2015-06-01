using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using EventPlanner.Contracts;
using EventPlanner.Models;

namespace EventPlanner.Api
{
    public class ReservatieController : ApiController
    {
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
    }
}