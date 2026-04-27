using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MassageAPI.Data;
using MassageAPI.Models;

namespace MassageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Services.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return Ok(service);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
             var data = await _context.Services.FindAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Service service)
        {
              var data = await _context.Services.FindAsync(id);
             if (data == null) return NotFound();

                 data.ServiceName = service.ServiceName;
                 data.Description = service.Description;
                 data.Price = service.Price;
                 data.Duration = service.Duration;

            await _context.SaveChangesAsync();
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.Services.FindAsync(id);
            if (data == null) return NotFound();

             _context.Services.Remove(data);
            await _context.SaveChangesAsync();
             return Ok("Deleted");
        }
    }
}