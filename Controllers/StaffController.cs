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
    public class StaffController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _hostEnvironment;
        private string wwwRootPath;

        private int ImageWidth= 300;
        private int ImageHeigth=300;

        public StaffController(ApplicationDbContext context , IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            wwwRootPath = _hostEnvironment.WebRootPath;
        }

        // GET: Staff
        public async Task<IActionResult> Index()
        {
              return _context.Staff != null ? 
                          View(await _context.Staff.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Staff'  is null.");
        }

        // GET: Staff/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Staff == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Staff/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Staff/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Title,Email,Number,ImageFile,AltText")] Staff staff)
        {
            if (ModelState.IsValid)
            {


                    if (staff.ImageFile != null) {

                    //Spara bilder till wwwroot
                    string fileName = Path.GetFileNameWithoutExtension(staff.ImageFile.FileName);
                    string extension = Path.GetExtension(staff.ImageFile.FileName);

                    //Plockar bort mellanslag i filnam + lägger till timestamp
                   staff.ImageName = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/imageupload", fileName);

                    //Lagra Fil
                    using (var fileStream = new FileStream(path, FileMode.Create)) 
                    {
                        await staff.ImageFile.CopyToAsync(fileStream);
                    }

                    //Funktion för att ange bildens storlek
                    createImageFile(fileName);

                }
                else {
                    staff.ImageName = null;
                }

                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        // GET: Staff/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Staff == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST: Staff/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Title,Email,Number,ImageName, ImageFile, AltText")] Staff staff)
        {
            if (id != staff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

            if (staff.ImageFile != null) {

                     //Spara bilder till wwwroot
                    string fileName = Path.GetFileNameWithoutExtension(staff.ImageFile.FileName);
                    string extension = Path.GetExtension(staff.ImageFile.FileName);

                    //Plockar bort mellanslag i filnam + lägger till timestamp
                    staff.ImageName = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/imageupload", fileName);

                     //Lagra Fil
                    using (var fileStream = new FileStream(path, FileMode.Create)) 
                    {
                        await staff.ImageFile.CopyToAsync(fileStream);
                    }

                    createImageFile(fileName);

                 }


                  else {
                    if(staff.ImageName != "") 
                    {
                       staff.ImageName = staff.ImageName;
                    }
                    else 
                    {
                        staff.ImageName =null;
                    }

                  }


                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.Id))
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
            return View(staff);
        }

        // GET: Staff/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Staff == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Staff == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Staff'  is null.");
            }
            var staff = await _context.Staff.FindAsync(id);
            if (staff != null)
            {
                _context.Staff.Remove(staff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(int id)
        {
          return (_context.Staff?.Any(e => e.Id == id)).GetValueOrDefault();
        }

/*
        //Funktion för bilder
         
         private void createImageFile(string fileName) {

            using(var img = Image.FromFile(Path.Combine(wwwRootPath + "/imageupload/" + fileName))) {
              
               img.Scale(ImageWidth, ImageHeigth).SaveAs(Path.Combine(wwwRootPath + "/imageupload" + fileName )); 
            } 


         } */


         private void createImageFile(string fileName)
{
    var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
    var uniqueFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{timestamp}{Path.GetExtension(fileName)}";

    using (var img = System.Drawing.Image.FromFile(Path.Combine(wwwRootPath + "/imageupload/", fileName)))
    {
        var resizedImage = new System.Drawing.Bitmap(ImageWidth, ImageHeigth);
        using (var graphics = System.Drawing.Graphics.FromImage(resizedImage))
        {
            graphics.DrawImage(img, 0, 0, ImageWidth, ImageHeigth);
        }

        var filePath = Path.Combine(wwwRootPath, "imageupload", uniqueFileName);
        using (var fs = new FileStream(filePath, FileMode.Create))
        {
            resizedImage.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}


    }


    }

