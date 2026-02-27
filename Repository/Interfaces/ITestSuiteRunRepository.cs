using QAgentApi.Model;

namespace QAgentApi.Repository.Interfaces
{
    public interface ITestSuiteRunRepository
    {
        Task<TestSuiteRun> InsertNewTestSuiteRun(TestSuiteRun testSuiteRun);
        Task<TestSuiteRun?> GetSuiteById(int suiteRunId);
        Task<TestSuiteRun> UpdateSuiteRun(TestSuiteRun testSuiteRun);
        Task<List<TestSuiteRun>> GetAllSuiteRunsByTestSuiteAuthor(string author);
    }
}
