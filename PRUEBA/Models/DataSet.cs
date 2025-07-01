using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
using System; 

namespace PRUEBA.Models
{
    public class DataSet
    {
        [Key]
        public int DataSetID { get; set; }
        

        [Required]
        [MaxLength(100)]
        public string DataSetName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }


        [ForeignKey("ProcedureID")]
        public Procedure? Procedure { get; set; }

        [ForeignKey("FieldId")] 
        public Field? Field { get; set; }


        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }


        [Required] 
        public int ProcedureID { get; set; }

        [Required]
        public int FieldId { get; set; }


    }
}
