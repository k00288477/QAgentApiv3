
using Microsoft.AspNetCore.Authorization;
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
        private readonly TestCaseService _testCaseService;
        public TestLibraryController(TestSuiteService testSuiteService, TestCaseService testCaseService)
        {
            _testSuiteService = testSuiteService;
            _testCaseService = testCaseService;
        }

        [HttpGet("TestNoAuth")]
        public ActionResult<string> Test()
        {
            return Ok("TestLibraryController is working!");
        }

        // Get all test cases for the logged in user
        [HttpGet("GetAllTests")]
        [Authorize]
        public async Task<ActionResult<TestSuite>> GetAllTests()
        {
            // Get the user email from JWT Token
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            // var userEmail = "gary@tus.ie";
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
        [Authorize]
        public async Task<ActionResult<TestSuite>> AddNewTestSuite([FromBody] TestSuite testSuite)
        {
            // Get the user email from JWT Token
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return NotFound("User not authorized");
            }
            // Set the author to the logged in user
            testSuite.Author = userEmail;

            // Validate input
            if (testSuite == null || string.IsNullOrEmpty(testSuite.Title) || string.IsNullOrEmpty(testSuite.Author))
            {
                return BadRequest("Invalid test suite data.");
            }

            // Add the new test suite using the service
            var createdTestSuite = await _testSuiteService.AddNewTestSuite(testSuite);

            // Return the created test suite
            return Ok(createdTestSuite);
        }


        // Add new Test Case (No Test Suite association required, can accept association)
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

            // Set the author to the logged in user
            testCase.Author = userEmail;

            // Validate input
            if (testCase == null || string.IsNullOrEmpty(testCase.Title) || string.IsNullOrEmpty(testCase.Author))
            {
                return BadRequest("Invalid test case data.");
            }
            // set Date created
            testCase.DateCreated = DateTime.UtcNow;
            // Add Test Case to database
            var createdTestCase = await _testSuiteService.AddNewTestCase(testCase);

            return Ok(createdTestCase);
        }

        [HttpGet("GetTestCase/{id}")]
        public async Task<ActionResult<TestCase>> GetTestCase(int id)
        {
            var testCase = await _testCaseService.GetTestCaseById(id);
            if (testCase == null)
            {
                return NotFound($"Test case with ID {id} not found.");
            }
            Console.WriteLine($"Retrieved Test Case: {testCase.Title}");
            return Ok(testCase);
        }

        [HttpPut("EditTestCase")]
        public async Task<ActionResult<TestCase>> EditTestCase([FromBody] TestCase updatedTestCase)
        {
            var updatedResult = await _testCaseService.EditTestCase(updatedTestCase);
            if (updatedResult == null)
            {
                return NotFound($"Test case with ID {updatedTestCase.TestCaseId} not found.");
            }
            return Ok(updatedResult);
        }
    }
}
