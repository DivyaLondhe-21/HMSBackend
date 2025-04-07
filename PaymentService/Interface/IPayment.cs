using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentService.Models;

namespace PaymentService.Interface
{
    public interface IPayment
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> GetPaymentByIdAsync(int id);
        Task<IEnumerable<Payment>> GetPaymentsByDate(DateTime date);
        Task<IEnumerable<Payment>> GetPaymentsByMonth(int month, int year);
        Task<Bill> GenerateBill(int paymentId);

        Task CreatePaymentAsync(Payment payment);
        Task UpdatePaymentAsync(Payment payment);
        Task DeletePaymentAsync(int id);
    }

}
