using Microsoft.AspNetCore.Mvc;
using QAgentApi.Service;

namespace QAgentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestExecutionController : Controller
    {
        private readonly TestExecutionService _testExecutionService;
        public TestExecutionController(TestExecutionService testExecutionService)
        {
            _testExecutionService = testExecutionService;
        }

        // Execute Single Test Case
        [HttpPost("ExecuteSingleTestCase/{testCaseId}")]
        public async Task<IActionResult> ExecuteSingleTestCase(int testCaseId)
        {
            try
            {   // Call service to execute the test case
                var executionRun = await _testExecutionService.ExecuteSingleTestCaseAsync(testCaseId);
                return Ok(executionRun);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
