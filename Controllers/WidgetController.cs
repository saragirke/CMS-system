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
    public class WidgetController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _hostEnvironment;
        private string wwwRootPath;


        public WidgetController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
         _hostEnvironment = hostEnvironment;
        wwwRootPath = _hostEnvironment.WebRootPath;
        _context = context;
        }

        // GET: Widget
        public async Task<IActionResult> Index()
        {
              return _context.Widget != null ? 
                          View(await _context.Widget.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Widget'  is null.");
        }

        // GET: Widget/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Widget == null)
            {
                return NotFound();
            }

            var widget = await _context.Widget
                .FirstOrDefaultAsync(m => m.Id == id);
            if (widget == null)
            {
                return NotFound();
            }

            return View(widget);
        }

        // GET: Widget/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Widget/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WidgetTitle,WidgetText,WidgetColor,Color,ImageFile,AltText,Display")] Widget widget)
        {
            if (ModelState.IsValid)
            {

                    if (widget.ImageFile != null) {

                    //Spara bilder till wwwroot
                    string fileName = Path.GetFileNameWithoutExtension(widget.ImageFile.FileName);
                    string extension = Path.GetExtension(widget.ImageFile.FileName);

                    //Plockar bort mellanslag i filnam + lägger till timestamp
                    widget.ImageName = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/imageupload", fileName);

                    //Lagra Fil
                    using (var fileStream = new FileStream(path, FileMode.Create)) 
                    {
                        await widget.ImageFile.CopyToAsync(fileStream);
                    }

                    //Funktion för att ange bildens storlek
                   // createImageFile(fileName);

                }
                else {
                    widget.ImageName = null;
                }

                
                _context.Add(widget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(widget);
        }

        // GET: Widget/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Widget == null)
            {
                return NotFound();
            }

            var widget = await _context.Widget.FindAsync(id);
            if (widget == null)
            {
                return NotFound();
            }
            return View(widget);
        }

        // POST: Widget/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WidgetTitle,WidgetText,WidgetColor,Color,ImageName, ImageFile, AltText,Display")] Widget widget)
        {
            if (id != widget.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (widget.ImageFile != null) {

                    //Spara bilder till wwwroot
                    string fileName = Path.GetFileNameWithoutExtension(widget.ImageFile.FileName);
                    string extension = Path.GetExtension(widget.ImageFile.FileName);

                    //Plockar bort mellanslag i filnam + lägger till timestamp
                    widget.ImageName = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/imageupload", fileName);

                    //Lagra Fil
                    using (var fileStream = new FileStream(path, FileMode.Create)) 
                    {
                        await widget.ImageFile.CopyToAsync(fileStream);
                    }

                    //Funktion för att ange bildens storlek
                   // createImageFile(fileName);

                }

                  else {
                    if(widget.ImageName != "") 
                    {
                       widget.ImageName = widget.ImageName;
                    }
                    else 
                    {
                        widget.ImageName =null;
                    }

                  }


                    _context.Update(widget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WidgetExists(widget.Id))
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
            return View(widget);
        }

        // GET: Widget/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Widget == null)
            {
                return NotFound();
            }

            var widget = await _context.Widget
                .FirstOrDefaultAsync(m => m.Id == id);
            if (widget == null)
            {
                return NotFound();
            }

            return View(widget);
        }

        // POST: Widget/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Widget == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Widget'  is null.");
            }
            var widget = await _context.Widget.FindAsync(id);
            if (widget != null)
            {
                _context.Widget.Remove(widget);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WidgetExists(int id)
        {
          return (_context.Widget?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
