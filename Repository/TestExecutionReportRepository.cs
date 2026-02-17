using Microsoft.EntityFrameworkCore;
using QAgentApi.Data;
using QAgentApi.Model;
using QAgentApi.Repository.Interfaces;
using System.Threading.Tasks;

namespace QAgentApi.Repository
{
    public class TestExecutionReportRepository : ITestExecutionReportRepository
    {
        private readonly AppDBContext _context;
        public TestExecutionReportRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task DeleteCommentById(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteExecutionReportById(int executionReportId)
        {
            var report = await _context.ExecutionReports.FindAsync(executionReportId);
            if (report != null)
            {
                _context.ExecutionReports.Remove(report);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<ExecutionReport?> GetExecutionReportById(int executionReportId)
        {
            return await _context.ExecutionReports.FirstOrDefaultAsync(e => e.ExecutionReportId == executionReportId);
        }

        public async Task<ExecutionReport> InsertNewCommentOnExecutionReport(Comment comment)
        {
            if (comment != null)
            {
                await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();
                return await _context.ExecutionReports
                    .FirstOrDefaultAsync(er => er.ExecutionReportId == comment.ExecutionReportId);
            }
            return null;
        }

        public async Task<ExecutionReport> InsertNewExecutionReport(ExecutionReport executionReport)
        {
            if(executionReport != null)
            { 
                await _context.ExecutionReports.AddAsync(executionReport);
                await _context.SaveChangesAsync();
                return await Task.FromResult(executionReport);
            }
            return null;
        }

        public async Task<ExecutionReport> UpdateCommentById(Comment comment)
        {
            if (comment != null)
            {
                _context.Comments.Update(comment);
                await _context.SaveChangesAsync();
                return await _context.ExecutionReports
                    .FirstOrDefaultAsync(er => er.ExecutionReportId == comment.ExecutionReportId);
            }
            return null;
        }

        public async Task<ExecutionReport> GetExecutionRunByExecutionRunId(int executionRunId) 
        {
            return await _context.ExecutionReports
                .FirstOrDefaultAsync(er => er.ExecutionRunId == executionRunId);
        }
    }
}
