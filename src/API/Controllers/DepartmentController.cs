using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BLL.Request;
using API.BLL.Services;
using API.DLL.Models;
using API.DLL.Repositories;
using LightQuery.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DepartmentController : BaseApiController
    {
        private readonly IDepartmentService departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        // https://localhost:5001/api/v1/Department?pageSize=50&page=1&sort=name%20desc
        // https://localhost:5001/api/v1/Department?pageSize=50&page=1&sort=name
        // Result must be IQueryable 
        [HttpGet]
        [AsyncLightQuery(forcePagination: true, defaultPageSize: 10, defaultSort: "departmentId desc")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await departmentService.GetAllAsync());
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetAll(string code)
        {
            return Ok(await departmentService.GetAsync(code));
        }

        [HttpPost]
        public async Task<IActionResult> Insert(DepartmentInsertRequestViewModel request)
        {
            return Ok(await departmentService.InsertAsync(request));
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> Update(string code, Department department)
        {
            return Ok(await departmentService.UpdateAsync(code, department));
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            return Ok(await departmentService.DeleteAsync(code));
        }
    }
}