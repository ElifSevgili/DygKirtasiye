using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dyg.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Dyg.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private ApplicationDbContext context;
        public CategoriesController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index(string id)
        {
            var categories = context.Categories.Include(p => p.ParentCategory).Include(p=>p.SubCategories).Where(p=>p.ParentCategoryId ==null).OrderBy(o => o.Name).ToList();
            ViewBag.Categories = categories;
            
            var product = context.Products.Include(c => c.Category).Where(p => p.CategoryId == id && p.IsPublished == true)
                .OrderByDescending(o => o.PublishDate).ToList();
            return View(product);
        }
    }

}

