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

        // 🔹 GET ALL
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        // 🔹 GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // 🔹 CREATE
        [HttpPost]
        public async Task<IActionResult> Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        // 🔹 UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            var data = await _context.Users.FindAsync(id);
            if (data == null) return NotFound();

            data.Username = user.Username;
            data.PasswordHash = user.PasswordHash;
            data.Role = user.Role;

            await _context.SaveChangesAsync();
            return Ok(data);
        }

        // 🔹 DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.Users.FindAsync(id);
            if (data == null) return NotFound();

            _context.Users.Remove(data);
            await _context.SaveChangesAsync();
            return Ok("Deleted");
        }
    }
}