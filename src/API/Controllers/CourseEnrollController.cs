using System.Threading.Tasks;
using API.BLL.Request;
using API.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CourseEnrollController : BaseApiController
    {
        private readonly ICourseStudentService courseStudentService;

        public CourseEnrollController(ICourseStudentService courseStudentService)
        {
            this.courseStudentService = courseStudentService;
        }
        [HttpPost]
        public async Task<IActionResult> Insert(CourseAssignInsertViewModel request)
        {
            return Ok(await courseStudentService.InsertAsync(request));
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> CourseList(int studentId)
        {
            return Ok(await courseStudentService.CourseListAsync(studentId));
        }
    }
}