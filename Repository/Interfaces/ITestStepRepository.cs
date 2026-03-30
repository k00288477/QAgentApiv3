using QAgentApi.Model;

namespace QAgentApi.Repository.Interfaces
{
    public interface ITestStepRepository
    {
        Task<TestStep?> GetTestStepById(int testStepId);
        Task<TestStep> InsertNewTestStep(TestStep testStep);
        Task<TestStep> UpdateTestStep(TestStep testStep);
        Task DeleteTestStepById(int testStepId);
        Task DeleteTestStep(object testStepId);
        Task<IEnumerable<TestStep>> GetTestStepsByTestCaseId(int testCaseId);
        Task AddTestStep(TestStep step);
    }
}
