//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc;
//using PaymentService.Interface;
//using PaymentService.Models;
//using ReservationService.Models;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace PaymentService.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PaymentController : ControllerBase
//    {
//        private readonly IPaymentRepository _paymentRepository;

//        public PaymentController(IPaymentRepository paymentRepository)
//        {
//            _paymentRepository = paymentRepository;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
//        {
//            var payments = await _paymentRepository.GetAllPaymentsAsync();
//            return Ok(payments);
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Payment>> GetPayment(int id)
//        {
//            var payment = await _paymentRepository.GetPaymentByIdAsync(id);
//            if (payment == null)
//            {
//                return NotFound();
//            }
//            return Ok(payment);
//        }

//        [HttpGet("filter")]
//        public async Task<ActionResult<IEnumerable<Payment>>> GetFilteredPayments([FromQuery] string filterType)
//        {
//            IEnumerable<Payment> payments;

//            if (filterType == "today")
//            {
//                payments = await _paymentRepository.GetPaymentsByDate(DateTime.Today);
//            }
//            else if (filterType == "month")
//            {
//                payments = await _paymentRepository.GetPaymentsByMonth(DateTime.Now.Month, DateTime.Now.Year);
//            }
//            else
//            {
//                return BadRequest("Invalid filter type. Use 'today' or 'month'.");
//            }

//            return Ok(payments);
//        }


//        [HttpPost]
//        public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
//        {
//            await _paymentRepository.CreatePaymentAsync(payment);
//            return CreatedAtAction(nameof(GetPayment), new { id = payment.PaymentId }, payment);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdatePayment(int id, Payment payment)
//        {
//            if (id != payment.PaymentId)
//            {
//                return BadRequest();
//            }

//            await _paymentRepository.UpdatePaymentAsync(payment);
//            return NoContent();
//        }

//        [HttpPost("{id}/generate-bill")]
//        public async Task<IActionResult> GenerateBill(int id)
//        {
//            var bill = await _paymentRepository.GenerateBill(id);
//            if (bill == null)
//            {
//                return NotFound("Payment not found.");
//            }
//            return Ok(new { message = "Bill generated successfully.", bill });
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeletePayment(int id)
//        {
//            var payment = await _paymentRepository.GetPaymentByIdAsync(id);
//            if (payment == null)
//            {
//                return NotFound();
//            }

//            await _paymentRepository.DeletePaymentAsync(id);
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
    public class PaymentController : ControllerBase
    {
        private readonly IPayment _paymentRepo;

        public PaymentController(IPayment paymentRepo)
        {
            _paymentRepo = paymentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _paymentRepo.GetAllPaymentsAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await _paymentRepo.GetPaymentByIdAsync(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            var payments = await _paymentRepo.GetPaymentsByDate(date);
            return Ok(payments);
        }

        [HttpGet("month/{month}/year/{year}")]
        public async Task<IActionResult> GetByMonth(int month, int year)
        {
            var payments = await _paymentRepo.GetPaymentsByMonth(month, year);
            return Ok(payments);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PaymentDTO dto)
        {
            var payment = new Payment
            {
                Amount = dto.Amount,
                PayTime = dto.PayTime,
                CreditCardNumber = dto.CreditCardNumber,
                CreditCardType = dto.CreditCardType,
                Cvv = dto.Cvv,
                CardHolderName = dto.CardHolderName,
                CreditExpiryDate = dto.CreditExpiryDate,
                ReservationId = dto.ReservationId
            };

            await _paymentRepo.CreatePaymentAsync(payment);
            return Ok(payment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PaymentDTO dto)
        {
            var payment = await _paymentRepo.GetPaymentByIdAsync(id);
            if (payment == null) return NotFound();

            payment.Amount = dto.Amount;
            payment.PayTime = dto.PayTime;
            payment.CreditCardNumber = dto.CreditCardNumber;
            payment.CreditCardType = dto.CreditCardType;
            payment.Cvv = dto.Cvv;
            payment.CardHolderName = dto.CardHolderName;
            payment.CreditExpiryDate = dto.CreditExpiryDate;
            payment.ReservationId = dto.ReservationId;

            await _paymentRepo.UpdatePaymentAsync(payment);
            return Ok(payment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _paymentRepo.DeletePaymentAsync(id);
            return NoContent();
        }
    }
}