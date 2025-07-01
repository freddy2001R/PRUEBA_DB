﻿using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PRUEBA.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }


        [Required] 
        [MaxLength(50)] 
        public string RoleName { get; set; } = string.Empty;


        [MaxLength(255)]
        public string? Description { get; set; }

        public ICollection<UserRole>? UserRoles { get; set; }
    }
}
