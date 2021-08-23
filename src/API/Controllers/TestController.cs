using System.Threading.Tasks;
using API.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TestController : BaseApiController
    {
        private readonly ITestService testService;
        public TestController(ITestService testService)
        {
            this.testService = testService;
        }

        [HttpGet]
        public async Task<IActionResult> LoadDummyData()
        {
            await testService.DummyData();
            return Ok("All dummy data loaded into database");
        }
    }
}