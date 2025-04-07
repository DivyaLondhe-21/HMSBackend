using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReservationService.Models;

public partial class Reservation
{
    [Key]
    public int ReservationId { get; set; }
    public int NumberOfChildren { get; set; }

    [Required]
    public int NumberOfAdults { get; set; }

    [Required]
    public DateTime CheckInDate { get; set; }

    [Required]
    public DateTime CheckOutDate { get; set; }

    public int NumberOfNights { get; set; }

    [Required]
    public int RoomId { get; set; }
    [ForeignKey("RoomId")]
    public virtual Room? Room { get; set; }

    [Required]
    public int GuestId { get; set; }

    [ForeignKey("GuestId")]
    public virtual Guest Guest { get; set; }

}
