using System.Collections.Generic;
namespace PRUEBA.Dtos
{
    public class UserDataSetResponseDto
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<DataSetDto> CreatedOrModifiedDataSets { get; set; } = new List<DataSetDto>();

    }
}
