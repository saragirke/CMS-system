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
    public class NewssController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewssController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Newss
        public async Task<IActionResult> Index()
        {
              return _context.Newss != null ? 
                          View(await _context.Newss.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Newss'  is null.");
        }

        // GET: Newss/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Newss == null)
            {
                return NotFound();
            }

            var newss = await _context.Newss
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newss == null)
            {
                return NotFound();
            }

            return View(newss);
        }

        // GET: Newss/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Newss/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Post,ImageName,AltText,DateCreated")] Newss newss)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newss);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newss);
        }

        // GET: Newss/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Newss == null)
            {
                return NotFound();
            }

            var newss = await _context.Newss.FindAsync(id);
            if (newss == null)
            {
                return NotFound();
            }
            return View(newss);
        }

        // POST: Newss/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Post,ImageName,AltText,DateCreated")] Newss newss)
        {
            if (id != newss.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newss);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewssExists(newss.Id))
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
            return View(newss);
        }

        // GET: Newss/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Newss == null)
            {
                return NotFound();
            }

            var newss = await _context.Newss
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newss == null)
            {
                return NotFound();
            }

            return View(newss);
        }

        // POST: Newss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Newss == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Newss'  is null.");
            }
            var newss = await _context.Newss.FindAsync(id);
            if (newss != null)
            {
                _context.Newss.Remove(newss);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewssExists(int id)
        {
          return (_context.Newss?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
