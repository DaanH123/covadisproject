using covadis.Api.Services;
using covadis.Shared.Requests;
using covadis.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace covadis.Api.Controllers
{
    [Authorize]
    [Route("api/trips")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly TripService _service;

        public TripController(TripService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrip([FromBody] TripRequest request)
        {
            var trip = await _service.CreateTripAsync(request);
            if (trip == null)
            {
                return BadRequest("Failed to create trip.");
            }

            return Ok(trip);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripById(int id)
        {
            var trip = await _service.GetTripByIdAsync(id);
            if (trip == null)
            {
                return NotFound();
            }

            return Ok(trip);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            var success = await _service.DeleteTripAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("reservations/{reservationId}/trips")]
        public async Task<ActionResult<List<TripResponse>>> GetTripsByReservationId(int reservationId)
        {
            var trips = await _service.GetTripsByReservationIdAsync(reservationId);
            return Ok(trips);
        }
    }
}
