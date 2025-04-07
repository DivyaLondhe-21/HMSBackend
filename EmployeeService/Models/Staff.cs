using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeService.Models;

public partial class Staff
{
    [Key]
    public int StaffId { get; set; }
    public int DepartmentId { get; set; }
    [ForeignKey("DepartmentId")]
    public virtual Department Department { get; set; }

    [Required]
    public string EmployeeName { get; set; }

    [Required]
    public string EmployeeAddress { get; set; }

    [Required]
    public string NIC { get; set; }

    public decimal Salary { get; set; }

    public int Age { get; set; }

    [Required]
    public string Occupation { get; set; }

    [Required]
    public string Email { get; set; }
    public string HiredBy { get; set; }
}
