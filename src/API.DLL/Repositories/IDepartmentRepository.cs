using System.Collections.Generic;
using System.Threading.Tasks;
using API.DLL.Contexts;
using API.DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DLL.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department> InsertAsync(Department department);
        Task<Department> UpdateAsync(string code, Department department);
        Task<Department> DeleteAsync(string code);
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> GetAsync(string code);
    }

    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Department> DeleteAsync(string code)
        {
            var department = await context.Departments.FirstOrDefaultAsync(x => x.Code == code);
            context.Entry(department).State = EntityState.Deleted;
            await context.SaveChangesAsync();
            return department;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await context.Departments.ToListAsync();
        }

        public async Task<Department> GetAsync(string code)
        {
            return await context.Departments.FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task<Department> InsertAsync(Department department)
        {
            await context.AddAsync(department);
            await context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> UpdateAsync(string code, Department department)
        {
            var _department = await context.Departments.FirstOrDefaultAsync(x => x.Code == code);
            _department.Name = department.Name;
            context.Entry(_department).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return _department;
        }
    }
}