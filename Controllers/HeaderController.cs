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
    public class HeaderController : Controller
    {
        private readonly ApplicationDbContext _context;

         private readonly IWebHostEnvironment _hostEnvironment;

           private string wwwRootPath;

        //Bilder
        private int HeaderWidth= 1920;
        private int HeaderHeigth=200;

                //Bilder
        private int LogoWidth= 250;
        private int LogoHeigth=100;

        public HeaderController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
               _hostEnvironment = hostEnvironment;
            wwwRootPath = _hostEnvironment.WebRootPath;
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
        public async Task<IActionResult> Create([Bind("Id,Title,Font, FontColor, NavColor, SubTitle, ImageFile,LogoFile")] Header header)
        {
            if (ModelState.IsValid)
            {

                  if (header.ImageFile != null) {

                    //Spara bilder till wwwroot
                    string fileName = Path.GetFileNameWithoutExtension(header.ImageFile.FileName);
                    string extension = Path.GetExtension(header.ImageFile.FileName);



                    //Plockar bort mellanslag i filnam + lägger till timestamp
                    header.ImageName = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/imageupload", fileName);



                    //Lagra Fil
                    using (var fileStream = new FileStream(path, FileMode.Create)) 
                    {
                        await header.ImageFile.CopyToAsync(fileStream);
                    }


                    createHeaderFile(fileName);
                    //Funktion för att ange bildens storlek
                    //createLogoFile(fileName2);
                   

                }
                  if (header.LogoFile != null) {
 
                    //Spara bilder till wwwroot
                    string fileName = Path.GetFileNameWithoutExtension(header.LogoFile.FileName);
                    string extension = Path.GetExtension(header.LogoFile.FileName);

                    
                    //Plockar bort mellanslag i filnam + lägger till timestamp
                    header.LogoName = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/imageupload", fileName);

                                       //Lagra Fil
                    using (var fileStream = new FileStream(path, FileMode.Create)) 
                    {
        
                        await header.LogoFile.CopyToAsync(fileStream);
                    }

                    createLogoFile(fileName);

                  }
                else {
                    header.ImageName = null;
                }



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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Font, FontColor, SubTitle, NavColor, ImageName,  ImageFile, LogoName, LogoFile")] Header header)
        {
            if (id != header.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

        try {
            //HEADER
             if (header.ImageFile != null) {

                     //Spara bilder till wwwroot
                    string fileName = Path.GetFileNameWithoutExtension(header.ImageFile.FileName);
                    string extension = Path.GetExtension(header.ImageFile.FileName);

                    //Plockar bort mellanslag i filnam + lägger till timestamp
                    header.ImageName = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/imageupload", fileName);

                     //Lagra Fil
                    using (var fileStream = new FileStream(path, FileMode.Create)) 
                    {
                        await header.ImageFile.CopyToAsync(fileStream);
                    }

                    createHeaderFile(fileName);

                 }

                    //LOGGA
                    if (header.LogoFile != null) {
 
                    //Spara bilder till wwwroot
                    string fileName = Path.GetFileNameWithoutExtension(header.LogoFile.FileName);
                    string extension = Path.GetExtension(header.LogoFile.FileName);

                    
                    //Plockar bort mellanslag i filnam + lägger till timestamp
                    header.LogoName = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/imageupload", fileName);

                    //Lagra Fil
                    using (var fileStream = new FileStream(path, FileMode.Create)) 
                    {
        
                        await header.LogoFile.CopyToAsync(fileStream);
                    }

                    createLogoFile(fileName);

                  } else {
                    if(header.ImageName != "") 
                    {
                        header.ImageName = header.ImageName;
                    }
                    else 
                    {
                        header.ImageName =null;
                    }
                    if(header.LogoName != "") 
                    {
                       header.LogoName = header.LogoName;
                    }
                    else 
                    {
                        header.LogoName =null;
                    }
                  }

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


                //Funktion för header
         
         private void createHeaderFile(string fileName) {

            using(var img = Image.FromFile(Path.Combine(wwwRootPath + "/imageupload/" , fileName))) {
              
               img.Scale(HeaderWidth, HeaderHeigth).SaveAs(Path.Combine(wwwRootPath + "/imageupload" + fileName)); 
            } 


         }


         
                //Funktion för Logga
         
         private void createLogoFile(string fileName2) {

            using(var img = Image.FromFile(Path.Combine(wwwRootPath + "/imageupload/" , fileName2))) {
              
               img.Scale(LogoWidth, LogoHeigth).SaveAs(Path.Combine(wwwRootPath + "/imageupload" + fileName2)); 
            } 


         } 
    }
}
