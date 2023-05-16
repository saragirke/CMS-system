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
    public class ApiAboutController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiAboutController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiAbout
        [HttpGet]
        public async Task<ActionResult<IEnumerable<About>>> GetAbout()
        {
          if (_context.About == null)
          {
              return NotFound();
          }
            return await _context.About.ToListAsync();
        }

        // GET: api/ApiAbout/5
        [HttpGet("{id}")]
        public async Task<ActionResult<About>> GetAbout(int id)
        {
          if (_context.About == null)
          {
              return NotFound();
          }
            var about = await _context.About.FindAsync(id);

            if (about == null)
            {
                return NotFound();
            }

            return about;
        }

        /*
        // PUT: api/ApiAbout/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAbout(int id, About about)
        {
            if (id != about.Id)
            {
                return BadRequest();
            }

            _context.Entry(about).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AboutExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        } */
        /*
        // POST: api/ApiAbout
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<About>> PostAbout(About about)
        {
          if (_context.About == null)
          {
              return Problem("Entity set 'ApplicationDbContext.About'  is null.");
          }
            _context.About.Add(about);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAbout", new { id = about.Id }, about);
        }
*/
/*
        // DELETE: api/ApiAbout/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbout(int id)
        {
            if (_context.About == null)
            {
                return NotFound();
            }
            var about = await _context.About.FindAsync(id);
            if (about == null)
            {
                return NotFound();
            }

            _context.About.Remove(about);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AboutExists(int id)
        {
            return (_context.About?.Any(e => e.Id == id)).GetValueOrDefault();
        } 
*/

    }
}
