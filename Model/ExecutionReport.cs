using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAgentApi.Model
{
    public class ExecutionReport
    {
        public ExecutionReport() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExecutionReportId { get; set; }

        // Foreign key to ExecutionRun
        [Required]
        public int ExecutionRunId { get; set; }

        [ForeignKey(nameof(ExecutionRunId))]
        public ExecutionRun? ExecutionRun { get; set; }

        // Foreign key to TestCase
        [Required]
        public int TestCaseId { get; set; }

        [ForeignKey(nameof(TestCaseId))]
        public TestCase? TestCase { get; set; }

        // Execution metadata
        [Required]
        public DateTime ExecutionDateTime { get; set; }
        // AI API Task Information
        [Required]
        [MaxLength(500)]
        public string TaskId { get; set; } // The AI API task_id

        [MaxLength(2000)]
        public string? TaskDescription { get; set; }

        // Execution results
        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        public bool Passed { get; set; }

        public bool Failed { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public double? DurationSeconds { get; set; }

        // Error handling
        public string? ErrorMessage { get; set; }

        // Result data (stored as JSON)
        [Column(TypeName = "longtext")]
        public string? ResultJson { get; set; }

        // Recording information
        public bool RecordingAvailable { get; set; }

        [MaxLength(1000)]
        public string? RecordingUrl { get; set; }

        [Column(TypeName = "longtext")]
        public string? RecordingBase64 { get; set; }

        public List<Comment>? Comments { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
        public int? SuiteRunId { get; set; }
        [ForeignKey(nameof(SuiteRunId))]
        public TestSuiteRun? SuiteRun { get; set; }
    }
}