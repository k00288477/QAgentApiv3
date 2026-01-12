using QAgentApi.Model;
using QAgentApi.Repository.Interfaces;

namespace QAgentApi.Service
{
    public class TestSuiteService
    {
        // Dependency Injection
        private readonly ITestSuiteRepository _testSuiteRepository;
        // CONSTRUCTOR
        public TestSuiteService(ITestSuiteRepository testSuiteRepository)
        {
            _testSuiteRepository = testSuiteRepository;
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
    }
}
