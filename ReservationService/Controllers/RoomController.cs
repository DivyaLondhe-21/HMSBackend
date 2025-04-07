//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ReservationService.Interface;
//using ReservationService.Models;

//namespace ReservationService.Controllers
//{
//    public class RoomController : Controller
//    {
//        private readonly IRoom _roomRepository;

//        public RoomController(IRoom roomRepository)
//        {
//            _roomRepository = roomRepository;
//        }
//        public IActionResult Index()
//        {
//            return View();
//        }
//        /* [HttpGet("{roomId}/rate")]
//         public async Task<IActionResult> GetRoomRate(int roomId)
//         {
//            *//*var room = await _roomRepository.GetRoomRates(roomId);

//             if (room == null)
//             {
//                 return NotFound("Room not found.");
//             }

//             return Ok(room);*//*
//         }*/

//        [HttpGet("rate/{RoomId}")]
//        public async Task<ActionResult<Rate>> GetRateByRoom(int RoomId)
//        {
//            var rate =  _roomRepository.GetRoomById(RoomId);
//            if (rate == null)
//            {
//                return NotFound("Rate not found for the selected room.");
//            }
//            return Ok(rate);
//        }

//    }
//}

using Microsoft.AspNetCore.Mvc;
using ReservationService.Models;
using ReservationService.Repositories;
using ReservationService.Interface;

namespace ReservationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoom _roomRepository;

        public RoomController(IRoom roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet("available/{roomType}")]
        public ActionResult<IEnumerable<Room>> GetAvailableRoomsByType(string roomType)
        {
            return Ok(_roomRepository.GetAvailableRoomsByType(roomType));
        }

        [HttpGet("{id}")]
        public ActionResult<Room> GetRoomById(int id)
        {
            var room = _roomRepository.GetRoomById(id);
            if (room == null) return NotFound();
            return Ok(room);
        }
    }
}