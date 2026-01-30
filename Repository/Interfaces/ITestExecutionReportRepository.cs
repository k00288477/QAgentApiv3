using QAgentApi.Model;

namespace QAgentApi.Repository.Interfaces
{
    public interface ITestExecutionReportRepository
    {
        Task<ExecutionReport?> GetExecutionReportById(int executionReportId);
        Task<ExecutionReport> InsertNewExecutionReport(ExecutionReport executionReport);
        Task<ExecutionReport> InsertNewCommentOnExecutionReport(Comment comment);
        Task<ExecutionReport> UpdateCommentById(Comment comment);
        Task DeleteCommentById(int commentId);
        Task DeleteExecutionReportById(int executionReportId);
    }
}
