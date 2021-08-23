using System.Threading.Tasks;
using API.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TestController : BaseApiController
    {
        private readonly ITestService testService;
        private readonly ITransactionService transactionService;

        public TestController(ITestService testService, ITransactionService transactionService)
        {
            this.testService = testService;
            this.transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> LoadDummyData()
        {
            // await testService.DummyData();
            // await testService.AddRoles();
            // await testService.AddUsersWithRoles();
            await transactionService.FinancialTransaction();
            return Ok("All dummy data loaded into database");
        }
    }
}