//using RoomService.Models;

//namespace RoomService.Interface

//{
//    public interface IRoom
//    {
//        Task<IEnumerable<Room>> GetAllRoomsAsync();
//        Task<IEnumerable<Room>> GetAvailableRoomsAsync();
//        Task<Room> GetRoomByIdAsync(int id);
//        Task<Room> CreateRoomAsync(Room room);
//        Task<Room> UpdateRoomAsync(int id, Room room);
//        Task<bool> UpdateAvailabilityOfRoom(int id, bool isAvailable);
//        Task<bool> DeleteRoomAsync(int id);
//    }

//}
using RoomService.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomService.Interfaces
{
    public interface IRoom
    {
        Task<IEnumerable<RoomDTO>> GetAllAsync();
        Task<RoomDTO> GetByIdAsync(int id);
        Task AddAsync(RoomDTO room);
        Task UpdateAsync(RoomDTO room);
        Task DeleteAsync(int id);
    }
}

