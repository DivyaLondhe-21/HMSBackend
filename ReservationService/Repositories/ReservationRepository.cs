//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using ReservationService.Models;
//using ReservationService.Interface;
//using ReservationService.ViewModel;

//namespace ReservationService.Repositories
//{
//    public class ReservationRepository : IReservation
//    {
//        private readonly ReservationDbContext _context;
//        private readonly IRoom _roomRepository;

//        public ReservationRepository(ReservationDbContext context, IRoom roomRepository)
//        {
//            _context = context;
//            _roomRepository = roomRepository;
//        }

//        public async Task<IEnumerable<ReservationGetDTO>> GetAllReservationsAsync()
//        {
//             var reservation = await _context.Reservations
//                                            .Include(r => r.Guest) // Include Guest to fetch guest details
//                                            .Select(r => new ReservationGetDTO
//                                            {
//                                               ReservationId = r.ReservationId,
//                                                GuestName = r.Guest.Name,
//                                                GuestPhoneNumber = r.Guest.PhoneNumber,
//                                                GuestEmail = r.Guest.Email,
//                                                NumberOfAdults = r.NumberOfAdults,
//                                                NumberOfChildren = r.NumberOfChildren,
//                                                CheckInDate = r.CheckInDate,
//                                                CheckOutDate = r.CheckOutDate,
//                                                NumberOfNights = r.NumberOfNights,
//                                                RoomId = r.RoomId
//                                            })
//                                            .ToListAsync();
//            return reservation;
//        }

//        public async Task<IEnumerable<ReservationGetDTO>> GetReservationByIdAsync(int ReservationId)
//        {

//                var reservation = await _context.Reservations
//                                            .Include(r => r.Guest) // Include Guest to fetch guest details
//                                            .Where(r => r.ReservationId == ReservationId)
//                                            .Select(r => new ReservationGetDTO
//                                            {
//                                                ReservationId = r.ReservationId,
//                                                GuestName = r.Guest.Name,
//                                                GuestPhoneNumber = r.Guest.PhoneNumber,
//                                                GuestEmail = r.Guest.Email,
//                                                NumberOfAdults = r.NumberOfAdults,
//                                                NumberOfChildren = r.NumberOfChildren,
//                                                CheckInDate = r.CheckInDate,
//                                                CheckOutDate = r.CheckOutDate,
//                                                NumberOfNights = r.NumberOfNights,
//                                                RoomId = r.RoomId
//                                            })
//                                            .ToListAsync();
//            return reservation;
//        }

//        public async Task<IEnumerable<ReservationGetDTO>> GetActiveReservationAsync()
//        {
//          var reservation = await _context.Reservations
//                                            .Include(r => r.Room)
//                                            .Include(r => r.Guest) // Include Guest to fetch guest details
//                                            .Where(r => r.CheckInDate <= DateTime.Today && r.CheckOutDate >= DateTime.Today)
//                                            .Select(r => new ReservationGetDTO
//                                            {
//                                                ReservationId = r.ReservationId,
//                                                GuestName = r.Guest.Name,
//                                                GuestPhoneNumber = r.Guest.PhoneNumber,
//                                                GuestEmail = r.Guest.Email,
//                                                NumberOfAdults = r.NumberOfAdults,
//                                                NumberOfChildren = r.NumberOfChildren,
//                                                CheckInDate = r.CheckInDate,
//                                                CheckOutDate = r.CheckOutDate,
//                                                NumberOfNights = r.NumberOfNights,
//                                                RoomId = r.RoomId
//                                            })
//                                            .ToListAsync();

//            return reservation;
//        }
//        //public async Task<ReservationViewModel> AddReservationAsync(ReservationViewModel reservation)
//        //{
//        //    int numberOfDays = reservation.CalculateNumOfNights();

//        //    var rate =  _roomRepository.GetRoomById(reservation.RoomId);
//        //    reservation.Price = reservation.CalculatePrice(rate.FirstNightPrice, numberOfDays);

//        //    var guestInfo = new Guest
//        //    {
//        //        Name = reservation.Name,
//        //        PhoneNumber = reservation.PhoneNumber,
//        //        Address = reservation.Address,
//        //        Company = reservation.Company,
//        //        Email = reservation.Email,
//        //        Gender = reservation.Gender
//        //    };

//        //    await _context.Guests.AddAsync(guestInfo);
//        //    await _context.SaveChangesAsync(); // Save guest info first to get GuestId


