using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAgentApi.Model
{
    public class TestCase
    {
        public TestCase() { }
        public TestCase(int testCaseId, string title, string description, string author)
        {
            TestCaseId = testCaseId;
            Title = title;
            Description = description;
            Author = author;
            DateCreated = DateTime.UtcNow;
        }
        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestCaseId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        public int? TestSuiteId { get; set; } // May not be part of a suite
        [Required]
        [MaxLength(255)]
        public string Author { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime? LastModified { get; set; }
        public int? UserId { get; set; } // Foreign key to User who last modified
        [ForeignKey(nameof(UserId))]
        public User? LastModifiedBy { get; set; }
        public List<TestStep> TestSteps { get; set; } = new List<TestStep>();
    }
}
