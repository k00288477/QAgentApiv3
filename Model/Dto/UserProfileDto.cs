using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAgentApi.Model.Dto
{
    public class UserProfileDto
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int? OrganisationId { get; set; } // Foreign key to Organisation, nullable.
        public Organisation? Organisation { get; set; } // User not required to belong to an organisation. 

    }
}
