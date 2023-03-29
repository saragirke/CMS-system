using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cmsSystem.Data;
using cmsSystem.Models;

namespace cmsSystem.Controllers
{
    public class HeaderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HeaderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Header
        public async Task<IActionResult> Index()
        {
              return _context.Header != null ? 
                          View(await _context.Header.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Header'  is null.");
        }

        // GET: Header/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Header == null)
            {
                return NotFound();
            }

            var header = await _context.Header
                .FirstOrDefaultAsync(m => m.Id == id);
            if (header == null)
            {
                return NotFound();
            }

            return View(header);
        }

        // GET: Header/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Header/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Font,HeaderName,LogoName")] Header header)
        {
            if (ModelState.IsValid)
            {
                _context.Add(header);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(header);
        }

        // GET: Header/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Header == null)
            {
                return NotFound();
            }

            var header = await _context.Header.FindAsync(id);
            if (header == null)
            {
                return NotFound();
            }
            return View(header);
        }

        // POST: Header/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Font,HeaderName,LogoName")] Header header)
        {
            if (id != header.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(header);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeaderExists(header.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(header);
        }

        // GET: Header/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Header == null)
            {
                return NotFound();
            }

            var header = await _context.Header
                .FirstOrDefaultAsync(m => m.Id == id);
            if (header == null)
            {
                return NotFound();
            }

            return View(header);
        }

        // POST: Header/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Header == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Header'  is null.");
            }
            var header = await _context.Header.FindAsync(id);
            if (header != null)
            {
                _context.Header.Remove(header);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeaderExists(int id)
        {
          return (_context.Header?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
