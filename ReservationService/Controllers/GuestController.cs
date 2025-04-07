using Microsoft.AspNetCore.Mvc;
using ReservationService.Models;
using ReservationService.Interface;
using ReservationService.Repositories;
using System.Collections.Generic;

namespace ReservationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly IGuest _guestRepository;

        public GuestController(IGuest guestRepository)
        {
            _guestRepository = guestRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Guest>> GetAllGuests()
        {
            var guests = _guestRepository.GetAllGuests();
            return Ok(guests);
        }

        [HttpGet("{id}")]
        public ActionResult<Guest> GetGuestById(int id)
        {
            var guest = _guestRepository.GetGuestById(id);
            if (guest == null) return NotFound();
            return Ok(guest);
        }

        [HttpPost]
        public IActionResult AddGuest([FromBody] Guest guest)
        {
            _guestRepository.AddGuest(guest);
            return CreatedAtAction(nameof(GetGuestById), new { id = guest.GuestId }, guest);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGuest(int id, [FromBody] Guest guest)
        {
            if (id != guest.GuestId) return BadRequest();
            _guestRepository.UpdateGuest(guest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGuest(int id)
        {
            var guestExists = _guestRepository.GuestExists(id);
            if (!guestExists) return NotFound();

            _guestRepository.DeleteGuest(id);
            return NoContent();
        }

        
    }
}
