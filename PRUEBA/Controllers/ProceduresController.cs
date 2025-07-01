using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRUEBA.Data;
using PRUEBA.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace PRUEBA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProceduresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProceduresController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpPost]
        [Authorize(Roles = "Admin")] // Este atributo aplica la autorización por rol
        public async Task<ActionResult<Procedure>> PostProcedure(Procedure procedure)
        {
            
            _context.Procedures.Add(procedure);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProcedure", new { id = procedure.ProcedureID }, procedure);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Procedure>> GetProcedure(int id)
        {
            var procedure = await _context.Procedures.FindAsync(id);

            if (procedure == null)
            {
                return NotFound();
            }

            return procedure;
        }

        
    }
}