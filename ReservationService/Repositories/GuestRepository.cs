//using Microsoft.EntityFrameworkCore;
//using ReservationService.Models;
//using ReservationService.Interface;

//namespace ReservationService.Repositories
//{
//    public class GuestRepository : IGuest
//    {
//        private readonly ReservationDbContext _context;

//        public GuestRepository(ReservationDbContext context)
//        {
//            _context = context;
//        }

//        // Get all guests with filter for active guests
//        public IEnumerable<Guest> GetAllGuests(bool isActiveOnly)
//        {
//            if (isActiveOnly)
//            {
//                return _context.Guests
//                    .Include(g => g.Reservations)
//                    .Where(g => g.Reservations.Any(r => r.CheckOutDate >= DateTime.Today)) // Active if no checkout
//                    .ToList();
//            }
//            return _context.Guests.Include(g => g.Reservations).ToList();
//        }

//        // Get guest by ID
//        public Guest GetGuestById(int id)
//        {
//            return _context.Guests.Include(g => g.Reservations).FirstOrDefault(g => g.GuestId == id);
//        }

//        // Add a new guest
//        public void AddGuest(Guest guest)
//        {
//            _context.Guests.Add(guest);
//            _context.SaveChanges();
//        }

//        // Update guest information
//        public void UpdateGuest(Guest guest)
//        {
//            _context.Entry(guest).State = EntityState.Modified;
//            _context.SaveChanges();
//        }

//        // Delete guest
//        public void DeleteGuest(int id)
//        {
//            var guest = _context.Guests.Find(id);
//            if (guest != null)
//            {
//                _context.Guests.Remove(guest);
//                _context.SaveChanges();
//            }
//        }

//        // Check if guest exists
//        public bool GuestExists(int id)
//        {
//            return _context.Guests.Any(g => g.GuestId == id);
//        }
//    }
//}

using Microsoft.EntityFrameworkCore;
using ReservationService.Models;
using ReservationService.Interface;
using System.Collections;
namespace ReservationService.Repositories
{
    public class GuestRepository : IGuest
    {
        private readonly ReservationDbContext _context;
        public GuestRepository(ReservationDbContext context) => _context = context;

        public Guest AddGuest(Guest guest)
        {
            _context.Guests.Add(guest);
            _context.SaveChanges();
            return guest;
        }

        public IEnumerable<Guest> GetAllGuests()
        {
            return _context.Guests.ToList();
        }

        public Guest GetGuestById(int id)
        {
            return _context.Guests.FirstOrDefault(g => g.GuestId == id);
        }

        public void DeleteGuest(int id)
        {
            var guest = _context.Guests.FirstOrDefault(g => g.GuestId == id);
            if (guest != null)
            {
                _context.Guests.Remove(guest);
                _context.SaveChanges();
            }
        }

        public Guest UpdateGuest(Guest guest)
        {
            _context.Guests.Update(guest);
            _context.SaveChanges();
            return guest;
        }

        public bool GuestExists(int id)
        {
            var guestexists = _context.Guests.FirstOrDefault(g => g.GuestId == id);
            if (guestexists == null)
            {
                return false;
            }
            return true;
        }
    }
}
