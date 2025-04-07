using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventoryService.Models;

public partial class Inventory
{
    [Key]
    public int InventoryId { get; set; }

    [Required]
    public string ItemName { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int DepartmentId { get; set; }
    [ForeignKey("DepartmentId")]
    public virtual Department Department { get; set; } 
}
