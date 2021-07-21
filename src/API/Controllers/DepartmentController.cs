using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DLL.Models;
using API.DLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DepartmentController : BaseApiController
    {
        private readonly IDepartmentRepository departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await departmentRepository.GetAllAsync());
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetAll(string code)
        {
            return Ok(await departmentRepository.GetAsync(code));
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Department department)
        {
            return Ok(await departmentRepository.InsertAsync(department));
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> Update(string code, Department department)
        {
            return Ok(await departmentRepository.UpdateAsync(code, department));
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            return Ok(await departmentRepository.DeleteAsync(code));
        }
    }
}