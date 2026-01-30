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

        public Task<ExecutionRun> InsertNewExecutionRun(ExecutionRun executionRun)
        {
            throw new NotImplementedException();
        }
    }
}
