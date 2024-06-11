using covadis.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace covadis.Api.Controllers
{
    [Route("api/vehicles")]
    [Authorize]
    [ApiController]
    public class VehicleController(VehicleService service) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetVehicles()
        {
            var vehicles = service.GetVehicles();

            return Ok(vehicles);
        }
    }
}
