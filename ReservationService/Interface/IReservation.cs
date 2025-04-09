using System.Collections.Generic;
using System.Threading.Tasks;
using ReservationService.DTO; 
using ReservationService.Models;
using ReservationService.ViewModel;

namespace ReservationService.Interface
{
    public interface IReservation
    {
        Task<IEnumerable<ReservationGetDTO>> GetAllReservationsAsync();
        Task<IEnumerable<ReservationGetDTO>> GetReservationByIdAsync(int reservationId);
        Task<IEnumerable<ReservationGetDTO>> GetActiveReservationAsync();
        Task<ReservationViewModel> AddReservationAsync(ReservationViewModel reservation);
        Task<ReservationViewModel> UpdateReservationAsync(int reservationId, ReservationViewModel reservation);
        Task DeleteReservationAsync(int reservationId);
    }
}
