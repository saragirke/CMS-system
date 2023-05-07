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
    public class SocialsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SocialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Socials
        public async Task<IActionResult> Index()
        {
              return _context.Socials != null ? 
                          View(await _context.Socials.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Socials'  is null.");
        }

        // GET: Socials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Socials == null)
            {
                return NotFound();
            }

            var socials = await _context.Socials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socials == null)
            {
                return NotFound();
            }

            return View(socials);
        }

        // GET: Socials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Socials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Facebook,Linkedin,Instagram")] Socials socials)
        {
            if (ModelState.IsValid)
            {
                _context.Add(socials);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(socials);
        }

        // GET: Socials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Socials == null)
            {
                return NotFound();
            }

            var socials = await _context.Socials.FindAsync(id);
            if (socials == null)
            {
                return NotFound();
            }
            return View(socials);
        }

        // POST: Socials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Facebook,Linkedin,Instagram")] Socials socials)
        {
            if (id != socials.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(socials);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocialsExists(socials.Id))
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
            return View(socials);
        }

        // GET: Socials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Socials == null)
            {
                return NotFound();
            }

            var socials = await _context.Socials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socials == null)
            {
                return NotFound();
            }

            return View(socials);
        }

        // POST: Socials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Socials == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Socials'  is null.");
            }
            var socials = await _context.Socials.FindAsync(id);
            if (socials != null)
            {
                _context.Socials.Remove(socials);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SocialsExists(int id)
        {
          return (_context.Socials?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
