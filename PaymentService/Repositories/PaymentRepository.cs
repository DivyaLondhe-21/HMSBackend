//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using PaymentService.Interface;
//using PaymentService.Models;

//namespace PaymentService.Repositories
//{
//    public class PaymentRepository : IPaymentRepository
//    {
//        private readonly PaymentDbContext _context;

//        public PaymentRepository(PaymentDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
//        {
//            return await _context.Payments.Include(p => p.Bills).ToListAsync();
//        }

//        public async Task<Payment> GetPaymentByIdAsync(int id)
//        {
//            return await _context.Payments.Include(p => p.Bills)
//                                          .FirstOrDefaultAsync(p => p.PaymentId == id);
//        }

//        public async Task<IEnumerable<Payment>> GetPaymentsByDate(DateTime date)
//        {
//            return await _context.Payments
//                .Where(p => p.PayTime.Date == date.Date)
//                .Include(p => p.Bills)
//                .ToListAsync();
//        }

//        public async Task<IEnumerable<Payment>> GetPaymentsByMonth(int month, int year)
//        {
//            return await _context.Payments
//                .Where(p => p.PayTime.Month == month && p.PayTime.Year == year)
//                .Include(p => p.Bills)
//                .ToListAsync();
//        }
//        public async Task<Bill> GenerateBill(int paymentId)
//        {
//            var payment = await _context.Payments.FindAsync(paymentId);
//            if (payment == null)
//            {
//                return null;
//            }

//            var bill = new Bill
//            {
//                Quantity = 1, // Default quantity
//                Price = payment.Amount,
//                Taxes = payment.Amount * 0.18m, // Assuming 18% GST for now
//                Date = DateTime.Now,
//                Service = "Room Booking", // Default service, modify if needed
//                Unit = "N/A",
//                PaymentId = payment.PaymentId
//            };

//            _context.Bills.Add(bill);
//            await _context.SaveChangesAsync();
//            return bill;
//        }

//        public async Task CreatePaymentAsync(Payment payment)
//        {
//            await _context.Payments.AddAsync(payment);
//            await _context.SaveChangesAsync();
//        }

//        public async Task UpdatePaymentAsync(Payment payment)
//        {
//            _context.Payments.Update(payment);
//            await _context.SaveChangesAsync();
//        }

//        public async Task DeletePaymentAsync(int id)
//        {
//            var payment = await _context.Payments.FindAsync(id);
//            if (payment != null)
//            {
//                _context.Payments.Remove(payment);
//                await _context.SaveChangesAsync();
//            }
//        }
//    }

//}


using Microsoft.EntityFrameworkCore;
using PaymentService.Interface;
using PaymentService.Models;
namespace PaymentService.Repositories
{
    public class PaymentRepository : IPayment
    {
        private readonly PaymentDbContext _context;

        public PaymentRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _context.Payments.Include(p => p.Bills).ToListAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await _context.Payments.Include(p => p.Bills)
                                          .FirstOrDefaultAsync(p => p.PaymentId == id);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByDate(DateTime date)
        {
            return await _context.Payments.Where(p => p.PayTime.Date == date.Date).ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByMonth(int month, int year)
        {
            return await _context.Payments
                .Where(p => p.PayTime.Month == month && p.PayTime.Year == year)
                .ToListAsync();
        }

        public async Task<Bill> GenerateBill(int paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);

            if (payment == null)
                return null;

            var bill = new Bill
            {
                Quantity = 1,
                Price = payment.Amount, // or split if multiple items
                Taxes = payment.Amount * 0.18M, // example: 18% tax
                Date = DateTime.Now,
                Service = "Room Booking", // or fetch from reservation/payment details
                Unit = "Night",
                PaymentId = payment.PaymentId
            };

            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();

            return bill;
        }

        public async Task CreatePaymentAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePaymentAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }
    }
}