//        //    var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Email == reservation.Email);
//        //    if (guest == null)
//        //    {
//        //        throw new KeyNotFoundException("Guest not found after saving.");
//        //    }

//        //    var reservationEntity = new Reservation
//        //    {
//        //        NumberOfAdults = reservation.NumberOfAdults,
//        //        NumberOfChildren = reservation.NumberOfChildren,
//        //        NumberOfNights = numberOfDays,
//        //        CheckInDate = reservation.CheckInDate,
//        //        CheckOutDate = reservation.CheckOutDate,
//        //        RoomId = reservation.RoomId,
//        //        GuestId = guest.GuestId
//        //    };

//        //    await _context.Reservations.AddAsync(reservationEntity);

//        //    // Update room status and availability
//        //    var roomToUpdate = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomID == reservation.RoomId);

//        //    if (roomToUpdate != null)
//        //    {
//        //        roomToUpdate.CheckInDate = reservation.CheckInDate;
//        //        roomToUpdate.CheckOutDate = reservation.CheckOutDate;
//        //        roomToUpdate.GuestId = guest.GuestId; // Correctly updating guestId in room
//        //        roomToUpdate.Availability = false;
//        //        roomToUpdate.Period = numberOfDays.ToString();
//        //    }
//        //    _context.Rooms.Update(roomToUpdate);
//        //    await _context.SaveChangesAsync();

//        //    return reservation;
//        //}

//        public async Task<ReservationViewModel> AddReservationAsync(ReservationViewModel reservation)
//        {
//            int numberOfDays = reservation.CalculateNumOfNights();

//            var rate = _roomRepository.GetRoomById(reservation.RoomId);
//            if (rate == null)
//            {
//                throw new KeyNotFoundException("Room not found.");
//            }

//            reservation.Price = reservation.CalculatePrice(rate.FirstNightPrice, numberOfDays);

//            var guestInfo = new Guest
//            {
//                Name = reservation.Name,
//                PhoneNumber = reservation.PhoneNumber,
//                Address = reservation.Address,
//                Company = reservation.Company,
//                Email = reservation.Email,
//                Gender = reservation.Gender
//            };

//            await _context.Guests.AddAsync(guestInfo);
//            var changes = await _context.SaveChangesAsync();
//            if (changes == 0)
//            {
//                throw new InvalidOperationException("Failed to save guest.");
//            }

//            var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Email == reservation.Email);
//            if (guest == null)
//            {
//                throw new KeyNotFoundException("Guest not found after saving.");
//            }

//            var reservationEntity = new Reservation
//            {
//                NumberOfAdults = reservation.NumberOfAdults,
//                NumberOfChildren = reservation.NumberOfChildren,
//                NumberOfNights = numberOfDays,
//                CheckInDate = reservation.CheckInDate,
//                CheckOutDate = reservation.CheckOutDate,
//                RoomId = reservation.RoomId,
//                GuestId = guest.GuestId
//            };

//            await _context.Reservations.AddAsync(reservationEntity);

//            var roomToUpdate = await _context.Rooms.FindAsync(reservation.RoomId);
//            if (roomToUpdate == null)
//            {
//                // Log more detailed information here if necessary.
//                throw new KeyNotFoundException($"Room with RoomId {reservation.RoomId} not found.");
//            }

//            roomToUpdate.CheckInDate = reservation.CheckInDate;
//            roomToUpdate.CheckOutDate = reservation.CheckOutDate;
//            roomToUpdate.GuestId = guest.GuestId;
//            roomToUpdate.Availability = false;
//            roomToUpdate.Period = numberOfDays.ToString();

//            _context.Rooms.Update(roomToUpdate); // Update the room entity

//            await _context.SaveChangesAsync();

//            return reservation;
//        }


//        /*        public async Task UpdateReservationAsync(Reservation reservation)
//                {
//                    _context.Reservations.Update(reservation);
//                    await _context.SaveChangesAsync();
//                }*/

//        public async Task<ReservationViewModel> UpdateReservationAsync(int reservationId, ReservationViewModel updatedReservation)
//        {

//            var reservationEntity = await _context.Reservations
//                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);

//            if (reservationEntity == null)
//            {
//                throw new KeyNotFoundException("Reservation not found.");
//            }


//            reservationEntity.NumberOfAdults = updatedReservation.NumberOfAdults;
//            reservationEntity.NumberOfChildren = updatedReservation.NumberOfChildren;
//            reservationEntity.CheckInDate = updatedReservation.CheckInDate;
//            reservationEntity.CheckOutDate = updatedReservation.CheckOutDate;


