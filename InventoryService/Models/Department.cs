using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventoryService.Models;

public partial class Department
{
    [Key]
    public int DepartmentId { get; set; }

    [Required]
    public string DepartmentName { get; set; }

    public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
