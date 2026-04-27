using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MassageAPI.Data;
using MassageAPI.Models;

namespace MassageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Customers.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return Ok(customer);
        }
        [HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)
{
    var data = await _context.Customers.FindAsync(id);
    if (data == null) return NotFound();
    return Ok(data);
}

[HttpPut("{id}")]
public async Task<IActionResult> Update(int id, Customer customer)
{
    var data = await _context.Customers.FindAsync(id);
    if (data == null) return NotFound();

    data.FullName = customer.FullName;
    data.ContactNumber = customer.ContactNumber;
    data.Email = customer.Email;

    await _context.SaveChangesAsync();
    return Ok(data);
}

[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
    var data = await _context.Customers.FindAsync(id);
    if (data == null) return NotFound();

    _context.Customers.Remove(data);
    await _context.SaveChangesAsync();
    return Ok("Deleted");
}
    }
}