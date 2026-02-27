namespace QAgentApi.Model.Dto
{
    public class ExecutionReportSummaryDto
    {
        public int ExecutionReportId { get; set; }
        public int TestCaseId { get; set; }
        public string TestCaseTitle { get; set; }
        public string Status { get; set; }
        public DateTime StartedAt { get; set; }
    }
}
