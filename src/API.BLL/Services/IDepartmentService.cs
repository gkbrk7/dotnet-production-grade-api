using System.Collections.Generic;
using System.Threading.Tasks;
using API.BLL.Request;
using API.BLL.Utilities.Exceptions;
using API.DLL.Models;
using API.DLL.Repositories;

namespace API.BLL.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> GetAsync(string code);
        Task<Department> InsertAsync(DepartmentInsertRequestViewModel request);
        Task<Department> UpdateAsync(string code, Department department);
        Task<Department> DeleteAsync(string code);
        Task<bool> IsCodeExists(string code);
        Task<bool> IsNameExists(string name);
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
            var department = await departmentRepository.GetAsync(code);

            if (department == null)
                throw new ApplicationValidationException("Department Not Found");

            if (await departmentRepository.DeleteAsync(department))
                return department;

            throw new ApplicationValidationException("An Error Occured Deleting Data");
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await departmentRepository.GetAllAsync();
        }

        public async Task<Department> GetAsync(string code)
        {
            var department = await departmentRepository.GetAsync(code);
            if (department == null)
                throw new ApplicationValidationException("Department Not Found");

            return department;
        }

        public async Task<Department> InsertAsync(DepartmentInsertRequestViewModel request)
        {
            Department department = new Department
            {
                Code = request.Code,
                Name = request.Name
            };

            return await departmentRepository.InsertAsync(department);
        }

        public async Task<bool> IsCodeExists(string code)
        {
            var department = await departmentRepository.FindByCode(code);
            if (department == null)
                return true;

            return false;
        }

        public async Task<bool> IsNameExists(string name)
        {
            var department = await departmentRepository.FindByName(name);
            if (department == null)
                return true;

            return false;
        }

        public async Task<Department> UpdateAsync(string code, Department department)
        {
            var _department = await departmentRepository.GetAsync(code);

            if (_department == null)
                throw new ApplicationValidationException("Department Not Found");

            if (!string.IsNullOrWhiteSpace(_department.Code))
            {
                var existsAlreadyCode = await departmentRepository.FindByCode(department.Code);
                if (existsAlreadyCode != null)
                    new ApplicationValidationException("Updated Code Already Exists");

                _department.Code = department.Code;
            }

            if (!string.IsNullOrWhiteSpace(_department.Name))
            {
                var existsAlreadyName = await departmentRepository.FindByName(department.Name);
                if (existsAlreadyName != null)
                    new ApplicationValidationException("Updated Name Already Exists");

                _department.Name = department.Name;
            }

            if (await departmentRepository.UpdateAsync(department))
                return _department;

            throw new ApplicationValidationException("An Error Occured Updating Data");
        }
    }
}