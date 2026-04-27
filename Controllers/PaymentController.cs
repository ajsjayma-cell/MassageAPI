using Microsoft.AspNetCore.Mvc;
using MassageAPI.Data;
using MassageAPI.Models;

namespace MassageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PaymentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Payments.ToList());
        }

        [HttpPost]
        public IActionResult Add(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
            return Ok(payment);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
             var data = _context.Payments.Find(id);
             if (data == null) return NotFound();
                return Ok(data);
        }

            [HttpPut("{id}")]
        public IActionResult Update(int id, Payment payment)
        {
            var data = _context.Payments.Find(id);
            if (data == null) return NotFound();

                data.Amount = payment.Amount;
                data.PaymentDate = payment.PaymentDate;
                data.PaymentMethod = payment.PaymentMethod;

            _context.SaveChanges();
             return Ok(data);
        }

            [HttpDelete("{id}")]
         public IActionResult Delete(int id)
        {
                var data = _context.Payments.Find(id);
                if (data == null) return NotFound();

             _context.Payments.Remove(data);
             _context.SaveChanges();
             return Ok("Deleted");
        }
    }
}