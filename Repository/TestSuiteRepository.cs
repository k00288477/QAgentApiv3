using QAgentApi.Model;
using QAgentApi.Repository.Interfaces;

namespace QAgentApi.Repository
{
    public class TestSuiteRepository : ITestSuiteRepository
    {
        public Task DeleteTestSuiteById(int testSuiteId)
        {
            throw new NotImplementedException();
        }

        public Task<TestSuite?> GetTestSuiteById(int testSuiteId)
        {
            throw new NotImplementedException();
        }

        public Task<TestSuite> InsertNewTestSuite(TestSuite testSuite)
        {
            throw new NotImplementedException();
        }

        public Task<TestSuite> UpdateTestSuite(TestSuite testSuite)
        {
            throw new NotImplementedException();
        }
    }
}
