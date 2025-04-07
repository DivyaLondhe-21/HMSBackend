using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentService.Models;

namespace PaymentService.Interface
{
    public interface IBill
    {
        Task<IEnumerable<Bill>> GetAllBillsAsync();
        Task<Bill> GetBillByIdAsync(int id);
        Task CreateBillAsync(Bill bill);
        Task UpdateBillAsync(Bill bill);
        Task DeleteBillAsync(int id);
    }

}
