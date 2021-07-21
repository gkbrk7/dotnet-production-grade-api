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
        Task<bool> UpdateAsync(Department department);
        Task<bool> DeleteAsync(Department department);
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> GetAsync(string code);
        Task<Department> FindByName(string name);
        Task<Department> FindByCode(string code);
    }

    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteAsync(Department department)
        {
            context.Entry(department).State = EntityState.Deleted;
            return await context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<Department> FindByCode(string code)
        {
            return await context.Departments.FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task<Department> FindByName(string name)
        {
            return await context.Departments.FirstOrDefaultAsync(x => x.Name == name);
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

        public async Task<bool> UpdateAsync(Department department)
        {
            context.Entry(department).State = EntityState.Modified;
            return await context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}