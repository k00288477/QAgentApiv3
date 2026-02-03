
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
    }
}
