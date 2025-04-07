using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PaymentService.Models;

public partial class Bill
{
    [Key]
    public int BillId { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public decimal Taxes { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public string Service { get; set; }
    public string Unit { get; set; }

    [Required]
    public int PaymentId { get; set; }

    [ForeignKey("PaymentId")]
    public virtual Payment Payment { get; set; }
}
