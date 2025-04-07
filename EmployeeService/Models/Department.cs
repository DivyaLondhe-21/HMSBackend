using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeService.Models;

public partial class Department
{
    [Key]
    public int DepartmentId { get; set; }

    [Required]
    public string DepartmentName { get; set; }

    public ICollection<Staff>? Staffs { get; set; }

}
