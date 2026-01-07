using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAgentApi.Model
{
    public class TestStep
    {
        public TestStep() { }
        public TestStep(string content, string index, int testCaseId)
        {
            Content = content;
            Index = index;
            TestCaseId = testCaseId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StepId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Index { get; set; } // represents step sequence
        [Required]
        public int TestCaseId { get; set; } // Must be linked to a TestCase
        [ForeignKey(nameof(TestCaseId))]
        public TestCase TestCase { get; set; } 
    }
}
