using System.Text.Json.Serialization;

namespace QAgentApi.Model
{
    public class TestExecutionStatus
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("task_id")]
        public string TaskId { get; set; }

        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("result")]
        public object? Result { get; set; }
        public int ExecutionRunId { get; set; }

        public TestExecutionStatus() { }
    }
}
