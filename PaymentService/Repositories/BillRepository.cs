//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using PaymentService.Interface;
//using PaymentService.Models;

//namespace PaymentService.Repositories
//{
//    public class BillRepository : IBillRepository
//    {
//        private readonly PaymentDbContext _context;

//        public BillRepository(PaymentDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<Bill>> GetAllBillsAsync()
//        {
//            return await _context.Bills.Include(b => b.Payment).ToListAsync();
//        }

//        public async Task<Bill> GetBillByIdAsync(int id)
//        {
//            return await _context.Bills.Include(b => b.Payment)
//                                       .FirstOrDefaultAsync(b => b.BillId == id);
//        }

//        public async Task CreateBillAsync(Bill bill)
//        {
//            await _context.Bills.AddAsync(bill);
//            await _context.SaveChangesAsync();
//        }

//        public async Task UpdateBillAsync(Bill bill)
//        {
//            _context.Bills.Update(bill);
//            await _context.SaveChangesAsync();
//        }

//        public async Task DeleteBillAsync(int id)
//        {
//            var bill = await _context.Bills.FindAsync(id);
//            if (bill != null)
//            {
//                _context.Bills.Remove(bill);
//                await _context.SaveChangesAsync();
//            }
//        }
//    }

//}


using Microsoft.EntityFrameworkCore;
using PaymentService.Models;
using PaymentService.Interface;
namespace PaymentService.Repositories
{
    public class BillRepository : IBill
    {
        private readonly PaymentDbContext _context;

        public BillRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bill>> GetAllBillsAsync()
        {
            return await _context.Bills.ToListAsync();
        }

        public async Task<Bill> GetBillByIdAsync(int id)
        {
            return await _context.Bills.FindAsync(id);
        }

        public async Task CreateBillAsync(Bill bill)
        {
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBillAsync(Bill bill)
        {
            _context.Bills.Update(bill);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBillAsync(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill != null)
            {
                _context.Bills.Remove(bill);
                await _context.SaveChangesAsync();
            }
        }
    }
}