using System.Collections.Generic;
using System.Threading.Tasks;
using API.DLL.Models;
using API.DLL.Repositories;

namespace API.BLL.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> GetAsync(string code);
        Task<Department> InsertAsync(Department department);
        Task<Department> UpdateAsync(string code, Department department);
        Task<Department> DeleteAsync(string code);
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;

        }
        public async Task<Department> DeleteAsync(string code)
        {
            return await departmentRepository.DeleteAsync(code);
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await departmentRepository.GetAllAsync();
        }

        public async Task<Department> GetAsync(string code)
        {
            return await departmentRepository.GetAsync(code);
        }

        public async Task<Department> InsertAsync(Department department)
        {
            return await departmentRepository.InsertAsync(department);
        }

        public async Task<Department> UpdateAsync(string code, Department department)
        {
            return await departmentRepository.UpdateAsync(code, department);
        }
    }
}