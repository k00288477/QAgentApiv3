using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAgentApi.Model
{
    public class User
    {
        public User()
        {
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public int? OrganisationId { get; set; } // Foreign key to Organisation, nullable.
        [ForeignKey(nameof(OrganisationId))]
        public Organisation? Organisation { get; set; } // User not required to belong to an organisation. 

    }
}
