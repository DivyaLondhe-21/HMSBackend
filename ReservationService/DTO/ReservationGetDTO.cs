//namespace ReservationService.ViewModel
//{
//    public class ReservationGetDTO
//    {
//        public int ReservationId { get; set; }
//        public string GuestName { get; set; }
//        public string GuestPhoneNumber { get; set; }
//        public string GuestEmail { get; set; }
//        public int NumberOfAdults { get; set; }
//        public int NumberOfChildren { get; set; }
//        public DateTime CheckInDate { get; set; }
//        public DateTime CheckOutDate { get; set; }
//        public int NumberOfNights { get; set; }
//        public int RoomId { get; set; }
//    }
//}

namespace ReservationService.DTO
{
    public class ReservationGetDTO
    {
        public int ReservationId { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfNights { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }
    }
}
