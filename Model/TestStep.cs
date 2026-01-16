using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAgentApi.Model
{
    public class TestStep
    {
        public TestStep() { }
        public TestStep(string action, string expectedResult, int index, int testCaseId)
        {
            Action = action;
            ExpectedResult = expectedResult;
            Index = index;
            TestCaseId = testCaseId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StepId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Action { get; set; }
        [Required]
        [MaxLength(255)]
        public string ExpectedResult { get; set; }
        [Required]
        public int Index { get; set; } // represents step sequence
        [Required]
        public int TestCaseId { get; set; } // Must be linked to a TestCase
        [ForeignKey(nameof(TestCaseId))]
        public TestCase? TestCase { get; set; } 
    }
}
