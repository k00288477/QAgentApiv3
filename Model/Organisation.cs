using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAgentApi.Model
{
    public class Organisation
    {
        public Organisation() { }
        public Organisation(string name)
        {
            Name = name;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrgId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public List<User>? Employees { get; set; }
        public List<TestSuite>? TestSuites { get; set; }
        public List<TestCase>? TestCases { get; set; }

    }
}
