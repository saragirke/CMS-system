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
    public class ApiStartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiStartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiStart
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Start>>> GetStart()
        {
          if (_context.Start == null)
          {
              return NotFound();
          }
            return await _context.Start.ToListAsync();
        }

        // GET: api/ApiStart/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Start>> GetStart(int id)
        {
          if (_context.Start == null)
          {
              return NotFound();
          }
            var start = await _context.Start.FindAsync(id);

            if (start == null)
            {
                return NotFound();
            }

            return start;
        }

        // PUT: api/ApiStart/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStart(int id, Start start)
        {
            if (id != start.Id)
            {
                return BadRequest();
            }

            _context.Entry(start).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StartExists(id))
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

        // POST: api/ApiStart
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Start>> PostStart(Start start)
        {
          if (_context.Start == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Start'  is null.");
          }
            _context.Start.Add(start);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStart", new { id = start.Id }, start);
        }

        // DELETE: api/ApiStart/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStart(int id)
        {
            if (_context.Start == null)
            {
                return NotFound();
            }
            var start = await _context.Start.FindAsync(id);
            if (start == null)
            {
                return NotFound();
            }

            _context.Start.Remove(start);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StartExists(int id)
        {
            return (_context.Start?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
