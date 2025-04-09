//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using RoomService.Models;
//using RoomService.Interface;
//using Microsoft.AspNetCore.Authorization;

////[Authorize]
//[Route("api/[controller]")]
//[ApiController]
//public class RoomController : ControllerBase
//{
//    private readonly IRoom _roomRepository;

//    public RoomController(IRoom roomRepository)
//    {
//        _roomRepository = roomRepository;
//    }

//    // GET: api/Room
//    [HttpGet("all")]
//    public async Task<IActionResult> GetRooms()
//    {
//        var rooms = await _roomRepository.GetAllRoomsAsync();
//        return Ok(rooms);
//    }
//    [HttpGet("available")]
//    public async Task<IActionResult> GetAvailableRooms()
//    {
//        var rooms = await _roomRepository.GetAvailableRoomsAsync();
//        return Ok(rooms);
//    }

//    // GET: api/Room/5
//    [HttpGet("{id}")]
//    public async Task<IActionResult> GetRoom(int id)
//    {
//        var room = await _roomRepository.GetRoomByIdAsync(id);
//        if (room == null)
//        {
//            return NotFound();
//        }

//        return Ok(room);
//    }

//    // POST: api/Room
//    [HttpPost("create")]
//    public async Task<IActionResult> CreateRoom([FromBody] Room room)
//    {
//        if (!ModelState.IsValid)
//        {
//            return BadRequest(ModelState);
//        }

//        var createdRoom = await _roomRepository.CreateRoomAsync(room);
//        return CreatedAtAction(nameof(GetRoom), new { id = createdRoom.RoomID }, createdRoom);
//    }

//    // PUT: api/Room/5
//    [HttpPut("update/{id}")]
//    public async Task<IActionResult> UpdateRoom(int id, [FromBody] Room room)
//    {
//        if (id != room.RoomID)
//        {
//            return BadRequest();
//        }

//        var updatedRoom = await _roomRepository.UpdateRoomAsync(id, room);
//        if (updatedRoom == null)
//        {
//            return NotFound();
//        }

//        return NoContent();
//    }

//    [HttpPatch("update/availability/{id}")]
//    public async Task<IActionResult> UpdateRoomAvailability(int id, [FromBody] bool isAvailable)
//    {
//        var updatedRoom = await _roomRepository.UpdateAvailabilityOfRoom(id, isAvailable);
//        if (updatedRoom == null)
//        {
//            return NotFound();
//        }
//        return Ok(updatedRoom);
//    }

//    // DELETE: api/Room/5
//    [HttpDelete("remove/{id}")]
//    public async Task<IActionResult> DeleteRoom(int id)
//    {
//        var deleted = await _roomRepository.DeleteRoomAsync(id);
//        if (!deleted)
//        {
//            return NotFound();
//        }

//        return NoContent();
//    }
//}

using Microsoft.AspNetCore.Mvc;
using RoomService.DTO;
using RoomService.Interfaces;
using System.Threading.Tasks;

namespace RoomService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoom _roomRepo;
        private readonly IRate _rateRepo;

        public RoomController(IRoom roomRepo, IRate rateRepo)
        {
            _roomRepo = roomRepo;
            _rateRepo = rateRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            Console.Write("Room is get");
            var rooms = await _roomRepo.GetAllAsync();
            return Ok(rooms);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _roomRepo.GetByIdAsync(id);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom(RoomDTO dto)
        {
            Console.Write(dto.ToString() + " " + dto.RoomID + " " + dto.RoomType + " " + dto.Price + " " + dto.Period + " " + dto.Availability + " " + dto.GuestId);
            await _roomRepo.AddAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, RoomDTO dto)
        {
            dto.RoomID = id;
            await _roomRepo.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _roomRepo.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("{roomId}/rates")]
        public async Task<IActionResult> GetRatesForRoom(int roomId)
        {
            var rates = await _rateRepo.GetRatesByRoomIdAsync(roomId);
            return Ok(rates);
        }

        [HttpPost("rates")]
        public async Task<IActionResult> AddRate(RateDTO dto)
        {
            await _rateRepo.AddAsync(dto);
            return Ok();
        }

        [HttpPut("rates/{rateId}")]
        public async Task<IActionResult> UpdateRate(int rateId, RateDTO dto)
        {
            dto.RateId = rateId;
            await _rateRepo.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete("rates/{rateId}")]
        public async Task<IActionResult> DeleteRate(int rateId)
        {
            await _rateRepo.DeleteAsync(rateId);
            return Ok();
        }
    }
}
