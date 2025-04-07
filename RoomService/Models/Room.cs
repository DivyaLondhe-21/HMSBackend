using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ReservationService.Models;
using System.Text.Json.Serialization;

namespace RoomService.Models
{
    public partial class Room
    {
        [Key]
        public int RoomID { get; set; }

        [Required]
        public string RoomType { get; set; }

        public decimal Price { get; set; }

        [Required]
        public string Period { get; set; }

        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        public bool Availability { get; set; }

        [Required]
        public int GuestId { get; set; }

        [ForeignKey("GuestId")]
        public virtual Guest Guest { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        public ICollection<Rate> Rates { get; set; } = new List<Rate>();
    }
}
