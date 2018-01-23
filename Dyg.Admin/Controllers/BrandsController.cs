using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dyg.Core.Data;
using Dyg.Core.Model;
using Dyg.Admin.Models;

namespace Dyg.Admin.Controllers
{
    public class BrandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Brands
        public IActionResult Index()
        {
            Security.LoginCheck(HttpContext);
            var category = _context.Brands.Include(b => b.BrandCategories).ThenInclude(bc => bc.Category).OrderBy(b=>b.Name).ToList();
            ViewBag.Categories = category;

            //var brands = (from e in _context.Brands
            //              join b in _context.BrandCategories on e.Id equals b.BrandId
            //              join c in _context.Categories on b.CategoryId equals c.Id
            //              select new Brand { CategoryId = c.Id });
            //ViewBag.Brands = brands;

            
            //var result = (
            //// instance from context
            //from a in _context.Brands
            //    // instance from navigation property
            //from b in a.BrandCategories
            //from c in b.CategoryId
            //    //join to bring useful data
            //join d in _context.Categories on b.CategoryId equals d.Id
            
            //select new CategoryViewModel
            //{
               
            //    Name = d.Name
            //}).ToList();

            //ViewBag.Result = result;
            return View(category);

           

        }


        // GET: Brands/Details/5
        public async Task<IActionResult> Details(string id)
        {
            Security.LoginCheck(HttpContext);
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .SingleOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Brands/Create
        public IActionResult Create()
        {

            Security.LoginCheck(HttpContext);
            Brand b = new Brand();

            b.CreateDate = DateTime.Now;
            b.CreatedBy = User.Identity.Name;
            b.UpdateDate = DateTime.Now;
            b.UpdatedBy = User.Identity.Name;
            b.PublishDate = DateTime.Now;

            var category = _context.Categories.Where(n => n.ParentCategoryId == null).OrderBy(o => o.Name).ToList(); ;
            ViewBag.Categories = category;
            return View(b);

        }
        // POST: Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreateDate,CreatedBy,PublishDate,IsPublished,UpdateDate,UpdatedBy")] Brand brand)
        {
            Security.LoginCheck(HttpContext);
            if (ModelState.IsValid)
            {
                brand.CreateDate = DateTime.Now;
                brand.CreatedBy = User.Identity.Name;
                brand.UpdateDate = DateTime.Now;
                brand.UpdatedBy = User.Identity.Name;
                brand.PublishDate = DateTime.Now;

                var category = _context.Categories.Where(n => n.ParentCategoryId == null).OrderBy(o => o.Name).ToList(); ;
                ViewBag.Categories = category;

                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            Security.LoginCheck(HttpContext);
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands.SingleOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,CreateDate,CreatedBy,PublishDate,IsPublished,UpdateDate,UpdatedBy")] Brand brand)
        {
            Security.LoginCheck(HttpContext);
            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    brand.UpdateDate = DateTime.Now;
                    brand.UpdatedBy = User.Identity.Name;

                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.Id))
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
            return View(brand);
        }

        // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            Security.LoginCheck(HttpContext);
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .SingleOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Security.LoginCheck(HttpContext);
            var brand = await _context.Brands.SingleOrDefaultAsync(m => m.Id == id);
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(string id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }
    }
}
