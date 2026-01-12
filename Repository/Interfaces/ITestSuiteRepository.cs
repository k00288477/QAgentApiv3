using QAgentApi.Model;

namespace QAgentApi.Repository.Interfaces
{
    public interface ITestSuiteRepository
    {
        Task<TestSuite?> GetTestSuiteById(int testSuiteId);
        Task<List<TestSuite>?> GetAllTestSuitesByAuthor(string authorEmail);
        Task<TestSuite> InsertNewTestSuite(TestSuite testSuite);
        Task<TestSuite> UpdateTestSuite(TestSuite testSuite);
        Task DeleteTestSuiteById(int testSuiteId);
    }
}
