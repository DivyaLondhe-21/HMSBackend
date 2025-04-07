using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReservationService.Models;

public partial class Guest
{
    [Key]
    public int GuestId { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public string Company { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Gender { get; set; }
    public string Address { get; set; }

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}
