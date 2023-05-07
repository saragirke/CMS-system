using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cmsSystem.Data;
using cmsSystem.Models;

namespace cmsSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiSocialsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiSocialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiSocials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Socials>>> GetSocials()
        {
          if (_context.Socials == null)
          {
              return NotFound();
          }
            return await _context.Socials.ToListAsync();
        }

        // GET: api/ApiSocials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Socials>> GetSocials(int id)
        {
          if (_context.Socials == null)
          {
              return NotFound();
          }
            var socials = await _context.Socials.FindAsync(id);

            if (socials == null)
            {
                return NotFound();
            }

            return socials;
        }

        // PUT: api/ApiSocials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSocials(int id, Socials socials)
        {
            if (id != socials.Id)
            {
                return BadRequest();
            }

            _context.Entry(socials).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocialsExists(id))
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

        // POST: api/ApiSocials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Socials>> PostSocials(Socials socials)
        {
          if (_context.Socials == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Socials'  is null.");
          }
            _context.Socials.Add(socials);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSocials", new { id = socials.Id }, socials);
        }

        // DELETE: api/ApiSocials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocials(int id)
        {
            if (_context.Socials == null)
            {
                return NotFound();
            }
            var socials = await _context.Socials.FindAsync(id);
            if (socials == null)
            {
                return NotFound();
            }

            _context.Socials.Remove(socials);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SocialsExists(int id)
        {
            return (_context.Socials?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
