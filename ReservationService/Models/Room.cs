using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReservationService.Models;

public partial class Room
{
    [Key]
    public int RoomID { get; set; }

    [Required]
    public string RoomType { get; set; }

    public decimal Price { get; set; }

    [Required]
    public string Period { get; set; }

    [Required]
    public DateTime CheckInDate { get; set; }

    [Required]
    public DateTime CheckOutDate { get; set; }
    public bool Availability { get; set; }

    [Required]
    public int GuestId { get; set; }
    [ForeignKey("GuestId")]
    
    public Guest Guest { get; set; }

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
