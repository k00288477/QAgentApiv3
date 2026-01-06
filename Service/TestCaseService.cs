using QAgentApi.Repository;

namespace QAgentApi.Service
{
    public class TestCaseService
    {
        // Dependency Injection
        private TestCaseRepository _testCaseRepository;
        private TestStepRepository _testStepRepository;
        // CONSTRUCTOR
        public TestCaseService(TestCaseRepository testCaseRepository, TestStepRepository testStepRepository)
        {
            _testCaseRepository = testCaseRepository;
            _testStepRepository = testStepRepository;
        }

        // METHODS
    }
}
