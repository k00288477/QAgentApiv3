using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAgentApi.Model
{
    public class ExecutionRun
    {
        public ExecutionRun() { }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExecutionRunId { get; set; }
        public int? ExecutionReportId { get; set; }
        [ForeignKey(nameof(ExecutionReportId))]
        public ExecutionReport? ExecutionReport { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        [Required]
        public Status Status { get; set; }
    }
}
