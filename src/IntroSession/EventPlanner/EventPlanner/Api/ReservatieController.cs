using System;
using System.Linq;
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
    }
}