using covadis.Shared.Requests;
using GraafschapCollege.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace covadis.Api.Controllers
{
    [Authorize]
    [Route("api/reservations")]
    [ApiController]
    public class ReservationController(ReservationService service) : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateReservation(CreateReservationRequest request)
        {
            var reservation = service.CreateReservation(request);

            if (reservation.Errors.Count > 0)
            {
                return BadRequest(reservation);
            }

            return CreatedAtAction(nameof(CreateReservation), reservation);
        }

        [HttpGet]
        public IActionResult GetReservations()
        {
            var reservations = service.GetReservations();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public IActionResult GetReservation(int id)
        {
            var reservation = service.GetReservationById(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }
    }
}
