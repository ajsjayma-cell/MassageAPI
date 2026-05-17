using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MassageAPI.Data;
using MassageAPI.DTO;
using MassageAPI.Models;
using BCrypt.Net;


namespace MassageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // 🔐 LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (dto == null || 
                string.IsNullOrWhiteSpace(dto.Email) || 
                string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest("Full name and password required");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
                return Unauthorized("User not found");

            // check hashed password
            if (string.IsNullOrEmpty(user.PasswordHash))
                return Unauthorized("Invalid stored password");

            bool valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!valid)
                return Unauthorized("Wrong password");

            return Ok(new
            {
                message = "Login successful",
                user = user.FullName
            });
        }

        // 📝 REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            if (dto == null || 
                string.IsNullOrWhiteSpace(dto.FullName) || 
                string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest("Full name and password required");
            }

            var exists = await _context.Users
                .AnyAsync(u => u.FullName == dto.FullName);

            if (exists)
                return BadRequest("Full name already exists");

            var user = new User
            {
                FullName = dto.FullName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Registered successfully",
                user = user.FullName
            });
        }
    }
}