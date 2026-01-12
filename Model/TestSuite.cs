using System.ComponentModel.DataAnnotations.Schema;

namespace QAgentApi.Model
{
    public class TestSuite
    {
        public TestSuite() { }
        public TestSuite(int testSuiteId, string title, string description, int? organisationId, string author)
        {
            TestSuiteId = testSuiteId;
            Title = title;
            Description = description;
            OrganisationId = organisationId;
            Author = author;
            DateCreated = DateTime.UtcNow;
        }


        public int TestSuiteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? OrganisationId { get; set; }
        [ForeignKey(nameof(OrganisationId))]
        public Organisation? Organisation { get; set; }
        public string Author { get; set; }
        public DateTime DateCreated { get; set; }
        public List<TestCase>? TestCases { get; set; }
    }
}
