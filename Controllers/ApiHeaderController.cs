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
    public class ApiHeaderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiHeaderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiHeader
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Header>>> GetHeader()
        {
          if (_context.Header == null)
          {
              return NotFound();
          }
            return await _context.Header.ToListAsync();
        }

        // GET: api/ApiHeader/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Header>> GetHeader(int id)
        {
          if (_context.Header == null)
          {
              return NotFound();
          }
            var header = await _context.Header.FindAsync(id);

            if (header == null)
            {
                return NotFound();
            }

            return header;
        }

        /*

        // PUT: api/ApiHeader/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHeader(int id, Header header)
        {
            if (id != header.Id)
            {
                return BadRequest();
            }

            _context.Entry(header).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeaderExists(id))
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

        // POST: api/ApiHeader
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Header>> PostHeader(Header header)
        {
          if (_context.Header == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Header'  is null.");
          }
            _context.Header.Add(header);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHeader", new { id = header.Id }, header);
        } */

        /*

        // DELETE: api/ApiHeader/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHeader(int id)
        {
            if (_context.Header == null)
            {
                return NotFound();
            }
            var header = await _context.Header.FindAsync(id);
            if (header == null)
            {
                return NotFound();
            }

            _context.Header.Remove(header);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HeaderExists(int id)
        {
            return (_context.Header?.Any(e => e.Id == id)).GetValueOrDefault();
        } */
    }
}
