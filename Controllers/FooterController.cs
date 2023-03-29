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
    public class FooterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FooterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Footer
        public async Task<IActionResult> Index()
        {
              return _context.Footer != null ? 
                          View(await _context.Footer.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Footer'  is null.");
        }

        // GET: Footer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Footer == null)
            {
                return NotFound();
            }

            var footer = await _context.Footer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (footer == null)
            {
                return NotFound();
            }

            return View(footer);
        }

        // GET: Footer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Footer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FooterColor,FontColor,FooterAdress,FooterPhone,FooterEmail")] Footer footer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(footer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(footer);
        }

        // GET: Footer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Footer == null)
            {
                return NotFound();
            }

            var footer = await _context.Footer.FindAsync(id);
            if (footer == null)
            {
                return NotFound();
            }
            return View(footer);
        }

        // POST: Footer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FooterColor,FontColor,FooterAdress,FooterPhone,FooterEmail")] Footer footer)
        {
            if (id != footer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(footer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FooterExists(footer.Id))
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
            return View(footer);
        }

        // GET: Footer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Footer == null)
            {
                return NotFound();
            }

            var footer = await _context.Footer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (footer == null)
            {
                return NotFound();
            }

            return View(footer);
        }

        // POST: Footer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Footer == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Footer'  is null.");
            }
            var footer = await _context.Footer.FindAsync(id);
            if (footer != null)
            {
                _context.Footer.Remove(footer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FooterExists(int id)
        {
          return (_context.Footer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
