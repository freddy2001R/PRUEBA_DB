using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace PRUEBA.Models
{
    public class UserRole
    {
        [Key] 
        public int ID { get; set; }

        [Required]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }



        [Required]
        public int RoleID { get; set; }

        [ForeignKey("RoleID")]
        public Role? Role { get; set; }

    }
}
