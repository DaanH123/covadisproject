using covadis.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace covadis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WerknemerController : ControllerBase
    {
        private readonly DbContextCovadis context;
        public WerknemerController(DbContextCovadis context)
        {
            this.context = context;
        }

        [HttpPost]
        public ActionResult<Werknemer> CreateEmployee(Werknemer werknemer)
        {
            context.werknemers.Add(werknemer);
            context.SaveChanges();
            return Ok();
        }
        [HttpGet]
        public IEnumerable<Werknemer> SelectAllEmployees()
        {
            List<Werknemer> werknemers = context.werknemers.ToList();
            return werknemers;
        }

        [HttpGet("(id)")]
        public ActionResult<Werknemer> SelectEmployee(Guid id)
        {
            Werknemer werknemer = context.werknemers.SingleOrDefault(r => r.Id == id);
            return Ok();
        }

        [HttpPut("(id)")]
        public ActionResult<Werknemer> UpdateEmployee(Guid id, Werknemer nieuweWerknemer)
        {
            Werknemer oudeWerknemer = context.werknemers.SingleOrDefault(e => e.Id == id);
            oudeWerknemer.Naam = nieuweWerknemer.Naam;
            oudeWerknemer.Email = nieuweWerknemer.Email;
            oudeWerknemer.TelefoonNr = nieuweWerknemer.TelefoonNr;

            context.werknemers.Update(oudeWerknemer);
            context.SaveChanges();

            return Ok();
        }

        [HttpDelete("(id)")]
        public ActionResult<Werknemer> DeleteEmployee(Guid id)
        {
            Werknemer werknemer = context.werknemers.FirstOrDefault(r => r.Id == id);
            context.werknemers.Remove(werknemer);
            context.SaveChanges();
            return Ok();
        }
    }
}
