using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PRUEBA.Models
{
    public class Field
    {

        [Key] 
        public int FieldID { get; set; }


        [Required] 
        [MaxLength(100)] 
        public string FieldName { get; set; } = string.Empty;


        [Required] 
        [MaxLength(50)] 
        public string DataType { get; set; } = string.Empty;

        public ICollection<DataSet>? DataSets { get; set; }
    }
}
