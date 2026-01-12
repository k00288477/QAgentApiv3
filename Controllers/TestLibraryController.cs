
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
            if(string.IsNullOrEmpty(userEmail))
            {
                return NotFound("User not authorized");
            }

            // Get all the test suites with the matching author email
            var testSuites = await _testSuiteService.GetAllTestSuitesByAuthor(userEmail);

            // return the test suites (can be a null return, handled in the UI
            return Ok(testSuites);
        }

        // Add new Test Suite
        


        // Add new Test Case
    }
}
