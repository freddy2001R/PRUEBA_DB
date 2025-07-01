using System;
namespace PRUEBA.Dtos
{
    public class DataSetDto
    {

        public int DataSetID { get; set; }
        public string DataSetName { get; set; }
        public string Description { get; set; }
        public int ProcedureID { get; set; }
        public string ProcedureName { get; set; } 
        public DateTime CreatedDate { get; set; }
        
    }
}
