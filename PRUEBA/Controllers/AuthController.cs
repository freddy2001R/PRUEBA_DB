
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PRUEBA.Models;
using PRUEBA.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net; 
using System.ComponentModel.DataAnnotations; 

namespace PRUEBA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // 1. Buscar el usuario por nombre de usuario
            var user = await _context.Users
                                     .Include(u => u.UserRoles)!
                                     .ThenInclude(ur => ur.Role)
                                     .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
            {
                return Unauthorized("Nombre de usuario o contraseña incorrectos.");
            }

            // 2. Verificar la contraseña hasheada
            // LA CORRECCIÓN ESTÁ AQUÍ: Se añade ".BCrypt" para la sintaxis de BCrypt.Net-Next
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return Unauthorized("Nombre de usuario o contraseña incorrectos.");
            }

            // 3. Crear Claims para el token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            // Añadir los roles del usuario como Claims
            if (user.UserRoles != null)
            {
                foreach (var userRole in user.UserRoles)
                {
                    if (userRole.Role != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, userRole.Role.RoleName));
                    }
                }
            }

            // 4. Generar el Token JWT
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Secret"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            if (string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            {
                // Esto debería ser manejado como una excepción al inicio de la app, como en Program.cs
                return StatusCode(500, "Configuración JWT incompleta.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Token válido por 1 hora
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { Token = jwt });
        }
    }

    // Clase para el cuerpo de la solicitud de login
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}