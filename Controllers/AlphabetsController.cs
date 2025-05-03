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
    public class AlphabetsController : ControllerBase
    {
        private readonly EduApplicationContext _context;

        public AlphabetsController(EduApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Alphabets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alphabet>>> GetAlphabets()
        {
            return await _context.Alphabets.ToListAsync();
        }

        // GET: api/Alphabets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alphabet>> GetAlphabet(int id)
        {
            var alphabet = await _context.Alphabets.FindAsync(id);

            if (alphabet == null)
            {
                return NotFound();
            }

            return alphabet;
        }

        //// PUT: api/Alphabets/5
        //   [HttpPut("{id}")]
        //public async Task<IActionResult> PutAlphabet(int id, Alphabet alphabet)
        //{
        //    if (id != alphabet.letter_id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(alphabet).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AlphabetExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Alphabets
        // [HttpPost]
        //public async Task<ActionResult<Alphabet>> PostAlphabet(Alphabet alphabet)
        //{
        //    _context.Alphabets.Add(alphabet);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetAlphabet", new { id = alphabet.letter_id }, alphabet);
        //}

        // DELETE: api/Alphabets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlphabet(int id)
        {
            var alphabet = await _context.Alphabets.FindAsync(id);
            if (alphabet == null)
            {
                return NotFound();
            }

            _context.Alphabets.Remove(alphabet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlphabetExists(int id)
        {
            return _context.Alphabets.Any(e => e.letter_id == id);
        }
    }
}
