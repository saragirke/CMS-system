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
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private string wwwRootPath;

                //Bilder
        private int ImageWidth= 640;
        private int ImageHeigth=420;

        public NewsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
             _hostEnvironment = hostEnvironment;
            wwwRootPath = _hostEnvironment.WebRootPath;
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
              return _context.News != null ? 
                          View(await _context.News.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.News'  is null.");
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Post,ImageFile,AltText,DateCreated")] News news)
        {
            if (ModelState.IsValid)
            {

                    if (news.ImageFile != null) {

                    //Spara bilder till wwwroot
                    string fileName = Path.GetFileNameWithoutExtension(news.ImageFile.FileName);
                    string extension = Path.GetExtension(news.ImageFile.FileName);

                    //Plockar bort mellanslag i filnam + lägger till timestamp
                    news.ImageName = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/imageupload", fileName);

                    //Lagra Fil
                    using (var fileStream = new FileStream(path, FileMode.Create)) 
                    {
                        await news.ImageFile.CopyToAsync(fileStream);
                    }

                    //Funktion för att ange bildens storlek
                    createImageFile(fileName);

                }
                else {
                    news.ImageName = null;
                }


                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Post,ImageName, ImageFile, AltText,DateCreated")] News news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
            try {
            //HEADER
             if (news.ImageFile != null) {

                     //Spara bilder till wwwroot
                    string fileName = Path.GetFileNameWithoutExtension(news.ImageFile.FileName);
                    string extension = Path.GetExtension(news.ImageFile.FileName);

                    //Plockar bort mellanslag i filnam + lägger till timestamp
                    news.ImageName = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/imageupload", fileName);

                     //Lagra Fil
                    using (var fileStream = new FileStream(path, FileMode.Create)) 
                    {
                        await news.ImageFile.CopyToAsync(fileStream);
                    }

                    createImageFile(fileName);

                 }


                  else {
                    if(news.ImageName != "") 
                    {
                       news.ImageName = news.ImageName;
                    }
                    else 
                    {
                        news.ImageName =null;
                    }

                  }

                    _context.Update(news);
                    await _context.SaveChangesAsync();
            }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
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
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }
            var news = await _context.News.Include(s => s.Comment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.News == null)
            {
                return Problem("Entity set 'ApplicationDbContext.News'  is null.");
            }
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
          return (_context.News?.Any(e => e.Id == id)).GetValueOrDefault();
        }



        //Funktion för bilder
         
         private void createImageFile(string fileName) {

            using(var img = Image.FromFile(Path.Combine(wwwRootPath + "/imageupload/" , fileName))) {
              
               img.Scale(ImageWidth, ImageHeigth).SaveAs(Path.Combine(wwwRootPath + "/imageupload" + fileName)); 
            } 


         }


    }
}