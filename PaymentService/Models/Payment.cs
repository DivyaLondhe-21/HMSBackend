using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ReservationService.Models;
using System.Text.Json.Serialization;

namespace PaymentService.Models;

public partial class Payment
{
    [Key]
    public int PaymentId { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public DateTime PayTime { get; set; } = DateTime.Now;

    [Required]
    public string CreditCardNumber { get; set; }

    [Required]
    public string CreditCardType { get; set; }

    [Required]
    public string Cvv { get; set; }

    [Required]
    public string CardHolderName { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime CreditExpiryDate { get; set; }

    [Required]
    public int ReservationId { get; set; }

    [ForeignKey("ReservationId")]
    public virtual Reservation Reservation { get; set; }
    public ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
