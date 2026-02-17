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

        // Execute Multiple Test Cases

        // Check Test Execution Status
        [HttpGet("CheckExecutionStatus/{executionRunId}")]
        public async Task<ActionResult> CheckExecutionStatus(string executionRunId)
        {
            try
            {   // Call service to check the status of the execution run
                var status = await _testExecutionService.GetTestExecutionStatusAsync(executionRunId);
                if (status == null)
                {
                    return NotFound($"Execution run with ID {executionRunId} not found.");
                }
                // Check if API returned an error status 
                if (!status.Success)
                {
                    return BadRequest($"Error checking execution status: {status.Error}");
                }
                return Ok(status);
            }
            catch (HttpRequestException ex) 
            {
                return BadRequest($"Error communicating with test execution service: {ex.Message}");
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

        // Get Test Execution Report QAgent-AIEngine and save to database
        [HttpGet("GetTestReport/{taskId}")]
        public async Task<ActionResult> GetTestReport(string taskId)
        {
            try
            {   // Call service to get the test execution report amd save it to the database
                var executionReport = await _testExecutionService.GetTestExecutionReportAndSaveToDatabaseAsync(taskId);
                if (executionReport == null)
                {
                    return NotFound($"Execution report for task ID {taskId} not found.");
                }
                return Ok(executionReport);
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

        // Get Test Execution Report from database by execution run ID  
        [HttpGet("GetTestReportFromDB/{executionRunId}")]
        public async Task<ActionResult> GetTestReportFromDB(int executionRunId)
        {
            try
            {   // Call service to get the test execution report from the database
                var executionReport = await _testExecutionService.GetExecutionReportFromDatabaseAsync(executionRunId);
                if (executionReport == null)
                {
                    return NotFound($"Execution report for execution run ID {executionRunId} not found in database.");
                }
                return Ok(executionReport);
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

        // Cancel Test Execution


    }
}
