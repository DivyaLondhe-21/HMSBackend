namespace ReservationService.DTO
{
    public class ReservationDTO
    {
        public int NumberOfChildren { get; set; }
        public int NumberOfAdults { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int RoomId { get; set; }
        public GuestDTO Guest { get; set; }
    }
}
