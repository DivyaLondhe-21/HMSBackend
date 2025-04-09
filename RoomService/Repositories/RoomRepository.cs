//using Microsoft.EntityFrameworkCore;
//using RoomService.Models;
//using RoomService.Interface;

//namespace RoomService.Repositories
//{
//    public class RoomRepository : IRoom
//    {
//        private readonly RoomDbContext _context;

//        public RoomRepository(RoomDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
//        {
//            return await _context.Rooms.Include(r => r.Rates).ToListAsync();
//        }

//        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync()
//        {
//            return await _context.Rooms.Where(r=> r.Availability== true).ToListAsync();
//        }
//        public async Task<Room> GetRoomByIdAsync(int id)
//        {
//            return await _context.Rooms
//                .Include(r => r.Rates)
//                .FirstOrDefaultAsync(r => r.RoomID == id);
//        }

//        public async Task<Room> CreateRoomAsync(Room room)
//        {
//            _context.Rooms.Add(room);
//            await _context.SaveChangesAsync();
//            return room;
//        }

//        public async Task<Room> UpdateRoomAsync(int id, Room room)
//        {
//            var existingRoom = await _context.Rooms.FindAsync(id);
//            if (existingRoom == null)
//            {
//                return null;
//            }

//            existingRoom.RoomType = room.RoomType;
//            existingRoom.Period = room.Period;
//            existingRoom.CheckInDate = room.CheckInDate;
//            existingRoom.CheckOutDate = room.CheckOutDate;
//            existingRoom.Availability = room.Availability;

//            await _context.SaveChangesAsync();
//            return existingRoom;
//        }

//        public async Task<bool> UpdateAvailabilityOfRoom(int id, bool isAvailable)
//        {
//            var existingRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomID == id);
//            if (existingRoom == null)
//            {
//                return false;
//            }
//            else
//            {
//                if (existingRoom.Availability)
//                {
//                    return true;
//                }
//                else
//                {
//                    existingRoom.Availability = isAvailable;
//                    await _context.SaveChangesAsync();
//                    return true;
//                }
//            }
//        }

//        public async Task<bool> DeleteRoomAsync(int id)
//        {
//            var room = await _context.Rooms.FindAsync(id);
//            if (room == null)
//            {
//                return false;
//            }

//            _context.Rooms.Remove(room);
//            await _context.SaveChangesAsync();
//            return true;
//        }
//    }
//}

using Microsoft.EntityFrameworkCore;
using RoomService.DTO;
using RoomService.Interfaces;
using RoomService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Repositories
{
    public class RoomRepository : IRoom
    {
        private readonly RoomDbContext _context;

        public RoomRepository(RoomDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoomDTO>> GetAllAsync()
        {
            return await _context.Rooms.Select(r => new RoomDTO
            {
                RoomID = r.RoomID,
                RoomType = r.RoomType,
                Price = r.Price,
                Period = r.Period,
                Availability = r.Availability,
                GuestId = r.GuestId
            }).ToListAsync();
        }

        public async Task<RoomDTO> GetByIdAsync(int id)
        {
            var r = await _context.Rooms.FindAsync(id);
            if (r == null) return null;
            return new RoomDTO
            {
                RoomID = r.RoomID,
                RoomType = r.RoomType,
                Price = r.Price,
                Period = r.Period,
                Availability = r.Availability,
                GuestId = r.GuestId
            };
        }

        public async Task AddAsync(RoomDTO dto)
        {

            var r = new Room
            {
                RoomType = dto.RoomType,
                Price = dto.Price,
                Period = dto.Period,
                Availability = dto.Availability,
                GuestId = dto.GuestId
            };
            _context.Rooms.Add(r);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RoomDTO dto)
        {
            var r = await _context.Rooms.FindAsync(dto.RoomID);
            if (r != null)
            {
                r.RoomType = dto.RoomType;
                r.Price = dto.Price;
                r.Period = dto.Period;
                r.Availability = dto.Availability;
                r.GuestId = dto.GuestId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var r = await _context.Rooms.FindAsync(id);
            if (r != null)
            {
                _context.Rooms.Remove(r);
                await _context.SaveChangesAsync();
            }
        }
    }
}
