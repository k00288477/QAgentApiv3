using QAgentApi.Repository.Interfaces;

namespace QAgentApi.Service
{
    public class TestSuiteService
    {
        // Dependency Injection
        private ITestSuiteRepository _testSuiteRepository;
        // CONSTRUCTOR
        public TestSuiteService(ITestSuiteRepository testSuiteRepository)
        {
            _testSuiteRepository = testSuiteRepository;
        }

        // METHODS
    }
}
