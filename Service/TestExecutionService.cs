/*
This service class is responsible for managing test executions within the QAgentApi application.

This service class provides methods to send data to the AI Engine for: 
- Test Execution
- Status Checking
- Queue Management
- Viewing Status of Executions

This class also interacts with the data layer to store and retrieve test execution information.
 */

using QAgentApi.Model;
using QAgentApi.Repository.Interfaces;

namespace QAgentApi.Service
{
    public class TestExecutionService
    {
        private readonly HttpClient _httpClient;
        private readonly ITestCaseRepository _testCaseRepository;
        private readonly ITestExecutionReportRepository _executionReportRepository;
        private readonly IExecutionRunRepository _executionRunRepository;
        public TestExecutionService(HttpClient httpClient, ITestCaseRepository testCaseRepository, ITestExecutionReportRepository testExecutionReportRepository, IExecutionRunRepository executionRunRepository)
        {
            _httpClient = httpClient;
            _testCaseRepository = testCaseRepository;
            _executionReportRepository = testExecutionReportRepository;
            _executionRunRepository = executionRunRepository;
        }
        // Execute Single Test
        public async Task<ExecutionRun> ExecuteSingleTestCaseAsync(int testCaseId)
        {
            // Get the test case details from repository
            TestCase testCase = await _testCaseRepository.GetTestCaseById(testCaseId);
            if (testCase == null) 
            {
                throw new ArgumentException($"Test case with ID {testCaseId} not found.");
            }
            // Create request body, passing Test Steps
            var requestBody = new
            {
                task = testCase.TestSteps,
                generate_gif = true,
                headless = true
            };
            // Send request to AI Engine to execute the test case
            var response = await _httpClient.PostAsJsonAsync($"/run-task", requestBody);
            if (response.IsSuccessStatusCode)
            {
                // Parse response to get execution run details
                var executionRun = await response.Content.ReadFromJsonAsync<ExecutionRun>();

                // Add to execution run repository
                await _executionRunRepository.InsertNewExecutionRun(executionRun);
                // Return execution run details, will be sent as is to client to be rendered
                return executionRun;
            }
            throw new Exception($"Failed to execute test case. Status: { response.StatusCode }");
        }

        // Execute Multiple Tests
        public Task<List<ExecutionReport>> ExecuteMultipleTestCasesAsync(List<TestCase> testCases)
        {
            // Implementation for executing multiple test cases
            return Task.FromResult(new List<ExecutionReport>());
        }

        // Get Test Execution Status
        public Task<Status> GetTestExecutionStatusAsync(int executionId)
        {
            // Implementation for getting test execution status
            return Task.FromResult(new Status());
        }

        // Remove Test From Queue
        public Task<String> RemoveTestFromQueueAsync(int executionId)
        {
            // Implementation for removing a test from the execution queue
            return Task.FromResult("Test removed from queue.");
        }

        // View Execution Queue
        public Task<List<TestCase>> ViewExecutionQueueAsync()
        {
            // Implementation for viewing the execution queue
            return Task.FromResult(new List<TestCase>());
        }


    }
}
