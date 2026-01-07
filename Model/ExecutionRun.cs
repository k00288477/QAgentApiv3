using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAgentApi.Model
{
    public class ExecutionRun
    {
        public ExecutionRun() { }

        public ExecutionRun(TimeOnly startTime, Status status)
        {
            StartTime = startTime;
            Status = status;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExecutionRunId { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        [Required]
        public Status Status { get; set; }

        public ExecutionReport? ExecutionReport { get; set; } // Navigation property to ExecutionReport
    }
}
