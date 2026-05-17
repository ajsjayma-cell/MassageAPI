using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MassageAPI.Data;
using MassageAPI.Models;

namespace MassageAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TherapistController : ControllerBase
{
    private readonly AppDbContext _context;

    public TherapistController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.Therapists.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var therapist = await _context.Therapists.FindAsync(id);

        if (therapist == null)
            return NotFound();

        return Ok(therapist);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Therapist therapist)
    {
        _context.Therapists.Add(therapist);
        await _context.SaveChangesAsync();

        return Ok(therapist);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Therapist therapist)
    {
        var existing = await _context.Therapists.FindAsync(id);

        if (existing == null)
            return NotFound();

        existing.FullName = therapist.FullName;
        existing.Specialization = therapist.Specialization;
        existing.AvailabilityStatus = therapist.AvailabilityStatus;

        await _context.SaveChangesAsync();

        return Ok(existing);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var therapist = await _context.Therapists.FindAsync(id);

        if (therapist == null)
            return NotFound();

        _context.Therapists.Remove(therapist);

        await _context.SaveChangesAsync();

        return Ok();
    }
}