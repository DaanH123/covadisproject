using covadis.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace covadis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly DbContextCovadis context;
        public RegistrationController(DbContextCovadis context)
        {
            this.context = context;
        }

        [HttpPost]
        public ActionResult<Registration> CreateRegistration(Registration registratie)
        {
            context.Registration.Add(registratie);
            context.SaveChanges();
            return Ok();
        }
        [HttpGet]
        public IEnumerable<Registration> SelectAllRegistration()
        {
            List<Registration> registratie = context.Registration.ToList();
            return registratie;
        }

        [HttpGet("(id)")]
        public ActionResult<Registration> SelectRegistration(Guid id)
        {
            Registration registratie = context.Registration.SingleOrDefault(r => r.Id == id);
            return Ok();
        }

        [HttpPut("(id)")]
        public ActionResult<Registration> UpdateRegistration(Guid id, Registration nieuweRegistratie)
        {
            Registration oudeRegistratie = context.Registration.SingleOrDefault(r => r.Id == id);
            oudeRegistratie.StartKM = nieuweRegistratie.StartKM;
            oudeRegistratie.EndKM = nieuweRegistratie.EndKM;
            oudeRegistratie.Adresses = nieuweRegistratie.Adresses;

            context.Registration.Update(oudeRegistratie);
            context.SaveChanges();

            return Ok();
        }

        [HttpDelete("(id)")]
        public ActionResult<Registration> DeleteRegistration(Guid id)
        {
            Registration registratie = context.Registration.FirstOrDefault(r => r.Id == id);
            context.Registration.Remove(registratie);
            context.SaveChanges();
            return Ok();
        }
    }
}
