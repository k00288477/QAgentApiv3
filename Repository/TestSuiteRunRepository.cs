using Microsoft.EntityFrameworkCore;
using QAgentApi.Data;
using QAgentApi.Model;
using QAgentApi.Repository.Interfaces;

namespace QAgentApi.Repository
{
    public class TestSuiteRunRepository : ITestSuiteRunRepository
    {
        private readonly AppDBContext _context;
        public TestSuiteRunRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<TestSuiteRun>> GetAllSuiteRunsByTestSuiteAuthor(string author)
        {
            return await _context.TestSuiteRuns
                .Include(sr => sr.TestSuite)
                .Include(sr => sr.ExecutionReports)
                    .ThenInclude(er => er.TestCase)
                .Where(sr => sr.TestSuite.Author == author)
                .OrderByDescending(sr => sr.StartedAt)
                .ToListAsync();
        }

        public async Task<TestSuiteRun?> GetSuiteById(int suiteRunId)
        {
            return await _context.TestSuiteRuns
                .Include(sr => sr.ExecutionReports)
                .ThenInclude(er => er.TestCase)
                .FirstOrDefaultAsync(sr => sr.SuiteRunId == suiteRunId);
        }

        public async Task<TestSuiteRun> InsertNewTestSuiteRun(TestSuiteRun testSuiteRun)
        {
            if(testSuiteRun == null)
            {
                throw new ArgumentNullException(nameof(testSuiteRun));
            }
            await _context.TestSuiteRuns.AddAsync(testSuiteRun);
            await _context.SaveChangesAsync();
            return testSuiteRun;
        }

        public async Task<TestSuiteRun> UpdateSuiteRun(TestSuiteRun testSuiteRun)
        {
            if(testSuiteRun == null)
            {
                throw new ArgumentNullException(nameof(testSuiteRun));
            }
            _context.TestSuiteRuns.Update(testSuiteRun);
            await _context.SaveChangesAsync();
            return testSuiteRun;
        }
    }
}
