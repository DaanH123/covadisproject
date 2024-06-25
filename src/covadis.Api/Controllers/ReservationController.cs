using covadis.Api.Services;
using covadis.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace covadis.Api.Controllers
{
    [Authorize]
    [Route("api/reservations")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _service;

        public ReservationController(ReservationService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CreateReservation(CreateReservationRequest request)
        {
            var reservation = _service.CreateReservation(request);

            if (reservation.Errors.Count > 0)
            {
                return BadRequest(reservation);
            }

            return CreatedAtAction(nameof(CreateReservation), reservation);
        }

        [HttpGet]
        public IActionResult GetReservations()
        {
            var reservations = _service.GetReservations();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public IActionResult GetReservation(int id)
        {
            var reservation = _service.GetReservationById(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateResevation(int id, UpdateReservationRequest request)
        {
            try
            {
                var response = _service.UpdateReservation(id, request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            try
            {
                _service.DeleteReservation(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
