using QAgentApi.Model;

namespace QAgentApi.Repository.Interfaces
{
    public interface ITestStepRepository
    {
        Task<TestStep?> GetTestStepById(int testStepId);
        Task<TestStep> InsertNewTestStep(TestStep testStep);
        Task<TestStep> UpdateTestStep(TestStep testStep);
        Task DeleteTestStepById(int testStepId);
        Task DeleteTestStep(int testStepId);
        Task<IEnumerable<TestStep>> GetTestStepsByTestCaseId(int testCaseId);
        Task<TestStep> AddTestStep(TestStep step);
    }
}
