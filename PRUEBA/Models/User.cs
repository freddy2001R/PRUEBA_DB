using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace PRUEBA.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; } 

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty; 

        [Required]
       
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(70)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty; 

        public ICollection<UserRole>? UserRoles { get; set; }

       
        public ICollection<Procedure>? CreatedProcedures { get; set; }
        public ICollection<Procedure>? ModifiedProcedures { get; set; }
    }
}
