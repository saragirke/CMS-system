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
    public class ApiWidgetController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiWidgetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiWidget
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Widget>>> GetWidget()
        {
          if (_context.Widget == null)
          {
              return NotFound();
          }
            return await _context.Widget.ToListAsync();
        }

        // GET: api/ApiWidget/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Widget>> GetWidget(int id)
        {
          if (_context.Widget == null)
          {
              return NotFound();
          }
            var widget = await _context.Widget.FindAsync(id);

            if (widget == null)
            {
                return NotFound();
            }

            return widget;
        }


/*
        // PUT: api/ApiWidget/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWidget(int id, Widget widget)
        {
            if (id != widget.Id)
            {
                return BadRequest();
            }

            _context.Entry(widget).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WidgetExists(id))
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

        // POST: api/ApiWidget
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Widget>> PostWidget(Widget widget)
        {
          if (_context.Widget == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Widget'  is null.");
          }
            _context.Widget.Add(widget);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWidget", new { id = widget.Id }, widget);
        } */

        /*

        // DELETE: api/ApiWidget/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWidget(int id)
        {
            if (_context.Widget == null)
            {
                return NotFound();
            }
            var widget = await _context.Widget.FindAsync(id);
            if (widget == null)
            {
                return NotFound();
            }

            _context.Widget.Remove(widget);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WidgetExists(int id)
        {
            return (_context.Widget?.Any(e => e.Id == id)).GetValueOrDefault();
        } */
    }
}
