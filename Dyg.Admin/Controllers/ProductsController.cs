using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dyg.Core.Data;
using Dyg.Core.Model;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Dyg.Admin.Models;

namespace Dyg.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;
        public ProductsController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            Security.LoginCheck(HttpContext);
            var applicationDbContext = _context.Products.Include(p => p.Brand).Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(string id)
        {
            Security.LoginCheck(HttpContext);
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            Security.LoginCheck(HttpContext);
            Product p = new Product();
            p.CreateDate = DateTime.Now;
            p.CreatedBy = User.Identity.Name;
            p.UpdateDate = DateTime.Now;
            p.UpdatedBy = User.Identity.Name;
            p.PublishDate = DateTime.Now;

            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View(p);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CategoryId,BrandId,Photo,Price,Stock,CreateDate,CreatedBy,PublishDate,IsPublished,UpdateDate,UpdatedBy")] Product product,IFormFile upload)
        {
            Security.LoginCheck(HttpContext);
            // dosya uzantısı için geçerlilik denetimi
            if (upload != null && !IsExtensionValid(upload))
            {
                ModelState.AddModelError("Photo", "Dosya uzantısı .jpg, .jpeg, .gif veya .png olmalıdır.");
            }
            else if (upload == null && product.Photo == null) // eğer resim yüklemeyi zorunlu yapmak istemiyorsanız bu else if'i kaldırın
            {
                ModelState.AddModelError("Photo", "Resim yüklemeniz gerekmektedir.");
            }


            if (ModelState.IsValid)
            {
                product.CreateDate = DateTime.Now;
                product.CreatedBy = User.Identity.Name;
                product.UpdateDate = DateTime.Now;
                product.UpdatedBy = User.Identity.Name;
                product.PublishDate = DateTime.Now;

                // dosya yüklemesi
                string fileName = await UploadFileAsync(upload);
                if (fileName != null)
                {
                    product.Photo = fileName;
                }


                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            Security.LoginCheck(HttpContext);
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
           


           
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,CategoryId,BrandId,Photo,Price,Stock,CreateDate,CreatedBy,PublishDate,IsPublished,UpdateDate,UpdatedBy")] Product product,IFormFile upload)
        {
            Security.LoginCheck(HttpContext);
            if (id != product.Id)
            {
                return NotFound();
            }

            if (upload != null && !IsExtensionValid(upload))
            {
                ModelState.AddModelError("Photo", "Dosya uzantısı .jpg, .jpeg, .gif veya .png olmalıdır.");
            }
            //else if (upload == null && product.Photo == null) // eğer resim yüklemeyi zorunlu yapmak istemiyorsanız bu else if'i kaldırın
            //{
            //    ModelState.AddModelError("Photo", "Resim yüklemeniz gerekmektedir.");
            //}


            if (ModelState.IsValid)
            {
                try
                {
                    product.UpdateDate = DateTime.Now;
                    product.UpdatedBy = User.Identity.Name;

                    // dosya yüklemesi
                    //string fileName = await UploadFileAsync(upload);
                    if (upload !=null && upload.Length>0 && IsExtensionValid(upload))
                    {
                        product.Photo = await UploadFileAsync(upload);
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
           
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            Security.LoginCheck(HttpContext);
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Security.LoginCheck(HttpContext);
            var product = await _context.Products.SingleOrDefaultAsync(m => m.Id == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        // upload edilecek dosyanın uzantısı geçerli mi?
        private bool IsExtensionValid(IFormFile upload)
        {
            if (upload != null)
            {
                var allowedExtensions = new string[] { ".jpg", ".jpeg", ".gif", ".png" };
                var extension = Path.GetExtension(upload.FileName).ToLowerInvariant();
                return allowedExtensions.Contains(extension);
            }
            return false;
        }


        private async Task<string> UploadFileAsync(IFormFile upload)
        {
            if (upload != null && upload.Length > 0 && IsExtensionValid(upload))
            {
                var fileName = upload.FileName;
                var extension = Path.GetExtension(fileName);
                // sitenin içinde uploads dizinine yüklenecek
                var uploadLocation = Path.Combine(hostingEnvironment.WebRootPath, "uploads");

                // eğer uploads dizini yoksa oluştur
                if (!Directory.Exists(uploadLocation))
                {
                    Directory.CreateDirectory(uploadLocation);
                }

                uploadLocation += "/" + fileName;

                using (var stream = new FileStream(uploadLocation, FileMode.Create))
                {
                    await upload.CopyToAsync(stream);
                }
                return fileName;
            }
            return null;
        }
    }
}
