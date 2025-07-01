using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRUEBA.Data; 
using PRUEBA.Models; 
using PRUEBA.Dtos;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;
using System; 

namespace PRUEBA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSetsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DataSetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpPost]
        public async Task<ActionResult<DataSet>> PostDataSet([FromBody] CreateDataSetDto createDataSetDto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            
            var existingProcedure = await _context.Procedures.FindAsync(createDataSetDto.ProcedureID);
            if (existingProcedure == null)
            {
                return BadRequest($"ProcedureID {createDataSetDto.ProcedureID} no existe.");
            }

     
            var existingField = await _context.Fields.FindAsync(createDataSetDto.FieldId);
            if (existingField == null)
            {
                return BadRequest($"FieldId {createDataSetDto.FieldId} no existe.");
            }

            
            var dataSet = new DataSet
            {
                DataSetName = createDataSetDto.DataSetName,
                Description = createDataSetDto.Description,
                ProcedureID = createDataSetDto.ProcedureID,
                FieldId = createDataSetDto.FieldId,
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,
            };

            _context.DataSets.Add(dataSet);
            await _context.SaveChangesAsync();

           
            return CreatedAtAction(nameof(GetDataSet), new { id = dataSet.DataSetID }, dataSet);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<DataSet>> GetDataSet(int id)
        {
            if (_context.DataSets == null)
            {
                return NotFound();
            }
            var dataSet = await _context.DataSets.FindAsync(id);

            if (dataSet == null)
            {
                return NotFound();
            }

            return dataSet;
        }

        
        // *********  CÓDIGO  PARA LA TAREA 3 ****
       
        [HttpGet("ByProcedure/{procedureId}")]
        public async Task<ActionResult<IEnumerable<DataSetWithFieldTypeDto>>> GetDataSetsByProcedure(int procedureId)
        {
            // 1. Verificar si el procedimiento existe
            var existingProcedure = await _context.Procedures.FindAsync(procedureId);
            if (existingProcedure == null)
            {
                return NotFound($"Procedure con ID {procedureId} no encontrado.");
            }

           
            var dataSets = await _context.DataSets
                                         .Where(ds => ds.ProcedureID == procedureId)
                                         .Include(ds => ds.Procedure) 
                                         .Include(ds => ds.Field)     
                                         .Select(ds => new DataSetWithFieldTypeDto
                                         {
                                             DataSetID = ds.DataSetID,
                                             DataSetName = ds.DataSetName,
                                             Description = ds.Description,
                                             ProcedureID = ds.ProcedureID,
                                             ProcedureName = ds.Procedure.ProcedureName, 
                                             FieldId = ds.FieldId,
                                             FieldName = ds.Field.FieldName, 
                                             FieldDataType = ds.Field.DataType, 
                                             CreatedDate = ds.CreatedDate
                                         })
                                         .ToListAsync();

           
            if (!dataSets.Any())
            {
                return Ok(new List<DataSetWithFieldTypeDto>());
            }

            return Ok(dataSets);
        }

  
    }
}
