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
    public class ApiFooterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiFooterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiFooter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Footer>>> GetFooter()
        {
          if (_context.Footer == null)
          {
              return NotFound();
          }
            return await _context.Footer.ToListAsync();
        }

        // GET: api/ApiFooter/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Footer>> GetFooter(int id)
        {
          if (_context.Footer == null)
          {
              return NotFound();
          }
            var footer = await _context.Footer.FindAsync(id);

            if (footer == null)
            {
                return NotFound();
            }

            return footer;
        }
/*
        // PUT: api/ApiFooter/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFooter(int id, Footer footer)
        {
            if (id != footer.Id)
            {
                return BadRequest();
            }

            _context.Entry(footer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FooterExists(id))
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

        // POST: api/ApiFooter
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Footer>> PostFooter(Footer footer)
        {
          if (_context.Footer == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Footer'  is null.");
          }
            _context.Footer.Add(footer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFooter", new { id = footer.Id }, footer);
        }*/
        /*

        // DELETE: api/ApiFooter/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFooter(int id)
        {
            if (_context.Footer == null)
            {
                return NotFound();
            }
            var footer = await _context.Footer.FindAsync(id);
            if (footer == null)
            {
                return NotFound();
            }

            _context.Footer.Remove(footer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FooterExists(int id)
        {
            return (_context.Footer?.Any(e => e.Id == id)).GetValueOrDefault();
        } */
    }
}
