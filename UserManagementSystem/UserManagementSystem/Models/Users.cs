using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserManagementSystem.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("First Name")]
        [StringLength(50)]
        public string? FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        [StringLength(50)]
        public string? LastName { get; set; }
        [StringLength(10)]
        public string? Username { get; set; }
        [StringLength(50)]
        public string? Password { get; set; }
        [EmailAddress(ErrorMessage = "The email address is not valid")]
        public string? Email { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public bool Code { get; set; }
        public bool Description { get; set; }
    }
}
