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
            try {
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

                    executionRun.TestCaseId = testCaseId;
                    // Save to Database
                    await _executionRunRepository.InsertNewExecutionRun(executionRun);
                    return executionRun;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during processing: {ex.Message}");
                    Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                    throw new Exception($"Database error: {ex.InnerException?.Message ?? ex.Message}", ex);
                }
            }

            throw new Exception($"Failed to execute test case. Status: {response.StatusCode}");
        }
            catch (Exception ex)
    {
        Console.WriteLine($"Full error: {ex}");
        throw;
    }
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
        public async Task<TestExecutionStatus> GetTestExecutionStatusAsync(string executionId)
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
                }
                else
                {
                    throw new Exception($"Failed to retrieve test execution status. Status: {response.StatusCode}");

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during processing: {ex.Message}");
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

        // Get Test Execution Report
        public async Task<ExecutionReport> GetExecutionReportFromDatabaseAsync(int executionRunId)
        {
            try {                 // Get the ExecutionRun from the database
                var executionReport = await _executionReportRepository.GetExecutionRunByExecutionRunId(executionRunId);
                if (executionReport == null)
                {
                    throw new Exception($"Execution run with ID {executionRunId} not found in database.");
                }
                return executionReport;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during processing: {ex.Message}");
            }
        }
        public async Task<ExecutionReport> GetTestExecutionReportAndSaveToDatabaseAsync(string taskId)
        {
            // Implementation for retrieving the test execution report after execution is complete
            try
            {
                // Get the execution report from the AI Engine
                var apiReportResponse = await _httpClient.GetAsync($"/task/{taskId}/report");

                // Check if failed to retrieve report
                if (!apiReportResponse.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to retrieve execution report. {apiReportResponse.StatusCode}");
                }

                // Deserialize the API response to create a Test Report object
                var apiReport = await apiReportResponse.Content.ReadFromJsonAsync<TestReportResponse>();

                // Check if null
                if(apiReport == null)
                {
                    throw new Exception($"Failed to deserialize execution report response. Raw response: {apiReportResponse.Content}");
                }

                var reportData = apiReport.TestReport;

                // Get the ExecutionRun from the database
                var executionRun = await _executionRunRepository.GetExecutionRunByTaskId(taskId);
                if (executionRun == null)
                {
                    throw new Exception($"Execution run with ID {taskId} not found in database."); 
                }

                // create the execution report 
                var executionReport = new ExecutionReport
                {
                    ExecutionRunId = executionRun.ExecutionRunId,
                    TestCaseId = executionRun.TestCaseId,
                    ExecutionDateTime = DateTime.UtcNow,
                    TaskId = reportData.TaskId, // From AI Engine
                    TaskDescription = reportData.TaskDescription,
                    Status = reportData.Status,
                    Passed = reportData.Passed,
                    Failed = reportData.Failed,
                    StartTime = ParseDateTime(reportData.StartTime),
                    EndTime = ParseDateTime(reportData.EndTime),
                    DurationSeconds = reportData.DurationSeconds,
                    ErrorMessage = reportData.ErrorMessage,
                    ResultJson = reportData.Result,
                    RecordingAvailable = reportData.RecordingAvailable,
                    RecordingUrl = reportData.RecordingUrl,
                    RecordingBase64 = reportData.RecordingBase64
                };

                // Save the execution report to the database
                await _executionReportRepository.InsertNewExecutionReport(executionReport);
                return executionReport;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during processing: {ex.Message}");
            }
        }

        private DateTime? ParseDateTime(string? dateTimeString)
        {
            if (string.IsNullOrEmpty(dateTimeString))
                return null;

            if (DateTime.TryParse(dateTimeString, out var result))
                return result;

            return null;
        }
    }
}
