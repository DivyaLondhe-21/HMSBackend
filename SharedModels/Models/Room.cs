using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReservationService.Models;

public partial class Room
{
    [Key]
    public int RoomID { get; set; }

    [Required(ErrorMessage = "Room type is required.")]
    [StringLength(50, ErrorMessage = "Room type cannot be longer than 50 characters.")]
    public string RoomType { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Period is required.")]
    [StringLength(20, ErrorMessage = "Period cannot be longer than 20 characters.")]
    public string Period { get; set; }

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Check-in date is required.")]
    public DateTime CheckInDate { get; set; }

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Check-out date is required.")]
    public DateTime CheckOutDate { get; set; }
    public bool Availability { get; set; }

    [Required]
    public int GuestId { get; set; }
    [ForeignKey("GuestId")]
    public Guest Guest { get; set; }

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
