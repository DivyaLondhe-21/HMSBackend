using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReservationService.Models;

public partial class Rate
{
    [Key]
    public int RateId { get; set; }

    [Required]
    public decimal FirstNightPrice { get; set; }

    [Required]
    public decimal ExtensionPrice { get; set; }

    [Required]
    public int NumberOfChildren { get; set; }

    [Required]
    public int NumberOfGuests { get; set; }

    [Required]
    public int NumberOfDays { get; set; }

    //[Required]
    //public int Discount { get; set; }

    [Required]
    public int RoomId { get; set; }
    [ForeignKey("RoomId")]
    public virtual Room Room { get; set; }
}
