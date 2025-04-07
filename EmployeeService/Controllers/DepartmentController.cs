using EmployeeService.DTO;
using EmployeeService.Interface;
using EmployeeService.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment _departmentService;

        public DepartmentController(IDepartment departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllDepartments();
            var result = departments.Select(d => new DepartmentDTO
            {
                DepartmentId = d.DepartmentId,
                DepartmentName = d.DepartmentName
            });
            return Ok(result);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<DepartmentDTO>> GetDepartment(string name)
        {
            var department = await _departmentService.GetSpecificDepartment(name);
            if (department == null)
                return NotFound();

            return Ok(new DepartmentDTO
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName
            });
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentDTO>> AddDepartment(DepartmentDTO departmentDto)
        {
            var department = new Department
            {
                DepartmentName = departmentDto.DepartmentName
            };
            var result = await _departmentService.AddDepartment(department);

            return CreatedAtAction(nameof(GetDepartment), new { name = result.DepartmentName }, new DepartmentDTO
            {
                DepartmentId = result.DepartmentId,
                DepartmentName = result.DepartmentName
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, DepartmentDTO departmentDto)
        {
            var department = new Department
            {
                DepartmentId = id,
                DepartmentName = departmentDto.DepartmentName
            };
            await _departmentService.UpdateDepartment(id, department);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _departmentService.RemoveDepartment(id);
            return NoContent();
        }
    }
}
