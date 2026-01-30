using QAgentApi.Model;

namespace QAgentApi.Repository.Interfaces
{
    public interface IExecutionRunRepository
    {
        Task<ExecutionRun> InsertNewExecutionRun(ExecutionRun executionRun);
        Task<ExecutionRun?> GetExecutionRunById(int executionRunId);
    }
}
