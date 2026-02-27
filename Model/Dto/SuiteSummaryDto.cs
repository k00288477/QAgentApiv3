namespace QAgentApi.Model.Dto
{
    public class SuiteSummaryDto
    {
        public int SuiteRunId { get; set; }
        public int TestSuiteId { get; set; }
        public string TestSuiteTitle { get; set; }
        public string Status { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public List<ExecutionReportSummaryDto> ExecutionReports { get; set; }
    }
}
