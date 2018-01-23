using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dyg.Core.Data;
using Dyg.Core.Model;

namespace Dyg.Web.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext context;
        public ProductController(ApplicationDbContext context)
        {
            this.context = context;
           
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BuyProduct(string id)
        {
            var buy = context.Products.Where(x => x.Id == id).OrderBy(p=>p.Name).ToList();
            return View(buy);
        }
       
    }
}