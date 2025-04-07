namespace RoomService.DTO
{
    public class RoomDTO
    {
        public int RoomID { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }
        public string Period { get; set; }
        public bool Availability { get; set; }
        public int GuestId { get; set; }
    }
}
