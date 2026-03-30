
using QAgentApi.Model;
using QAgentApi.Repository.Interfaces;

namespace QAgentApi.Service
{
    public class TestCaseService
    {
        // Dependency Injection
        private ITestCaseRepository _testCaseRepository;
        private ITestStepRepository _testStepRepository;
        // CONSTRUCTOR
        public TestCaseService(ITestCaseRepository testCaseRepository, ITestStepRepository testStepRepository)
        {
            _testCaseRepository = testCaseRepository;
            _testStepRepository = testStepRepository;
        }

        // METHODS
        public async Task<TestCase?> GetTestCaseById(int testCaseId) 
        {
            return await _testCaseRepository.GetTestCaseById(testCaseId);
        }

        // Edit Test Case
        public async Task<TestCase> EditTestCase(TestCase updatedTestCase)
        {
            try {
                var existingTestCase = await _testCaseRepository.GetTestCaseById(updatedTestCase.TestCaseId);
                if (existingTestCase == null)
                {
                    throw new Exception("Test case not found");
                }

                // Update properties
                existingTestCase.Title = updatedTestCase.Title;
                existingTestCase.Description = updatedTestCase.Description;
                existingTestCase.TestSuiteId = updatedTestCase.TestSuiteId;
                existingTestCase.LastModified = DateTime.UtcNow;
                // Update test steps
                if (updatedTestCase.TestSteps != null)
                {
                    // Delete existing test steps
                    var existingTestSteps = await _testStepRepository.GetTestStepsByTestCaseId(existingTestCase.TestCaseId);
                    foreach (TestStep step in existingTestSteps)
                    {
                        await _testStepRepository.DeleteTestStep(step.StepId);
                    }
                    // Add new test steps
                    foreach (var step in updatedTestCase.TestSteps)
                    {
                        step.TestCaseId = existingTestCase.TestCaseId;
                        await _testStepRepository.AddTestStep(step);
                    }
                }
                return await _testCaseRepository.UpdateTestCase(existingTestCase);

            }
            catch (Exception ex) {
                throw new Exception("Error updating test case: " + ex.Message);
            }
        }

    }
}
