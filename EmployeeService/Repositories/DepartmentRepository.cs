//using EmployeeService.Interface;
//using EmployeeService.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace EmployeeService.Repositories
//{
//    public class DepartmentRepository : IDepartment
//    {
//        private readonly EmployeeDbContext _context;
//        //private readonly RoomRepository _roomRepository;
//        public DepartmentRepository(EmployeeDbContext context) 
//        {
//           _context = context;
//        }

//        public async Task<IEnumerable<Department>> GetAllDepartments()
//        {
//            var departments = await _context.Departments.ToListAsync();
//            return departments;
//        }

//        public async Task<Department> GetSpecificDepartment(string departmentName)
//        {
//            var department = await _context.Departments
//                                           .FirstOrDefaultAsync(d => d.DepartmentName == departmentName);

//            if (department == null)
//            {
//                throw new KeyNotFoundException("Department not found");
//            }
//            return department;
//        }

//        public async Task<int> GetDepartmentID(string departmentName)
//        {
//            var deparment = await _context.Departments.FindAsync(departmentName);
//            return deparment.DepartmentId;
//        }
//        public async Task<Department> AddDepartment(Department department)
//        {
//            await _context.Departments.AddAsync(department);
//            await _context.SaveChangesAsync();
//            return department;
//        }

//        public async Task UpdateDepartment(int departmentId, Department updatedDepartment)
//        {
//            var existingDepartment = await _context.Departments.FindAsync(departmentId);
//            if (existingDepartment == null)
//            {
//                throw new KeyNotFoundException("Department not found");
//            }

//            existingDepartment.DepartmentName = updatedDepartment.DepartmentName;
//            //existingDepartment.Description = updatedDepartment.Description;

//            _context.Departments.Update(existingDepartment);
//            await _context.SaveChangesAsync();
//        }
//        public async Task RemoveDepartment(int departmentId)
//        {
//            var department = await _context.Departments.FindAsync(departmentId);

//            if (department == null)
//            {
//                throw new KeyNotFoundException("Department not found");
//            }

//            _context.Departments.Remove(department);
//            await _context.SaveChangesAsync();
//        }
//    }
//}

using EmployeeService.Interface;
using EmployeeService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Repositories
{
    public class DepartmentRepository : IDepartment
    {
        private readonly EmployeeDbContext _context;

        public DepartmentRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _context.Departments.Include(d => d.Staffs).ToListAsync();
        }

        public async Task<Department> GetSpecificDepartment(string departmentName)
        {
            return await _context.Departments.Include(d => d.Staffs)
                .FirstOrDefaultAsync(d => d.DepartmentName == departmentName);
        }

        public async Task<int> GetDepartmentID(string departmentName)
        {
            var dept = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentName == departmentName);
            return dept != null ? dept.DepartmentId : 0;
        }

        public async Task<Department> AddDepartment(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task UpdateDepartment(int departmentId, Department department)
        {
            var existing = await _context.Departments.FindAsync(departmentId);
            if (existing != null)
            {
                existing.DepartmentName = department.DepartmentName;
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveDepartment(int departmentId)
        {
            var dept = await _context.Departments.FindAsync(departmentId);
            if (dept != null)
            {
                _context.Departments.Remove(dept);
                await _context.SaveChangesAsync();
            }
        }
    }
}
