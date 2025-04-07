using System.Collections.Generic;
using System.Threading.Tasks;
using ReservationService.DTO; 
using ReservationService.Models;    

namespace ReservationService.Interface
{
    public interface IReservation
    {
        Task<IEnumerable<ReservationGetDTO>> GetAllReservationsAsync();
        Task<IEnumerable<ReservationGetDTO>> GetReservationByIdAsync(int reservationId);
        Task<IEnumerable<ReservationGetDTO>> GetActiveReservationAsync();
        Task<ReservationGetDTO> AddReservationAsync(ReservationGetDTO reservation);
        Task<ReservationGetDTO> UpdateReservationAsync(int reservationId, ReservationGetDTO reservation);
        Task DeleteReservationAsync(int reservationId);
    }
}
