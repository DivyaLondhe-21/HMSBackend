//using Microsoft.EntityFrameworkCore;
//using RoomService.Models;
//using RoomService.Interface;

//namespace RoomService.Repositories
//{
//    public class RateRepository : IRate
//    {
//        private readonly RoomDbContext _context;

//        public RateRepository(RoomDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<Rate>> GetAllRatesAsync()
//        {
//            return await _context.Rates.Include(r => r.Room).ToListAsync();
//        }

//        public async Task<Rate> GetRateByIdAsync(int id)
//        {
//            return await _context.Rates
//                .Include(r => r.Room)
//                .FirstOrDefaultAsync(r => r.RateId == id);
//        }

//        public async Task<Rate> CreateRateAsync(Rate rate)
//        {
//            _context.Rates.Add(rate);
//            await _context.SaveChangesAsync();
//            return rate;
//        }

//        public async Task<Rate> UpdateRateAsync(int id, Rate rate)
//        {
//            var existingRate = await _context.Rates.FindAsync(id);
//            if (existingRate == null)
//            {
//                return null;
//            }

//            existingRate.FirstNightPrice = rate.FirstNightPrice;
//            existingRate.ExtensionPrice = rate.ExtensionPrice;
//            existingRate.NumberOfChildren = rate.NumberOfChildren;
//            existingRate.NumberOfGuests = rate.NumberOfGuests;
//            existingRate.NumberOfDays = rate.NumberOfDays;

//            await _context.SaveChangesAsync();
//            return existingRate;
//        }

//        public async Task<bool> DeleteRateAsync(int id)
//        {
//            var rate = await _context.Rates.FindAsync(id);
//            if (rate == null)
//            {
//                return false;
//            }

//            _context.Rates.Remove(rate);
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
    public class RateRepository : IRate
    {
        private readonly RoomDbContext _context;

        public RateRepository(RoomDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<RateDTO>> GetAllRatesAsync()
        {
            return await _context.Rates
                .Select(rate => new RateDTO
                {
                    RateId = rate.RateId,
                    FirstNightPrice = rate.FirstNightPrice,
                    ExtensionPrice = rate.ExtensionPrice,
                    NumberOfChildren = rate.NumberOfChildren,
                    NumberOfGuests = rate.NumberOfGuests,
                    NumberOfDays = rate.NumberOfDays,
                    RoomId = rate.RoomId
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<RateDTO>> GetRatesByRoomIdAsync(int roomId)
        {
            return await _context.Rates
                .Where(r => r.RoomId == roomId)
                .Select(r => new RateDTO
                {
                    RateId = r.RateId,
                    FirstNightPrice = r.FirstNightPrice,
                    ExtensionPrice = r.ExtensionPrice,
                    NumberOfChildren = r.NumberOfChildren,
                    NumberOfGuests = r.NumberOfGuests,
                    NumberOfDays = r.NumberOfDays,
                    //Discount = r.Discount,
                    RoomId = r.RoomId
                }).ToListAsync();
        }

        public async Task AddAsync(RateDTO dto)
        {
            var rate = new Rate
            {
                FirstNightPrice = dto.FirstNightPrice,
                ExtensionPrice = dto.ExtensionPrice,
                NumberOfChildren = dto.NumberOfChildren,
                NumberOfGuests = dto.NumberOfGuests,
                NumberOfDays = dto.NumberOfDays,
                //Discount = dto.Discount,
                RoomId = dto.RoomId
            };
            _context.Rates.Add(rate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RateDTO dto)
        {
            var rate = await _context.Rates.FindAsync(dto.RateId);
            if (rate != null)
            {
                rate.FirstNightPrice = dto.FirstNightPrice;
                rate.ExtensionPrice = dto.ExtensionPrice;
                rate.NumberOfChildren = dto.NumberOfChildren;
                rate.NumberOfGuests = dto.NumberOfGuests;
                rate.NumberOfDays = dto.NumberOfDays;
                //rate.Discount = dto.Discount;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int rateId)
        {
            var rate = await _context.Rates.FindAsync(rateId);
            if (rate != null)
            {
                _context.Rates.Remove(rate);
                await _context.SaveChangesAsync();
            }
        }
    }
}
