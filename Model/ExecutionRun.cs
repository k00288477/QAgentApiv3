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

        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("task_id")]
        public string? Task_id { get; set; }
        [JsonPropertyName("recording_enabled")]
        public bool RecordingEnabled { get; set; }
        [JsonPropertyName("check_status_at")]
        public string? CheckStatusUrl { get; set; }
        public ExecutionReport? ExecutionReport { get; set; } // Navigation property to ExecutionReport
    }
}
