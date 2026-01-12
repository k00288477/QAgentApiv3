
using Microsoft.AspNetCore.Mvc;
using QAgentApi.Model;
using QAgentApi.Service;
using System.Security.Claims;

namespace QAgentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestLibraryController : ControllerBase
    {
        private readonly TestSuiteService _testSuiteService;
        public TestLibraryController(TestSuiteService testSuiteService)
        {
            _testSuiteService = testSuiteService;
        }

        // Get all test cases for the logged in user
        [HttpGet("GetAllTests")]
        public async Task<ActionResult<TestSuite>> GetAllTests()
        {
            // Get the user email from JWT Token
            //var userEmail = User.FindFirst(ClaimTypes.Name)?.Value;
            var userEmail = "newemail@example.com";
            // check if null
            if (string.IsNullOrEmpty(userEmail))
            {
                return NotFound("User not authorized");
            }

            // Get all the test suites with the matching author email
            var testSuites = await _testSuiteService.GetAllTestSuitesByAuthor(userEmail);

            // return the test suites (can be a null return, handled in the UI
            return Ok(testSuites);
        }

        // Add new Test Suite (No Test Cases)
        [HttpPost("AddNewTestSuite")]
        public async Task<ActionResult<TestSuite>> AddNewTestSuite([FromBody] TestSuite testSuite)
        {
            // Validate input
            if (testSuite == null || string.IsNullOrEmpty(testSuite.Title) || string.IsNullOrEmpty(testSuite.Author))
            {
                return BadRequest("Invalid test suite data.");
            }

            // Add the new test suite using the service
            var createdTestSuite = await _testSuiteService.AddNewTestSuite(testSuite);

            // Return the created test suite with a 201 status code
            return Ok(createdTestSuite);
        }


        // Add new Test Case (No Test Suite association required,can accept association)
        [HttpPost("AddNewTestCase")]
        public async Task<ActionResult<TestCase>> AddNewTestCase([FromBody] TestCase testCase)
        {
            // Get the user email from JWT Token
            var userEmail = User.FindFirst(ClaimTypes.Name)?.Value;
            // Check if null
            if (string.IsNullOrEmpty(userEmail))
            {
                return NotFound("User not authorized");
            }
            // Check if author matches the user email
            if (testCase == null || string.IsNullOrEmpty(testCase.Title) || testCase.Author != userEmail)
            {
                return BadRequest("Invalid test case data or unauthorized author.");
            }

            // Add Test Case to database
            var createdTestCase = await _testSuiteService.AddNewTestCase(testCase);

            return Ok(createdTestCase);
        }

        // Add Test Case to existing Test Suite
        //[HttpPost("AddTestCaseToSuite/{testSuiteId}")] - TODO

    }
}