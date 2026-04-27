using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MassageAPI.Data;
using MassageAPI.Models;
using MassageAPI.DTO;

namespace MassageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 GET ALL BOOKINGS
        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _context.Bookings.ToListAsync();
            return Ok(bookings);
        }

        // 🔹 GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        // 🔹 CREATE BOOKING
        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingDTO dto)
        {
            var booking = new Booking
            {
                CustomerName = dto.CustomerName,
                Service = dto.Service,
                Date = dto.Date
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(booking);
        }

        // 🔹 UPDATE BOOKING
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, BookingDTO dto)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound();

            booking.CustomerName = dto.CustomerName;
            booking.Service = dto.Service;
            booking.Date = dto.Date;

            await _context.SaveChangesAsync();

            return Ok(booking);
        }

        // 🔹 DELETE BOOKING
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound();

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return Ok("Deleted successfully");
        }
    }
}