//using EmployeeService.Interface;
//using EmployeeService.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace EmployeeService.Repositories
//{
//    public class EmployeeRepository : IEmployee
//    {
//        private readonly EmployeeDbContext _context;
//        private readonly IDepartment _departmentRepository;
//        public EmployeeRepository(EmployeeDbContext context, IDepartment departmentRepository) 
//        {
//           _context = context;
//           _departmentRepository = departmentRepository;
//        }

//        public async Task<IEnumerable<Staff>> GetAllStaff()
//        {
//            var allStaffs = await _context.Staffs.ToListAsync();
//            return allStaffs; 
//        }

//        public async Task<Staff> GetStaffById(int staffID)
//        {
//            var staff = await _context.Staffs
//                                           .FirstOrDefaultAsync(d => d.StaffId == staffID);

//            if (staff == null)
//            {
//                throw new KeyNotFoundException("Department not found");
//            }
//            return staff;
//        }

//        public async Task<IEnumerable<Staff>> GetAllStaffofDepartment(string departmentName)
//        {
//            var departmentID = await _departmentRepository.GetDepartmentID(departmentName);
//            if(departmentID == null)
//            {
//                throw new KeyNotFoundException("Department does not exist");
//            }

//            var staffs = await _context.Staffs.Where(s=> s.DepartmentId == departmentID).ToListAsync();
//            return staffs;
//        }
//        public async Task<Staff> AddStaff(Staff staff)
//        {
//            await _context.Staffs.AddAsync(staff);
//            await _context.SaveChangesAsync();
//            return staff;
//        }

//        public async Task<Staff> UpdateStaff(int staffID, Staff updatedStaff)
//        {
//            var existingStaff = await _context.Staffs.FindAsync(staffID);
//            if (existingStaff == null)
//            {
//                throw new KeyNotFoundException("Department not found");
//            }

//            existingStaff.EmployeeName = updatedStaff.EmployeeName;
//            existingStaff.Occupation = updatedStaff.Occupation;
//            existingStaff.Salary = updatedStaff.Salary;
//            existingStaff.NIC = updatedStaff.NIC;
//            existingStaff.EmployeeAddress = updatedStaff.EmployeeAddress;
//            existingStaff.Age  = updatedStaff.Age;
//            existingStaff.Email = updatedStaff.Email;
//            existingStaff.DepartmentId = updatedStaff.DepartmentId;


//            _context.Staffs.Update(existingStaff);
//            await _context.SaveChangesAsync();
//            return updatedStaff;
//        }
//        public async Task DeleteStaff(int staffID)
//        {
//            var staff = await _context.Staffs.FindAsync(staffID);

//            if (staff == null)
//            {
//                throw new KeyNotFoundException("Staff not found");
//            }

//            _context.Staffs.Remove(staff);
//            await _context.SaveChangesAsync();
//        }
//    }
//}

using EmployeeService.Interface;
using EmployeeService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Repositories
{
    public class EmployeeRepository : IEmployee
    {
        private readonly EmployeeDbContext _context;

        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Staff>> GetAllStaff()
        {
            return await _context.Staffs.Include(s => s.Department).ToListAsync();
        }

        public async Task<Staff> GetStaffById(int staffId)
        {
            return await _context.Staffs.Include(s => s.Department).FirstOrDefaultAsync(s => s.StaffId == staffId);
        }

        public async Task<IEnumerable<Staff>> GetAllStaffofDepartment(string departmentName)
        {
            return await _context.Staffs
                .Include(s => s.Department)
                .Where(s => s.Department.DepartmentName == departmentName)
                .ToListAsync();
        }

        public async Task<Staff> AddStaff(Staff staff)
        {
            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task<Staff> UpdateStaff(int staffId, Staff staff)
        {
            var existing = await _context.Staffs.FindAsync(staffId);
            if (existing == null) return null;

            existing.EmployeeName = staff.EmployeeName;
            existing.EmployeeAddress = staff.EmployeeAddress;
            existing.NIC = staff.NIC;
            existing.Salary = staff.Salary;
            existing.Age = staff.Age;
            existing.Occupation = staff.Occupation;
            existing.Email = staff.Email;
            existing.HiredBy = staff.HiredBy;
            existing.DepartmentId = staff.DepartmentId;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task DeleteStaff(int staffId)
        {
            var staff = await _context.Staffs.FindAsync(staffId);
            if (staff != null)
            {
                _context.Staffs.Remove(staff);
                await _context.SaveChangesAsync();
            }
        }
    }
}
