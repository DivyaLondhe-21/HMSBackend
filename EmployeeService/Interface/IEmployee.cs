using EmployeeService.Models;
namespace EmployeeService.Interface
{
    public interface IEmployee
    {
        Task<IEnumerable<Staff>> GetAllStaff();
        Task<Staff> GetStaffById(int staffId);
        Task<IEnumerable<Staff>> GetAllStaffofDepartment(string departmentName);
        Task<Staff> AddStaff(Staff staff);
        Task<Staff> UpdateStaff(int staffId, Staff staff);
        Task DeleteStaff(int staffId);


    }
}
