using QAgentApi.Data;
using QAgentApi.Model;
using QAgentApi.Repository.Interfaces;

namespace QAgentApi.Repository
{
    public class TestCaseRepository : ITestCaseRepository
    {
        private readonly AppDBContext _context;
        public TestCaseRepository(AppDBContext context)
        {
            _context = context;
        }
        public Task DeleteTestCaseById(int testCaseId)
        {
            throw new NotImplementedException();
        }

        public Task<TestCase?> GetTestCaseById(int testCaseId)
        {
            throw new NotImplementedException();
        }

        public async Task<TestCase> InsertNewTestCase(TestCase testCase)
        {
            await _context.TestCases.AddAsync(testCase);
            await _context.SaveChangesAsync();
            return testCase;
        }

        public Task<TestCase> UpdateTestCase(TestCase testCase)
        {
            throw new NotImplementedException();
        }
    }
}
