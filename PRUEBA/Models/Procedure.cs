using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRUEBA.Models
{
    public class Procedure
    {
        [Key]
        public int ProcedureID { get; set; }

        [Required] 
        [MaxLength(100)] 
        public string ProcedureName { get; set; } = string.Empty;

        [MaxLength(200)] 
        public string? Description { get; set; }


        [ForeignKey("CreatedByUserID")]
        public User? CreatedByUser { get; set; }


        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("LastModifiedUserID")]
        public User? LastModifiedByUser { get; set; }




        [Required]
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;


        [Required]
        public int CreatedByUserID { get; set; }

        [Required] 
        public int LastModifiedUserID { get; set; }


        public ICollection<DataSet>? DataSets { get; set; }
    }
}