//            int numberOfDays = updatedReservation.CalculateNumOfNights();
//            reservationEntity.NumberOfNights = numberOfDays;

//            var rate =  _roomRepository.GetRoomById(updatedReservation.RoomId);
//            updatedReservation.Price = updatedReservation.CalculatePrice(rate.FirstNightPrice, numberOfDays);


//            var guest = await _context.Guests
//                .FirstOrDefaultAsync(g => g.Email == updatedReservation.Email);

//            if (guest != null)
//            {
//                guest.Name = updatedReservation.Name;
//                guest.PhoneNumber = updatedReservation.PhoneNumber;
//                guest.Address = updatedReservation.Address;
//                guest.Company = updatedReservation.Company;
//                guest.Gender = updatedReservation.Gender;
//            }
//            else
//            {
//                throw new KeyNotFoundException("Guest not found for the provided email.");
//            }


//            var roomEntity = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomID == updatedReservation.RoomId);
//            if (roomEntity != null)
//            {
//                roomEntity.CheckInDate = updatedReservation.CheckInDate;
//                roomEntity.CheckOutDate = updatedReservation.CheckOutDate;
//                roomEntity.GuestId = guest.GuestId;
//                roomEntity.Availability = false;
//                roomEntity.Period = numberOfDays.ToString();
//            }
//            else
//            {
//                throw new KeyNotFoundException("Room not found.");
//            }


//            await _context.SaveChangesAsync();

//            return updatedReservation;
//        }

//        public async Task DeleteReservationAsync(int ReservationId)
//        {
//            var reservation = await _context.Reservations.FindAsync(ReservationId);
//            if (reservation != null)
//            {
//                var roomEntity = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomID == reservation.RoomId);
//                if (roomEntity != null)
//                {
//                     roomEntity.Availability = true;

//                }
//                _context.Reservations.Remove(reservation);
//                await _context.SaveChangesAsync();
//            }
//        }
//    }
//}

using Microsoft.EntityFrameworkCore;
using ReservationService.Interface;
using ReservationService.Models;
using ReservationService.DTO; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReservationService.ViewModel;

namespace ReservationService.Repository
{
    public class ReservationRepository : IReservation
    {
        private readonly ReservationDbContext _context;

