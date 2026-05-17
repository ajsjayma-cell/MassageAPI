using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MassageAPI.Data;
using MassageAPI.Models;

namespace MassageAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReservationController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.Reservations.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);

        if (reservation == null)
            return NotFound();

        return Ok(reservation);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Reservation reservation)
    {
        _context.Reservations.Add(reservation);

        await _context.SaveChangesAsync();

        return Ok(reservation);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Reservation reservation)
    {
        var existing = await _context.Reservations.FindAsync(id);

        if (existing == null)
            return NotFound();

        existing.UserId = reservation.UserId;
        existing.ServiceId = reservation.ServiceId;
        existing.TherapistId = reservation.TherapistId;
        existing.ReservationDate = reservation.ReservationDate;
        existing.ReservationTime = reservation.ReservationTime;
        existing.Status = reservation.Status;

        await _context.SaveChangesAsync();

        return Ok(existing);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);

        if (reservation == null)
            return NotFound();

        _context.Reservations.Remove(reservation);

        await _context.SaveChangesAsync();

        return Ok();
    }
}