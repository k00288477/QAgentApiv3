using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAgentApi.Model
{
    public class TestSuiteRun
    {
        public TestSuiteRun() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SuiteRunId { get; set; }

        [Required]
        public int TestSuiteId { get; set; }
        [ForeignKey(nameof(TestSuiteId))]
        public TestSuite TestSuite { get; set; }

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Running"; // "Running", "Passed", "Failed"

        public int TotalTests { get; set; }
        public int CompletedTests { get; set; }

        public List<ExecutionReport> ExecutionReports { get; set; } = new List<ExecutionReport>();
    }
}