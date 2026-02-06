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
using System.Text;
using System.Text.Json;

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

            // Test Steps must be formated as a single string
            var task = FormatTestStepsAsTask(testCase.TestSteps);
            // Create request body, passing Test Steps
            var requestBody = new
            {
                task = task,
                generate_gif = true,
                headless = true
            };
            // Send request to AI Engine to execute the test case
            var response = await _httpClient.PostAsJsonAsync($"/run-task", requestBody);

            if (response.IsSuccessStatusCode)
            {
                try
                {   // Read response content
                    var executionRun = await response.Content.ReadFromJsonAsync<ExecutionRun>();

                    if (executionRun == null)
                    {
                        throw new Exception($"Failed to deserialize execution run response. Raw response: {response.Content}");
                    }

                    // Save to Database
                    await _executionRunRepository.InsertNewExecutionRun(executionRun);
                    return executionRun;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during processing: {ex.Message}");
                    throw;
                }
            }

            throw new Exception($"Failed to execute test case. Status: {response.StatusCode}");
        }

        // Format Test Steps As Task
        private object FormatTestStepsAsTask(List<TestStep> testSteps)
        {
            if (testSteps == null)
            {
                throw new ArgumentException("Test steps cannot be null.");
            }

            // Order test steps by Index
            var orderedSteps = testSteps.OrderBy(step => step.Index).ToList();

            var taskBuilder = new StringBuilder();
            foreach (var step in orderedSteps)
            {
                taskBuilder.AppendLine($"Step {step.Index}: {step.Action}");
                taskBuilder.AppendLine($"Expected Result: {step.ExpectedResult}");
                taskBuilder.AppendLine(); // Add a blank line between steps
            }

            return taskBuilder.ToString();
        }


        // Execute Multiple Tests
        public Task<List<ExecutionReport>> ExecuteMultipleTestCasesAsync(List<TestCase> testCases)
        {
            // Implementation for executing multiple test cases
            return Task.FromResult(new List<ExecutionReport>());
        }

        // Get Test Execution Status
        public async Task<TestExecutionStatus> GetTestExecutionStatusAsync(int executionId)
        {
            // Implementation for retrieving the status of a test execution, will be called every 15 seconds by the frontend until the execution is complete
            try
            {
                // Call AI Engine API to get staus of the test execution
                var response = await _httpClient.GetAsync($"/task/{executionId}");

                if (response.IsSuccessStatusCode)
                {
                    // Read response content
                    var status = await response.Content.ReadFromJsonAsync<TestExecutionStatus>();
                    if (status == null)
                    {
                        throw new Exception($"Failed to deserialize execution run response. Raw response: {response.Content}");
                    }
                    return status;
                } else
                {
                    throw new Exception($"Failed to retrieve test execution status. Status: {response.StatusCode}");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during processing: {ex.Message}");
                throw;
            }
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
