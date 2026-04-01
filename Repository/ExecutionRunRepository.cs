using Microsoft.EntityFrameworkCore;
using QAgentApi.Data;
using QAgentApi.Model;
using QAgentApi.Repository.Interfaces;

namespace QAgentApi.Repository
{
    public class ExecutionRunRepository : IExecutionRunRepository
    {
        private readonly AppDBContext _context;
        public ExecutionRunRepository(AppDBContext context)
        {
            _context = context;
        }
        public Task<ExecutionRun?> GetExecutionRunById(int executionRunId)
        {
            throw new NotImplementedException();
        }

        public async Task<ExecutionRun> GetExecutionRunByTaskId(string taskId)
        {
            if (taskId == null)
            {
                throw new ArgumentNullException("Task Id is null");
            }
            return await _context.ExecutionRuns.FirstOrDefaultAsync(er => er.TaskId == taskId);
        }

        public async Task<ExecutionRun> InsertNewExecutionRun(ExecutionRun executionRun)
        {
            if(executionRun == null)
            {
                throw new ArgumentNullException(nameof(executionRun));
            }
            await _context.ExecutionRuns.AddAsync(executionRun);
            await _context.SaveChangesAsync();
            return executionRun;
        }

        public async Task<IEnumerable<ExecutionRun>> GetStandaloneExecutionRunsAsync(string userEmail)
        {
            return await _context.ExecutionRuns
                .Include(er => er.TestCase)
                .Include(er => er.ExecutionReport)
                .Where(er =>
                    er.TestCase.Author == userEmail &&
                    (er.ExecutionReport == null || er.ExecutionReport.SuiteRunId == null))
                .OrderByDescending(er => er.StartTime)
                .ToListAsync();
        }
    }
}
