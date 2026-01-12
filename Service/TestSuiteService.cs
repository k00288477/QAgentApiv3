using QAgentApi.Model;
using QAgentApi.Repository.Interfaces;

namespace QAgentApi.Service
{
    public class TestSuiteService
    {
        // Dependency Injection
        private readonly ITestSuiteRepository _testSuiteRepository;
        private readonly ITestCaseRepository _testCaseRepository;
        // CONSTRUCTOR
        public TestSuiteService(ITestSuiteRepository testSuiteRepository, ITestCaseRepository testCaseRepository    )
        {
            _testSuiteRepository = testSuiteRepository;
            _testCaseRepository = testCaseRepository;
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

    }
}
