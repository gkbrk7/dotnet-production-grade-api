using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BLL.Request;
using API.BLL.Services;
using API.DLL.Models;
using API.DLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CourseController : BaseApiController
    {
        private readonly ICourseService courseService;

        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await courseService.GetAllAsync());
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetAll(string code)
        {
            return Ok(await courseService.GetAsync(code));
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CourseInsertRequestViewModel request)
        {
            return Ok(await courseService.InsertAsync(request));
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> Update(string code, Course course)
        {
            return Ok(await courseService.UpdateAsync(code, course));
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            return Ok(await courseService.DeleteAsync(code));
        }
    }
}