//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using PaymentService.Interface;
//using PaymentService.Models;
//using System.Collections.Generic;
//using System.Threading.Tasks;


//namespace PaymentService.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BillController : ControllerBase
//    {
//        private readonly IBillRepository _billRepository;

//        public BillController(IBillRepository billRepository)
//        {
//            _billRepository = billRepository;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Bill>>> GetBills()
//        {
//            var bills = await _billRepository.GetAllBillsAsync();
//            return Ok(bills);
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Bill>> GetBill(int id)
//        {
//            var bill = await _billRepository.GetBillByIdAsync(id);
//            if (bill == null)
//            {
//                return NotFound();
//            }
//            return Ok(bill);
//        }

//        [HttpPost]
//        public async Task<ActionResult<Bill>> CreateBill(Bill bill)
//        {
//            await _billRepository.CreateBillAsync(bill);
//            return CreatedAtAction(nameof(GetBill), new { id = bill.BillId }, bill);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateBill(int id, Bill bill)
//        {
//            if (id != bill.BillId)
//            {
//                return BadRequest();
//            }

//            await _billRepository.UpdateBillAsync(bill);
//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteBill(int id)
//        {
//            var bill = await _billRepository.GetBillByIdAsync(id);
//            if (bill == null)
//            {
//                return NotFound();
//            }

//            await _billRepository.DeleteBillAsync(id);
//            return NoContent();
//        }
//    }

//}

using Microsoft.AspNetCore.Mvc;
using PaymentService.DTO;
using PaymentService.Interface;
using PaymentService.Models;
namespace PaymentService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBill _billRepo;

        public BillController(IBill billRepo)
        {
            _billRepo = billRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bills = await _billRepo.GetAllBillsAsync();
            return Ok(bills);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bill = await _billRepo.GetBillByIdAsync(id);
            if (bill == null) return NotFound();
            return Ok(bill);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BillDTO dto)
        {
            var bill = new Bill
            {
                Quantity = dto.Quantity,
                Price = dto.Price,
                Taxes = dto.Taxes,
                Date = dto.Date,
                Service = dto.Service,
                Unit = dto.Unit,
                PaymentId = dto.PaymentId
            };

            await _billRepo.CreateBillAsync(bill);
            return Ok(bill);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BillDTO dto)
        {
            var bill = await _billRepo.GetBillByIdAsync(id);
            if (bill == null) return NotFound();

            bill.Quantity = dto.Quantity;
            bill.Price = dto.Price;
            bill.Taxes = dto.Taxes;
            bill.Date = dto.Date;
            bill.Service = dto.Service;
            bill.Unit = dto.Unit;
            bill.PaymentId = dto.PaymentId;

            await _billRepo.UpdateBillAsync(bill);
            return Ok(bill);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _billRepo.DeleteBillAsync(id);
            return NoContent();
        }
    }
}