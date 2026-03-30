using QAgentApi.Model;
using QAgentApi.Repository.Interfaces;

namespace QAgentApi.Service
{
    public class TestSuiteService
    {
        // Dependency Injection
        private readonly ITestSuiteRepository _testSuiteRepository;
        private readonly ITestCaseRepository _testCaseRepository;
        private readonly TestCaseService _testCaseService;
        // CONSTRUCTOR
        public TestSuiteService(ITestSuiteRepository testSuiteRepository, ITestCaseRepository testCaseRepository, TestCaseService testCaseService)
        {
            _testSuiteRepository = testSuiteRepository;
            _testCaseRepository = testCaseRepository;
            _testCaseService = testCaseService;
        }

        // METHODS
        public async Task<List<TestSuite>> GetAllTestSuitesByAuthor(string authorEmail)
        {
            return await _testSuiteRepository.GetAllTestSuitesByAuthor(authorEmail);
        }

        public async Task<TestSuite> AddNewTestSuite(TestSuite testSuite)
        {
            return await _testSuiteRepository.InsertNewTestSuite(testSuite);
        }

        public async Task<TestCase> AddNewTestCase(TestCase testCase)
        {
            return await _testCaseRepository.InsertNewTestCase(testCase);
        }

        public async Task<TestSuite> EditTestSuite(TestSuite updatedTestSuite)
        {
            try
            {
                var existingTestSuite = await _testSuiteRepository.GetTestSuiteById(updatedTestSuite.TestSuiteId);
                if (existingTestSuite == null)
                {
                    throw new Exception("Test suite not found");
                }
                // Update properties
                existingTestSuite.Title = updatedTestSuite.Title;
                existingTestSuite.Description = updatedTestSuite.Description;
                return await _testSuiteRepository.UpdateTestSuite(existingTestSuite);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating test suite: " + ex.Message);
            }

        }

        public async Task DeleteTestSuite(int testSuiteId)
        {
            try
            {
                // First delete all test cases associated with the test suite
                var existingTestSuite = await _testSuiteRepository.GetTestSuiteById(testSuiteId);
                if (existingTestSuite == null)
                {
                    throw new Exception("Test suite not found");
                }
                if (existingTestSuite.TestCases != null)
                {
                    foreach (var testCase in existingTestSuite.TestCases)
                    {
                        await _testCaseService.DeleteTestCase(testCase.TestCaseId);
                    }
                }
                // Then delete the test suite itself
                await _testSuiteRepository.DeleteTestSuiteById(testSuiteId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting test suite: " + ex.Message);
            }
        }
    }
}
