using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dyg.Admin.Models;
using Dyg.Core.Data;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Text;
using Dyg.Core.Model;

namespace Dyg.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            Security.LoginCheck(HttpContext);

            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                login.Password = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(login.Password)));
                var user = await _context.Users.
                    FirstOrDefaultAsync(u => u.Email == login.UserName && u.Password == login.Password);
                if (user != null)
                {
                    // login işlemi
                    HttpContext.Session.SetString("UserName", login.UserName);
                    return RedirectToAction("Index", "Products");
                }
                ModelState.AddModelError("Result", "Geçersiz kullanıcı adı veya şifre!");
            }
            return View(login);
        }
        public IActionResult Logout()
        {
            Security.LoginCheck(HttpContext);
            HttpContext.Session.SetString("UserName", "");
            return RedirectToAction("Login", "Home");
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        //public IActionResult Profile(string id)
        //{
           
        //    return RedirectToAction("Details","Users") ;
        //}

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