        public ReservationRepository(ReservationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReservationGetDTO>> GetAllReservationsAsync()
        {

            return await _context.Reservations
                  .Include(r => r.Guest)
                .Select(r => new ReservationGetDTO
                {
                    ReservationId = r.ReservationId,
                    NumberOfAdults = r.NumberOfAdults,
                    NumberOfChildren = r.NumberOfChildren,
                    CheckInDate = r.CheckInDate,
                    CheckOutDate = r.CheckOutDate,
                    NumberOfNights = r.NumberOfNights,
                    RoomId = r.RoomId,
                    GuestId = r.GuestId,
                    GuestName = r.Guest.Name,
                    GuestEmail = r.Guest.Email,
                    GuestPhoneNumber = r.Guest.PhoneNumber
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ReservationGetDTO>> GetReservationByIdAsync(int reservationId)
        {
            return await _context.Reservations
                .Include(r=> r.Guest)
                .Where(r => r.ReservationId == reservationId)
                .Select(r => new ReservationGetDTO
                {
                    ReservationId = r.ReservationId,
                    NumberOfAdults = r.NumberOfAdults,
                    NumberOfChildren = r.NumberOfChildren,
                    CheckInDate = r.CheckInDate,
                    CheckOutDate = r.CheckOutDate,
                    NumberOfNights = r.NumberOfNights,
                    RoomId = r.RoomId,
                    GuestId = r.GuestId,
                    GuestName = r.Guest.Name,
                    GuestEmail = r.Guest.Email,
                    GuestPhoneNumber = r.Guest.PhoneNumber
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ReservationGetDTO>> GetActiveReservationAsync()
        {
            return await _context.Reservations
                .Include(r => r.Guest)
                .Where(r => r.CheckOutDate > System.DateTime.Now)
                .Select(r => new ReservationGetDTO
                {
                    ReservationId = r.ReservationId,
                    NumberOfAdults = r.NumberOfAdults,
                    NumberOfChildren = r.NumberOfChildren,
                    CheckInDate = r.CheckInDate,
                    CheckOutDate = r.CheckOutDate,
                    NumberOfNights = r.NumberOfNights,
                    RoomId = r.RoomId,
                    GuestId = r.GuestId,
                    GuestName = r.Guest.Name,
                    GuestEmail = r.Guest.Email,
                    GuestPhoneNumber = r.Guest.PhoneNumber
                })
                .ToListAsync();
        }

        public async Task<ReservationViewModel> AddReservationAsync(ReservationViewModel reservationDto)
        {
            var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Email == reservationDto.Email);

            if (guest == null)
            {
                guest = new Guest
                {
                    Name = reservationDto.Name,
                    Email = reservationDto.Email,
                    PhoneNumber = reservationDto.PhoneNumber,
                    Company = reservationDto.Company,
                    Gender = reservationDto.Gender,
                    Address = reservationDto.Address
                };

                _context.Guests.Add(guest);
                await _context.SaveChangesAsync(); // Save to get the GuestId
            }

            // 2. Save Reservation Info
            var reservation = new Reservation
            {
                NumberOfAdults = reservationDto.NumberOfAdults,
                NumberOfChildren = reservationDto.NumberOfChildren,
                CheckInDate = reservationDto.CheckInDate,
                CheckOutDate = reservationDto.CheckOutDate,
                NumberOfNights = reservationDto.NumberOfNights,
                RoomId = reservationDto.RoomId,
                GuestId = guest.GuestId
            };

            _context.Reservations.Add(reservation);

            // 3. Update Room Info
            var room = await _context.Rooms.FindAsync(reservationDto.RoomId);
            if (room != null)
            {
                room.Availability = false;
                room.CheckInDate = reservationDto.CheckInDate;
                room.CheckOutDate = reservationDto.CheckOutDate;
                room.GuestId = guest.GuestId;
            }

            await _context.SaveChangesAsync();

            // 4. Return Result
            reservationDto.ReservationId = reservation.ReservationId;
            reservationDto.GuestId = guest.GuestId;
            return reservationDto;
            /*var reservation = new Reservation
            {
                NumberOfAdults = reservationDto.NumberOfAdults,
                NumberOfChildren = reservationDto.NumberOfChildren,
                CheckInDate = reservationDto.CheckInDate,
                CheckOutDate = reservationDto.CheckOutDate,
                NumberOfNights = reservationDto.NumberOfNights,
                RoomId = reservationDto.RoomId,
                GuestId = reservationDto.GuestId
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            reservationDto.ReservationId = reservation.ReservationId;
            return reservationDto;*/
        }

        public async Task<ReservationViewModel> UpdateReservationAsync(int reservationId, ReservationViewModel reservationDto)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null) return null;

            // Update Reservation fields
            reservation.NumberOfAdults = reservationDto.NumberOfAdults;
            reservation.NumberOfChildren = reservationDto.NumberOfChildren;
            reservation.CheckInDate = reservationDto.CheckInDate;
            reservation.CheckOutDate = reservationDto.CheckOutDate;
            reservation.NumberOfNights = reservationDto.NumberOfNights;
            reservation.RoomId = reservationDto.RoomId;

            // Update Guest info
            var guest = await _context.Guests.FindAsync(reservation.GuestId);
            if (guest != null)
            {
                guest.Name = reservationDto.Name;
                guest.Email = reservationDto.Email;
                guest.PhoneNumber = reservationDto.PhoneNumber;
                guest.Company = reservationDto.Company;
                guest.Gender = reservationDto.Gender;
                guest.Address = reservationDto.Address;
            }

            // Update Room Info
            var room = await _context.Rooms.FindAsync(reservationDto.RoomId);
            if (room != null)
            {
                room.Availability = false;
                room.CheckInDate = reservationDto.CheckInDate;
                room.CheckOutDate = reservationDto.CheckOutDate;
                room.GuestId = guest?.GuestId ?? reservation.GuestId;
            }

            await _context.SaveChangesAsync();
            return reservationDto;
            /*var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null) return null;

            reservation.NumberOfAdults = reservationDto.NumberOfAdults;
            reservation.NumberOfChildren = reservationDto.NumberOfChildren;
            reservation.CheckInDate = reservationDto.CheckInDate;
            reservation.CheckOutDate = reservationDto.CheckOutDate;
            reservation.NumberOfNights = reservationDto.NumberOfNights;
            reservation.RoomId = reservationDto.RoomId;
            reservation.GuestId = reservationDto.GuestId;

            await _context.SaveChangesAsync();

            return reservationDto;*/
        }

        public async Task DeleteReservationAsync(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
