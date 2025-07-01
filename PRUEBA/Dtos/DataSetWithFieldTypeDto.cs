using System;
namespace PRUEBA.Dtos
{
    public class DataSetWithFieldTypeDto
    {
        public int DataSetID { get; set; }
        public string DataSetName { get; set; }
        public string Description { get; set; }
        public int ProcedureID { get; set; }
        public string ProcedureName { get; set; }
        public int FieldId { get; set; }
        public string FieldName { get; set; } 
        public string FieldDataType { get; set; } 
        public DateTime CreatedDate { get; set; }
    }
}