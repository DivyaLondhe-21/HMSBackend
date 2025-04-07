//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using ReservationService.Models;
//using ReservationService.Interface;
//using Microsoft.AspNetCore.Http.HttpResults;

//namespace ReservationService.Repositories
//{
//    public class RoomRepository : IRoom
//    {
//        private readonly ReservationDbContext _context;

//        public RoomRepository(ReservationDbContext context)
//        {
//            _context = context;
//        }

//      public async Task<IEnumerable<Room>> GetRoomRatesAsync()
//        {
//            return await _context.Reservations.Include(r=> r.Room).ToListAsync();
//        }*/

//        public Rate GetRoomById(int RoomId)
//        {
//            var rates = _context.Rates.FirstOrDefault(r => r.RoomId == RoomId);
//            if(rates == null)
//            {
//                return null;
//            }
//            return rates;
//        }



//        /*public async Task<Reservation> AddReservationAsync(Reservation reservation)
//        {
//            reservation.CalculateNumOfNights();
//            await _context.Reservations.AddAsync(reservation);
//            await _context.SaveChangesAsync();
//            return reservation;
//        }

//        public async Task UpdateReservationAsync(Reservation reservation)
//        {
//            _context.Reservations.Update(reservation);
//            await _context.SaveChangesAsync();
//        }

//        public async Task DeleteReservationAsync(int ReservationId)
//        {
//            var reservation = await _context.Reservations.FindAsync(ReservationId);
//            if (reservation != null)
//            {
//                _context.Reservations.Remove(reservation);
//                await _context.SaveChangesAsync();
//            }
//        }
//    }
//}

using ReservationService.Models;
using ReservationService.Interface;
using System.Collections.Generic;
using System.Linq;

namespace ReservationService.Repositories
{
    public class RoomRepository : IRoom
    {
        private readonly ReservationDbContext _context;
        public RoomRepository(ReservationDbContext context) => _context = context;

        public List<Room> GetAvailableRoomsByType(string roomType)
        {
            return _context.Rooms
                .Where(r => r.RoomType == roomType && r.Availability)
                .ToList();
        }

        public Room GetRoomById(int id)
        {
            return _context.Rooms.FirstOrDefault(r => r.RoomID == id);
        }
    }
}
