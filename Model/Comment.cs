using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAgentApi.Model
{
    public class Comment
    {
        public Comment() { } // Parameterless constructor for EF
        public Comment(string content, string author, int reportId)
        {
            Content = content;
            Author = author;
            DateTimeCreated = DateTime.UtcNow;
            ExecutionReportId = reportId;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public DateTime DateTimeCreated { get; set; }

        public int ExecutionReportId { get; set; } // Foreign key to ExecutionRun
        [ForeignKey(nameof(ExecutionReportId))]
        public ExecutionReport ExecutionReport { get; set; } // comments listed under an execution report
    }
}
