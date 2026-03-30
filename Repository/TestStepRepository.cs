using Microsoft.EntityFrameworkCore;
using QAgentApi.Data;
using QAgentApi.Model;
using QAgentApi.Repository.Interfaces;

namespace QAgentApi.Repository
{
    public class TestStepRepository : ITestStepRepository
    {
        private readonly AppDBContext _context;
        public TestStepRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<TestStep> AddTestStep(TestStep step)
        {
            _context.TestSteps.Add(step);
            await _context.SaveChangesAsync();
            return step;
        }

        public async Task DeleteTestStep(int stepId)
        {
            var step = await _context.TestSteps.FindAsync(stepId);
            if (step != null)
            {
                _context.TestSteps.Remove(step);
                await _context.SaveChangesAsync();
            }
        }

        public Task DeleteTestStepById(int testStepId)
        {
            throw new NotImplementedException();
        }

        public Task<TestStep?> GetTestStepById(int testStepId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TestStep>> GetTestStepsByTestCaseId(int testCaseId)
        {
            return await _context.TestSteps
                .Where(ts => ts.TestCaseId == testCaseId)
                .OrderBy(ts => ts.Index)
                .ToListAsync();
        }

        public Task<TestStep> InsertNewTestStep(TestStep testStep)
        {
            throw new NotImplementedException();
        }

        public Task<TestStep> UpdateTestStep(TestStep testStep)
        {
            throw new NotImplementedException();
        }
    }
}
