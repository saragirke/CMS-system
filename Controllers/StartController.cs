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
    public class StartController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _hostEnvironment;
        private string wwwRootPath;

        public StartController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            wwwRootPath = _hostEnvironment.WebRootPath;
        }

        // GET: Start
        public async Task<IActionResult> Index()
        {
              return _context.Start != null ? 
                          View(await _context.Start.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Start'  is null.");
        }

        // GET: Start/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Start == null)
            {
                return NotFound();
            }

            var start = await _context.Start
                .FirstOrDefaultAsync(m => m.Id == id);
            if (start == null)
            {
                return NotFound();
            }

            return View(start);
        }

        // GET: Start/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Start/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ImageFile,AltText")] Start start)
        {
            if (ModelState.IsValid)
            {

                    if (start.ImageFile != null) {

                    //Spara bilder till wwwroot
                    string fileName = Path.GetFileNameWithoutExtension(start.ImageFile.FileName);
                    string extension = Path.GetExtension(start.ImageFile.FileName);

                    //Plockar bort mellanslag i filnam + lägger till timestamp
                    start.ImageName = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/imageupload", fileName);

                    //Lagra Fil
                    using (var fileStream = new FileStream(path, FileMode.Create)) 
                    {
                        await start.ImageFile.CopyToAsync(fileStream);
                    }

                    //Funktion för att ange bildens storlek
                   // createImageFile(fileName);

                }
                else {
                    start.ImageName = null;
                }


                _context.Add(start);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(start);
        }

        // GET: Start/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Start == null)
            {
                return NotFound();
            }

            var start = await _context.Start.FindAsync(id);
            if (start == null)
            {
                return NotFound();
            }
            return View(start);
        }

        // POST: Start/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImageName, ImageFile, AltText")] Start start)
        {
            if (id != start.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (start.ImageFile != null) {

                     //Spara bilder till wwwroot
                    string fileName = Path.GetFileNameWithoutExtension(start.ImageFile.FileName);
                    string extension = Path.GetExtension(start.ImageFile.FileName);

                    //Plockar bort mellanslag i filnam + lägger till timestamp
                    start.ImageName = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/imageupload", fileName);

                     //Lagra Fil
                    using (var fileStream = new FileStream(path, FileMode.Create)) 
                    {
                        await start.ImageFile.CopyToAsync(fileStream);
                    }

                    //createImageFile(fileName);

                 }


                  else {
                    if(start.ImageName != "") 
                    {
                       start.ImageName = start.ImageName;
                    }
                    else 
                    {
                       start.ImageName =null;
                    }

                  }


                    _context.Update(start);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StartExists(start.Id))
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
            return View(start);
        }

        // GET: Start/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Start == null)
            {
                return NotFound();
            }

            var start = await _context.Start
                .FirstOrDefaultAsync(m => m.Id == id);
            if (start == null)
            {
                return NotFound();
            }

            return View(start);
        }

        // POST: Start/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Start == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Start'  is null.");
            }
            var start = await _context.Start.FindAsync(id);
            if (start != null)
            {
                _context.Start.Remove(start);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StartExists(int id)
        {
          return (_context.Start?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
