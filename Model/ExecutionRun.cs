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
        public int TestCaseId { get; set; }
        [ForeignKey(nameof(TestCaseId))]
        public TestCase TestCase { get; set; }



        // AI Engine Response fields
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("task_id")]
        public string? TaskId { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("recording_enabled")]
        public bool RecordingEnabled { get; set; }

        [JsonPropertyName("check_status_at")]
        public string? CheckStatusUrl { get; set; }

        [JsonPropertyName("report_at")]
        public string? ReportUrl { get; set; }



        // Timestamps
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime? EndTime { get; set; }
        public ExecutionReport? ExecutionReport { get; set; }
    }
}
