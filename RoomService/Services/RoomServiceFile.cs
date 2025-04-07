//using RoomService.Interfaces;
//using RoomService.Models;

//namespace RoomService.Services
//{
//    public class RoomServiceFile
//    {
//        private readonly IRoom _roomRepository;

//        public RoomServiceFile(IRoom roomRepository)
//        {
//            _roomRepository = roomRepository;
//        }

//        public Task<IEnumerable<Room>> GetAllRooms() => _roomRepository.GetAllRoomsAsync();
//        public Task<Room> GetRoomById(int id) => _roomRepository.GetRoomByIdAsync(id);
//        public Task<Room> CreateRoom(Room room) => _roomRepository.CreateRoomAsync(room);
//        public Task<Room> UpdateRoom(int id, Room room) => _roomRepository.UpdateRoomAsync(id, room);
//        public Task<bool> DeleteRoom(int id) => _roomRepository.DeleteRoomAsync(id);
//    }

//}
