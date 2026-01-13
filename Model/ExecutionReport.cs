using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAgentApi.Model
{
    public class ExecutionReport
    {
        public ExecutionReport() { }

        public ExecutionReport(TestCase testCase, DateTime executionDateTime, string executedBy, Status status, int executionRunId)
        {
            TestCase = testCase;
            ExecutionDateTime = executionDateTime;
            ExecutedBy = executedBy;
            Status = status;
            ExecutionRunId = executionRunId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExecutionReportId { get; set; }
        [Required]
        public TestCase TestCase { get; set; }
        [Required]
        public DateTime ExecutionDateTime { get; set; }
        [Required]
        [MaxLength(255)]
        public string ExecutedBy { get; set; }
        [Required]
        public Status Status { get; set; }
        public List<Comment>? Comments { get; set; }

        public int ExecutionRunId { get; set; } // Foreign key to ExecutionRun. Report is generated after run has finished
        [ForeignKey(nameof(ExecutionRunId))]
        public ExecutionRun ExecutionRun { get; set; }

    }
}
