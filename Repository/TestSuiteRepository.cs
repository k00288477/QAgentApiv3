using Microsoft.EntityFrameworkCore;
using QAgentApi.Data;
using QAgentApi.Model;
using QAgentApi.Repository.Interfaces;

namespace QAgentApi.Repository
{
    public class TestSuiteRepository : ITestSuiteRepository
    {
        private readonly AppDBContext _context;
        public TestSuiteRepository(AppDBContext context)
        {
            _context = context;
        }
        public Task DeleteTestSuiteById(int testSuiteId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TestSuite>?> GetAllTestSuitesByAuthor(string authorEmail)
        {
            return await _context.TestSuites
                .Where(ts => ts.Author == authorEmail)
                .Include(ts => ts.TestCases)
                .ToListAsync();
        }

        public async Task<TestSuite?> GetTestSuiteById(int testSuiteId)
        {
            return await _context.TestSuites
                .Where(ts => ts.TestSuiteId == testSuiteId)
                .Include(ts => ts.TestCases)
                .FirstOrDefaultAsync();
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
