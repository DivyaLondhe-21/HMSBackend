namespace RoomService.DTO
{
    public class RateDTO
    {
        public int RateId { get; set; }
        public decimal FirstNightPrice { get; set; }
        public decimal ExtensionPrice { get; set; }
        public int NumberOfChildren { get; set; }
        public int NumberOfGuests { get; set; }
        public int NumberOfDays { get; set; }
        //public int Discount { get; set; }
        public int RoomId { get; set; }
    }
}
