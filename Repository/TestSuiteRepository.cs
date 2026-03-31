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
        public async Task DeleteTestSuiteById(int testSuiteId)
        {
            var testSuite = await _context.TestSuites.FindAsync(testSuiteId);
            if (testSuite != null)
            {
                _context.TestSuites.Remove(testSuite);
                await _context.SaveChangesAsync();
            }
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
                .Include(ts => ts.TestCases!)
                .ThenInclude(tc => tc.TestSteps)
                .FirstOrDefaultAsync();
        }

        public async Task<TestSuite> InsertNewTestSuite(TestSuite testSuite)
        {
            await _context.TestSuites.AddAsync(testSuite);
            await  _context.SaveChangesAsync();
            return testSuite;
        }

        public async Task<TestSuite> UpdateTestSuite(TestSuite testSuite)
        {
            _context.TestSuites.Update(testSuite);
            await _context.SaveChangesAsync();
            return testSuite;

        }
    }
}
