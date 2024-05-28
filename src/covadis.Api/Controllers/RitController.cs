using covadis.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace covadis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RitController : ControllerBase
    {
        private readonly DbContextCovadis context;
        public RitController(DbContextCovadis context)
        {
            this.context = context;
        }

        [HttpPost]
        public ActionResult<Rit> CreateRide(Rit rit)
        {
            context.Ritten.Add(rit);
            context.SaveChanges();
            return Ok();
        }
        [HttpGet]
        public IEnumerable<Rit> SelectAllRides()
        {
            List<Rit> ritten = context.Ritten.ToList();
            return ritten;
        }

        [HttpGet("(id)")]
        public ActionResult<Rit> SelectRide(Guid id)
        {
            Rit rit = context.Ritten.SingleOrDefault(r => r.Id == id);
            return Ok();
        }

        [HttpPut("(id)")]
        public ActionResult<Rit> UpdateRide(Guid id, Rit nieuweRit)
        {
            Rit oudeRit = context.Ritten.SingleOrDefault(r => r.Id == id);
            oudeRit.registratie = nieuweRit.registratie;
            oudeRit.bestuurder = nieuweRit.bestuurder;
            oudeRit.auto = nieuweRit.auto;

            context.Ritten.Update(oudeRit);
            context.SaveChanges();

            return Ok();
        }

        [HttpDelete("(id)")]
        public ActionResult<Rit> DeleteRide(Guid id)
        {
            Rit rit = context.Ritten.FirstOrDefault(r => r.Id == id);
            context.Ritten.Remove(rit);
            context.SaveChanges();
            return Ok();
        }
    }
}
