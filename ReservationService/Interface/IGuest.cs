using System.Collections.Generic;
using System.Threading.Tasks;
using ReservationService.Models;
using ReservationService.ViewModel;

namespace ReservationService.Interface
{
    public interface IGuest
    {
        IEnumerable<Guest> GetAllGuests();
        Guest GetGuestById(int id);
        Guest AddGuest(Guest guest);
        Guest UpdateGuest(Guest guest);
        void DeleteGuest(int id);
        bool GuestExists(int id);
    }
}
