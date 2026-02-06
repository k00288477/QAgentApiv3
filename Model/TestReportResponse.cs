using System.Text.Json.Serialization;

namespace QAgentApi.Model
{
    public class TestReportResponse
    {
        [JsonPropertyName("test_report")]
        public TestReportData TestReport { get; set; }
    }

    public class TestReportData
    {
        [JsonPropertyName("task_id")]
        public string TaskId { get; set; }

        [JsonPropertyName("task_description")]
        public string TaskDescription { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("start_time")]
        public string? StartTime { get; set; }

        [JsonPropertyName("end_time")]
        public string? EndTime { get; set; }

        [JsonPropertyName("duration_seconds")]
        public double? DurationSeconds { get; set; }

        [JsonPropertyName("passed")]
        public bool Passed { get; set; }

        [JsonPropertyName("failed")]
        public bool Failed { get; set; }

        [JsonPropertyName("error_message")]
        public string? ErrorMessage { get; set; }

        [JsonPropertyName("result")]
        public string? Result { get; set; }

        [JsonPropertyName("recording_available")]
        public bool RecordingAvailable { get; set; }

        [JsonPropertyName("recording_url")]
        public string? RecordingUrl { get; set; }

        [JsonPropertyName("recording_base64")]
        public string? RecordingBase64 { get; set; }
    }
}