using covadis.Api.Services;
using covadis.Shared.Requests;
using covadis.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace covadis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleService vehicleService;

        public VehiclesController(VehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        [HttpGet]
        public IActionResult GetVehicles()
        {
            var vehicles = vehicleService.GetVehicles();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public IActionResult GetVehicleById(int id)
        {
            var vehicle = vehicleService.GetVehicleById(id);

            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }

        [HttpPost]
        public IActionResult CreateVehicle(CreateVehicleRequest request)
        {
            var createdVehicle = vehicleService.CreateVehicle(request);
            return CreatedAtAction(nameof(GetVehicleById), new { id = createdVehicle.Id }, createdVehicle);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVehicle(int id, UpdateVehicleRequest request)
        {
            var success = vehicleService.UpdateVehicle(id, request);

            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            var success = vehicleService.DeleteVehicle(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
