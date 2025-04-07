using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ReservationService.Models;

public partial class Reservation
{
    [Key]
    public int ReservationId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Number of children must be a non-negative value.")]
    public int NumberOfChildren { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Number of adults must be at least 1.")]
    [Required]
    public int NumberOfAdults { get; set; }

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Check-in date is required.")]
    public DateTime CheckInDate { get; set; }

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Check-out date is required.")]
    public DateTime CheckOutDate { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Number of nights must be at least 1.")]
    public int NumberOfNights { get; set; }

    [Required]
    public int RoomId { get; set; }
    [ForeignKey("RoomId")]
    public Room Room { get; set; }
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public void CalculateNumOfNights()
    {
        NumberOfNights = Math.Max((CheckOutDate - CheckInDate).Days, 1);
    }
}
