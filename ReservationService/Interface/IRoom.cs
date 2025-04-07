using System.Collections.Generic;
using System.Threading.Tasks;
using ReservationService.Models;
using ReservationService.ViewModel;

namespace ReservationService.Interface
{
    public interface IRoom
    {
        List<Room> GetAvailableRoomsByType(string roomType);
        Room GetRoomById(int id);
    }
}
