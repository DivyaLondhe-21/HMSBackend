//using RoomService.Models;

//namespace RoomService.Interface
//{
//    public interface IRate
//    {
//        Task<IEnumerable<Rate>> GetAllRatesAsync();
//        Task<Rate> GetRateByIdAsync(int id);
//        Task<Rate> CreateRateAsync(Rate rate);
//        Task<Rate> UpdateRateAsync(int id, Rate rate);
//        Task<bool> DeleteRateAsync(int id);
//    }
//}

using RoomService.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomService.Interfaces
{
    public interface IRate
    {
        Task<IEnumerable<RateDTO>> GetAllRatesAsync();
        Task<IEnumerable<RateDTO>> GetRatesByRoomIdAsync(int roomId);
        Task AddAsync(RateDTO rate);
        Task UpdateAsync(RateDTO rate);
        Task DeleteAsync(int rateId);
    }
}


