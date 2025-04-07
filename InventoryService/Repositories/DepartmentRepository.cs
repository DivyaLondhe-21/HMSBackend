using InventoryService.Interface;
using InventoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Repositories
{
    public class DepartmentRepository : IDepartment
    {
        private readonly InventoryDbContext _context;
        public DepartmentRepository(InventoryDbContext context) 
        {
            _context = context;
        }


        //public async Task<int> GetDepartmentID(string DepartmentName)
        //{
        //    var departmentID = await _context.Departments.FindAsync(DepartmentName);
        //    return departmentID.DepartmentId;
        //}

        public async Task<int> GetDepartmentID(string departmentName)
        {
            var dept = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentName == departmentName);

            return dept?.DepartmentId ?? 0;
        }
    }
}
