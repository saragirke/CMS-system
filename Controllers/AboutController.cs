using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cmsSystem.Data;
using cmsSystem.Models;
using LazZiya.ImageResize; // Bilder
using System.Drawing; // Bilder

namespace cmsSystem.Controllers
{
    public class AboutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private string wwwRootPath;

        public AboutController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            wwwRootPath = _hostEnvironment.WebRootPath;
        }

        // GET: About
        public async Task<IActionResult> Index()
        {
              return _context.About != null ? 
                          View(await _context.About.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.About'  is null.");
        }

        // GET: About/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.About == null)
            {
                return NotFound();
            }

            var about = await _context.About
                .FirstOrDefaultAsync(m => m.Id == id);
            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        // GET: About/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: About/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AboutTitle,AboutText,AboutFile,AltText")] About about)
        {
            if (ModelState.IsValid)
            {

                    if (about.AboutFile != null) {

                    //Spara bilder till wwwroot
                    string fileName = Path.GetFileNameWithoutExtension(about.AboutFile.FileName);
                    string extension = Path.GetExtension(about.AboutFile.FileName);

                    //Plockar bort mellanslag i filnam + lägger till timestamp
                    about.AboutImage = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/imageupload", fileName);

                    //Lagra Fil
                    using (var fileStream = new FileStream(path, FileMode.Create)) 
                    {
                        await about.AboutFile.CopyToAsync(fileStream);
                    }

                    //Funktion för att ange bildens storlek
                    //createImageFile(fileName);

                }
                else {
                    about.AboutImage = null;
                }

                _context.Add(about);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(about);
        }

        // GET: About/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.About == null)
            {
                return NotFound();
            }

            var about = await _context.About.FindAsync(id);
            if (about == null)
            {
                return NotFound();
            }
            return View(about);
        }

        // POST: About/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AboutTitle,AboutText,AboutImage, AboutFile, AltText")] About about)
        {
            if (id != about.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (about.AboutFile != null) {

                    //Spara bilder till wwwroot
                    string fileName = Path.GetFileNameWithoutExtension(about.AboutFile.FileName);
                    string extension = Path.GetExtension(about.AboutFile.FileName);

                    //Plockar bort mellanslag i filnam + lägger till timestamp
                    about.AboutImage = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/imageupload", fileName);

                    //Lagra Fil
                    using (var fileStream = new FileStream(path, FileMode.Create)) 
                    {
                        await about.AboutFile.CopyToAsync(fileStream);
                    }

                    //Funktion för att ange bildens storlek
                    //createImageFile(fileName);

                }
                    else {
                    if(about.AboutImage != "") 
                    {
                       about.AboutImage = about.AboutImage;
                    }
                    else 
                    {
                        about.AboutImage =null;
                    }

                  }




                    _context.Update(about);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutExists(about.Id))
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
            return View(about);
        }

        // GET: About/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.About == null)
            {
                return NotFound();
            }

            var about = await _context.About
                .FirstOrDefaultAsync(m => m.Id == id);
            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        // POST: About/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.About == null)
            {
                return Problem("Entity set 'ApplicationDbContext.About'  is null.");
            }
            var about = await _context.About.FindAsync(id);
            if (about != null)
            {
                _context.About.Remove(about);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutExists(int id)
        {
          return (_context.About?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
