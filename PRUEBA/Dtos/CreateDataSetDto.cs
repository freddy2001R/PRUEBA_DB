using System.ComponentModel.DataAnnotations; 
using System;

namespace PRUEBA.Dtos
{
    public class CreateDataSetDto
    {
        [Required(ErrorMessage = "El nombre del DataSet es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del DataSet no puede exceder los 100 caracteres.")]
        public string DataSetName { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El ProcedureID es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ProcedureID debe ser un número positivo.")]
        public int ProcedureID { get; set; } 

        [Required(ErrorMessage = "El FieldId es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El FieldId debe ser un número positivo.")]
        public int FieldId { get; set; } 

        
    }
}