using QAgentApi.Model;
using QAgentApi.Repository.Interfaces;

namespace QAgentApi.Repository
{
    public class TestCaseRepository : ITestCaseRepository
    {
        public Task DeleteTestCaseById(int testCaseId)
        {
            throw new NotImplementedException();
        }

        public Task<TestCase?> GetTestCaseById(int testCaseId)
        {
            throw new NotImplementedException();
        }

        public Task<TestCase> InsertNewTestCase(TestCase testCase)
        {
            throw new NotImplementedException();
        }

        public Task<TestCase> UpdateTestCase(TestCase testCase)
        {
            throw new NotImplementedException();
        }
    }
}
