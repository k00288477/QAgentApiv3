using QAgentApi.Model;

namespace QAgentApi.Repository.Interfaces
{
    public interface ITestCaseRepository
    {
        Task<TestCase?> GetTestCaseById(int testCaseId);
        Task<TestCase> InsertNewTestCase(TestCase testCase);
        Task<TestCase> UpdateTestCase(TestCase testCase);
        Task DeleteTestCaseById(int testCaseId);
    }
}
