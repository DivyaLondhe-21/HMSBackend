//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using RoomService.Models;
//using RoomService.Interfaces;
//using Microsoft.AspNetCore.Authorization;

////[Authorize]
//[Route("api/[controller]")]
//[ApiController]
//public class RateController : ControllerBase
//{
//    private readonly IRate _rateRepository;

//    public RateController(IRate rateRepository)
//    {
//        _rateRepository = rateRepository;
//    }

//    // GET: api/Rate
//    [HttpGet] 
//    public async Task<IActionResult> GetRates()
//    { 
//        var rates = await _rateRepository.GetAllRatesAsync();
//        return Ok(rates);
//    }

//    // GET: api/Rate/5
//    [HttpGet("{id}")]
//    public async Task<IActionResult> GetRate(int id)
//    {
//        var rate = await _rateRepository.GetRateByIdAsync(id);
//        if (rate == null)
//        {
//            return NotFound();
//        }

//        return Ok(rate);
//    }

//    // POST: api/Rate
//    [HttpPost("create")]
//    public async Task<IActionResult> CreateRate([FromBody] Rate rate)
//    {
//        if (!ModelState.IsValid)
//        {
//            return BadRequest(ModelState);
//        }

//        var createdRate = await _rateRepository.CreateRateAsync(rate);
//        return CreatedAtAction(nameof(GetRate), new { id = createdRate.RateId }, createdRate);
//    }

//    // PUT: api/Rate/5
//    [HttpPut("update/{id}")]
//    public async Task<IActionResult> UpdateRate(int id, [FromBody] Rate rate)
//    {
//        if (id != rate.RateId)
//        {
//            return BadRequest();
//        }

//        var updatedRate = await _rateRepository.UpdateRateAsync(id, rate);
//        if (updatedRate == null)
//        {
//            return NotFound();
//        }

//        return NoContent();
//    }

//    // DELETE: api/Rate/5
//    [HttpDelete("remove/{id}")]
//    public async Task<IActionResult> DeleteAsync(int id)
//    {
//        var deleted = await _rateRepository.DeleteAsync(id);
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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RateController : ControllerBase
    {
        private readonly IRate _rateService;

        public RateController(IRate rateService)
        {
            _rateService = rateService;
        }

        // GET: api/rate/room/5
        [HttpGet("room/{roomId}")]
        public async Task<ActionResult<IEnumerable<RateDTO>>> GetRatesByRoomId(int roomId)
        {
            var rates = await _rateService.GetRatesByRoomIdAsync(roomId);
            return Ok(rates);
        }

        // POST: api/rate
        [HttpPost]
        public async Task<ActionResult> AddRate([FromBody] RateDTO rate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _rateService.AddAsync(rate);
            return CreatedAtAction(nameof(GetRatesByRoomId), new { roomId = rate.RoomId }, rate);
        }

        // PUT: api/rate
        [HttpPut]
        public async Task<ActionResult> UpdateRate([FromBody] RateDTO rate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _rateService.UpdateAsync(rate);
            return NoContent();
        }

        // DELETE: api/rate/5
        [HttpDelete("{rateId}")]
        public async Task<ActionResult> DeleteRate(int rateId)
        {
            await _rateService.DeleteAsync(rateId);
            return NoContent();
        }
    }
}
