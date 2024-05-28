using covadis.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace covadis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoController : ControllerBase
    {
        private readonly DbContextCovadis context;
        public AutoController(DbContextCovadis context)
        {
            this.context = context;
        }

        [HttpPost]
        public ActionResult<Auto> CreateCar(Auto auto)
        {
            context.Add(auto);
            context.SaveChanges();

            return Ok();
        }


        [HttpGet]
        public IEnumerable<Auto> SelectAllCars()
        {
            List<Auto> autos = context.Autos.ToList();

            return autos;
        }


        [HttpGet("(id)")]
        public ActionResult<Auto> SelectCar(Guid id)
        {
            Auto auto = context.Autos.SingleOrDefault(x => x.Id == id);

            if (auto != null) return Ok(auto);

            return BadRequest();

        }


        [HttpPut("id")]
        public ActionResult<Auto> UpdateCar(Guid id, Auto nieuweAuto)
        {
            Auto oudeAuto = context.Autos.SingleOrDefault(x => x.Id == id);

            if (oudeAuto != null)
            {
                oudeAuto.Kenteken = nieuweAuto.Kenteken;
                oudeAuto.Merk = nieuweAuto.Merk;
                oudeAuto.Model = nieuweAuto.Model;
                oudeAuto.Rides = nieuweAuto.Rides;

                context.Autos.Update(oudeAuto);
                context.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }


        [HttpDelete("id")]
        public ActionResult DeleteCar(Guid id)
        {
            Auto auto = context.Autos.SingleOrDefault(x => x.Id == id);

            if (auto != null)
            {
                context.Autos.Remove(auto);
                context.SaveChanges();
            }

            return Ok();
        }
    }
}
