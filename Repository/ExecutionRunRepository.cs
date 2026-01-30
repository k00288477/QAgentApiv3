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
    }
}
