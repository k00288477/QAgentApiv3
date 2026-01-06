using QAgentApi.Repository;

namespace QAgentApi.Service
{
    public class TestSuiteService
    {
        // Dependency Injection
        private TestSuiteRepository _testSuiteRepository;
        // CONSTRUCTOR
        public TestSuiteService(TestSuiteRepository testSuiteRepository)
        {
            _testSuiteRepository = testSuiteRepository;
        }

        // METHODS
    }
}
