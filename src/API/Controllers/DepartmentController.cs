using System.Collections.Generic;
using System.Linq;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(DepartmentStatic.GetAllDepartments());
        }

        [HttpGet("{code}")]
        public IActionResult GetAll(string code)
        {
            return Ok(DepartmentStatic.GetDepartment(code));
        }

        [HttpPost]
        public IActionResult Insert(Department department)
        {
            return Ok(DepartmentStatic.InsertDepartment(department));
        }

        [HttpPut("{code}")]
        public IActionResult Update(string code, Department department)
        {
            return Ok(DepartmentStatic.UpdateDepartment(code, department));
        }

        [HttpDelete("{code}")]
        public IActionResult Delete(string code)
        {
            return Ok(DepartmentStatic.DeleteDepartment(code));
        }
    }

    public static class DepartmentStatic
    {
        private static List<Department> AllDepartments { get; set; } = new List<Department>();
        public static Department InsertDepartment(Department department)
        {
            AllDepartments.Add(department);
            return department;
        }

        public static IEnumerable<Department> GetAllDepartments()
        {
            return AllDepartments;
        }

        public static Department GetDepartment(string code)
        {
            return AllDepartments.FirstOrDefault(x => x.Code == code);
        }

        public static Department UpdateDepartment(string code, Department department)
        {
            var updated = AllDepartments.Where(x => x.Code == code).FirstOrDefault();
            updated.Name = department.Name;
            return department;
        }

        public static Department DeleteDepartment(string code)
        {
            var deleted = AllDepartments.FirstOrDefault(x => x.Code == code);
            AllDepartments.Remove(deleted);
            return deleted;
        }
    }
}