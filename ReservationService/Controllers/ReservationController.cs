using Microsoft.AspNetCore.Mvc;
using ReservationService.Interface;
using ReservationService.DTO; // Assuming DTOs are here
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservation _reservationService;

        public ReservationController(IReservation reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationGetDTO>>> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ReservationGetDTO>>> GetReservationById(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null) return NotFound();
            return Ok(reservation);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<ReservationGetDTO>>> GetActiveReservations()
        {
            var activeReservations = await _reservationService.GetActiveReservationAsync();
            return Ok(activeReservations);
        }

        [HttpPost]
        public async Task<ActionResult<ReservationGetDTO>> AddReservation([FromBody] ReservationGetDTO reservation)
        {
            var result = await _reservationService.AddReservationAsync(reservation);
            return CreatedAtAction(nameof(GetReservationById), new { id = result.ReservationId }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReservationGetDTO>> UpdateReservation(int id, [FromBody] ReservationGetDTO reservation)
        {
            if (id != reservation.ReservationId) return BadRequest();
            var updated = await _reservationService.UpdateReservationAsync(id, reservation);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return NoContent();
        }
    }
}
