using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduApplication.Models;

namespace EduApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumbersController : ControllerBase
    {
        private readonly EduApplicationContext _context;

        public NumbersController(EduApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Numbers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Number>>> GetNumbers()
        {
            return await _context.Numbers.ToListAsync();
        }

        // GET: api/Numbers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Number>> GetNumber(int id)
        {
            var number = await _context.Numbers.FindAsync(id);

            if (number == null)
            {
                return NotFound();
            }

            return number;
        }

        // PUT: api/Numbers/5
         [HttpPut("{id}")]
        public async Task<IActionResult> PutNumber(int id, Number number)
        {
            if (id != number.number_id)
            {
                return BadRequest();
            }

            _context.Entry(number).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NumberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Numbers
          [HttpPost]
        public async Task<ActionResult<Number>> PostNumber(Number number)
        {
            _context.Numbers.Add(number);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNumber", new { id = number.number_id }, number);
        }

        // DELETE: api/Numbers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNumber(int id)
        {
            var number = await _context.Numbers.FindAsync(id);
            if (number == null)
            {
                return NotFound();
            }

            _context.Numbers.Remove(number);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NumberExists(int id)
        {
            return _context.Numbers.Any(e => e.number_id == id);
        }
    }
}
