using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAgentApi.Model
{
    public class User
    {
        public User() { } // Parameterless constructor for EF
        public User(string email, string password)
        {
            Email = email;
            PasswordHash = password;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(255)]
        public string? Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string Email { get; set; }
        public int? OrganisationId { get; set; } // Foreign key to Organisation, nullable.
        [ForeignKey(nameof(OrganisationId))]
        public Organisation? Organisation { get; set; } // User not required to belong to an organisation. 

        // auth
        [Required]
        [MaxLength(500)]
        public string PasswordHash { get; set; }

    }
}
