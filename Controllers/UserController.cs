using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MassageAPI.Data;
using MassageAPI.Models;

namespace MassageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 GET ALL USERS
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _context.Users
                .Select(u => new
                {
                    u.Id,
                    u.FullName,
                    u.Email,
                    u.Role
                })
                .ToListAsync();

            return Ok(users);
        }

        // 🔹 GET USER BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _context.Users
                .Where(u => u.Id == id)
                .Select(u => new
                {
                    u.Id,
                    u.FullName,
                    u.Email,
                    u.Role
                })
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // 🔹 CREATE USER
        [HttpPost]
        public async Task<IActionResult> Add(User user)
        {
            if (string.IsNullOrWhiteSpace(user.FullName) ||
                string.IsNullOrWhiteSpace(user.Email) ||
                string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                return BadRequest("Full name, email, and password are required.");
            }

            // Check if email already exists
            var exists = await _context.Users
                .AnyAsync(x => x.Email == user.Email);

            if (exists)
            {
                return BadRequest("Email already exists.");
            }

            // Hash password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                user.Id,
                user.FullName,
                user.Email,
                user.Role
            });
        }

        // 🔹 UPDATE USER
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            var data = await _context.Users.FindAsync(id);

            if (data == null)
                return NotFound();

            data.FullName = user.FullName;
            data.Email = user.Email;
            data.Role = user.Role;

            // Update password if provided
            if (!string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                data.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                data.Id,
                data.FullName,
                data.Email,
                data.Role
            });
        }

        // 🔹 DELETE USER
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.Users.FindAsync(id);

            if (data == null)
                return NotFound();

            _context.Users.Remove(data);

            await _context.SaveChangesAsync();

            return Ok("Deleted successfully");
        }
    }
}