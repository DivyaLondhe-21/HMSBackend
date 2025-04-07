//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using EmployeeService.Interface;
//using EmployeeService.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.IdentityModel.Tokens;

//namespace EmployeeService.Controllers
//{
//    [AllowAnonymous]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EmployeeController : ControllerBase
//    {
//        private readonly IEmployee _employeeRepository;
//        private readonly IDepartment _departmentRepository;

//        public EmployeeController(IEmployee employeeRepository, IDepartment departmentRepository)  
//        {
//            _employeeRepository = employeeRepository;
//            _departmentRepository = departmentRepository;
//        }

//        [HttpGet("departments")]
//        [Authorize(Roles = "Admin,Manager,Receptionist")]
//        public async Task<IActionResult> GetDepartments()
//        {
//            var departments = await _departmentRepository.GetAllDepartments();
//            return Ok(departments);
//        }

//        [HttpGet("staff")]
//        [Authorize(Roles = "Admin,Manager,Receptionist")]
//        public async Task<IActionResult> GetStaff()
//        {
//            var staff = await _employeeRepository.GetAllStaff();
//            return Ok(staff);
//        }

//        [HttpGet("staff/byDepartment/{departmentName}")]
//        [Authorize(Roles = "Admin,Manager,Receptionist")]
//        public async Task<IActionResult> GetStaffOfaDepartment(string departmentName)
//        {
//            var staff = await _employeeRepository.GetAllStaffofDepartment(departmentName);
//            if (staff.IsNullOrEmpty())
//            {
//                return BadRequest("Staff not found");
//            }
//            return Ok(staff);
//        }

//        [HttpGet("staff/{staffID}")]
//        [Authorize(Roles = "Admin,Manager,Receptionist")]
//        public async Task<IActionResult> GetStaffbyId(int staffID)
//        {
//            var staff = await _employeeRepository.GetStaffById(staffID);
//            if (staff == null)
//            {
//                return BadRequest("Staff not found");
//            }
//            return Ok(staff);
//        }

//        [HttpPost("staff")]
//        [Authorize(Roles = "Admin,Manager")]
//        public async Task<IActionResult> AddStaff(Staff staff)
//        {
//            await _employeeRepository.AddStaff(staff);
//            return Ok("Staff created successfully.");
//        }

//        [HttpPut("staff/{staffId}")]
//        [Authorize(Roles = "Admin,Manager")]
//        public async Task<IActionResult> UpdateStaff(int staffId, Staff staff)
//        {
//            await _employeeRepository.UpdateStaff(staffId, staff);
//            return Ok("Staff updated successfully.");
//        }

//        [HttpDelete("staff/{staffId}")]
//        [Authorize(Roles = "Admin,Manager")]
//        public async Task<IActionResult> DeleteStaff(int staffId)
//        {
//            await _employeeRepository.DeleteStaff(staffId);
//            return Ok("Staff deleted successfully.");
//        }

//        [HttpPost("departments")]
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> AddDepartment(Department department)
//        {
//            await _departmentRepository.AddDepartment(department);
//            return Ok("Department created successfully.");
//        }

//        [HttpPut("departments/{departmentId}")]
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> UpdateDepartment(int departmentId, Department department)
//        {
//            await _departmentRepository.UpdateDepartment(departmentId, department);
//            return Ok("Department updated successfully.");
//        }

//        [HttpDelete("departments/{departmentId}")]
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> RemoveDepartment(int departmentId)
//        {
//            await _departmentRepository.RemoveDepartment(departmentId);
//            return Ok("Department deleted successfully.");
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using EmployeeService.Interface;
using EmployeeService.DTO;
using EmployeeService.Models;

namespace EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employeeService;
        private readonly IDepartment _departmentService;

        public EmployeeController(IEmployee employeeService, IDepartment departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDTO>>> GetAllStaff()
        {
            var staffList = await _employeeService.GetAllStaff();
            var result = staffList.Select(s => new StaffDTO
            {
                StaffId = s.StaffId,
                EmployeeName = s.EmployeeName,
                EmployeeAddress = s.EmployeeAddress,
                NIC = s.NIC,
                Salary = s.Salary,
                Age = s.Age,
                Occupation = s.Occupation,
                Email = s.Email,
                HiredBy = s.HiredBy,
                DepartmentName = s.Department?.DepartmentName
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDTO>> GetStaffById(int id)
        {
            var staff = await _employeeService.GetStaffById(id);
            if (staff == null) return NotFound();

            var dto = new StaffDTO
            {
                StaffId = staff.StaffId,
                EmployeeName = staff.EmployeeName,
                EmployeeAddress = staff.EmployeeAddress,
                NIC = staff.NIC,
                Salary = staff.Salary,
                Age = staff.Age,
                Occupation = staff.Occupation,
                Email = staff.Email,
                HiredBy = staff.HiredBy,
                DepartmentName = staff.Department?.DepartmentName
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<Staff>> AddStaff(StaffDTO dto)
        {
            var deptId = await _departmentService.GetDepartmentID(dto.DepartmentName);
            if (deptId == 0) return BadRequest("Invalid Department Name");

            var staff = new Staff
            {
                EmployeeName = dto.EmployeeName,
                EmployeeAddress = dto.EmployeeAddress,
                NIC = dto.NIC,
                Salary = dto.Salary,
                Age = dto.Age,
                Occupation = dto.Occupation,
                Email = dto.Email,
                HiredBy = dto.HiredBy,
                DepartmentId = deptId
            };

            var result = await _employeeService.AddStaff(staff);
            return CreatedAtAction(nameof(GetStaffById), new { id = result.StaffId }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStaff(int id, StaffDTO dto)
        {
            var deptId = await _departmentService.GetDepartmentID(dto.DepartmentName);
            if (deptId == 0) return BadRequest("Invalid Department Name");

            var staff = new Staff
            {
                EmployeeName = dto.EmployeeName,
                EmployeeAddress = dto.EmployeeAddress,
                NIC = dto.NIC,
                Salary = dto.Salary,
                Age = dto.Age,
                Occupation = dto.Occupation,
                Email = dto.Email,
                HiredBy = dto.HiredBy,
                DepartmentId = deptId
            };

            var updated = await _employeeService.UpdateStaff(id, staff);
            if (updated == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStaff(int id)
        {
            await _employeeService.DeleteStaff(id);
            return NoContent();
        }
    }
}
