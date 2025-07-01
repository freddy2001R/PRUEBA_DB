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
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context; 

       
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound("No se encontraron usuarios.");
            }
            return await _context.Users.ToListAsync();
        }

        
        [HttpGet("{userId}/DataSets")] 
        public async Task<ActionResult<UserDataSetResponseDto>> GetUserAssociatedDataSets(int userId)
        {
           
            var user = await _context.Users
                                     .Where(u => u.UserID == userId)
                                     .Select(u => new UserDataSetResponseDto
                                     {
                                         UserID = u.UserID,
                                         Username = u.Username,
                                         Email = u.Email
                                     })
                                     .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound($"Usuario con ID {userId} no encontrado.");
            }

           
            var dataSets = await _context.Procedures
                                         .Where(p => p.CreatedByUserID == userId || p.LastModifiedUserID == userId)
                                         .SelectMany(p => p.DataSets.Select(ds => new DataSetDto
                                         {
                                             DataSetID = ds.DataSetID,
                                             DataSetName = ds.DataSetName,
                                             Description = ds.Description,
                                             ProcedureID = ds.ProcedureID,
                                             ProcedureName = p.ProcedureName,
                                             CreatedDate = ds.CreatedDate
                                         }))
                                         .Distinct() 
                                         .ToListAsync();

            user.CreatedOrModifiedDataSets = dataSets;

            return Ok(user);
        }
    }
}
