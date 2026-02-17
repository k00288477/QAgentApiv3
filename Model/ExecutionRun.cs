using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QAgentApi.Model
{
    public class ExecutionRun
    {
        public ExecutionRun() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExecutionRunId { get; set; }
        [Required]

        [Column("TestCaseId")]
        public int TestCaseId { get; set; }
        [ForeignKey(nameof(TestCaseId))]
        public TestCase TestCase { get; set; }



        // AI Engine Response fields
        [JsonPropertyName("success")]
        [Column("Success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        [Column("Message")]
        public string? Message { get; set; }

        [JsonPropertyName("task_id")]
        [Column("Task_id")]
        public string? TaskId { get; set; }

        [JsonPropertyName("status")]
        [Column("Status")]
        public string? Status { get; set; }

        [JsonPropertyName("recording_enabled")]
        [Column("RecordingEnabled")]
        public bool RecordingEnabled { get; set; }

        [JsonPropertyName("check_status_at")]
        [Column("CheckStatusUrl")]
        public string? CheckStatusUrl { get; set; }

        [JsonPropertyName("report_at")]
        [Column("ReportUrl")]
        public string? ReportUrl { get; set; }



        // Timestamps
        [JsonPropertyName("startTime")]
        [Column("StartTime")]
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        [Column("EndTime")]
        public DateTime? EndTime { get; set; }
        public ExecutionReport? ExecutionReport { get; set; }
    }
}
