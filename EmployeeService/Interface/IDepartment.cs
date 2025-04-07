using EmployeeService.Models;

namespace EmployeeService.Interface
{
    public interface IDepartment
    {
        Task<IEnumerable<Department>> GetAllDepartments();
        Task<Department> GetSpecificDepartment(string DepartmentName);
        Task<int> GetDepartmentID(string DepartmentName);
        Task<Department> AddDepartment(Department department);
        Task UpdateDepartment(int departmentId, Department department);
        Task RemoveDepartment(int departmentId);
    }
}
