using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dyg.Core.Data;
using Dyg.Core.Model;
using System.Security.Cryptography;
using System.Text;
using Dyg.Admin.Models;
using Microsoft.AspNetCore.Identity;
namespace Dyg.Admin.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        //private readonly UserManager<Dyg.Core.Model.User> _userManager;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
            //_userManager = userManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            Security.LoginCheck(HttpContext);
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            Security.LoginCheck(HttpContext);
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        
        // GET: Users/Create
        public IActionResult Create()
        {
            //Security.LoginCheck(HttpContext);
            User user = new User();
            user.CreateDate = DateTime.Now;
            user.CreatedBy = User.Identity.Name;
            user.UpdateDate = DateTime.Now;
            user.UpdatedBy = User.Identity.Name;
            return View(user);
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Password,ConfirmPassword,CreateDate,CreatedBy,UpdateDate,UpdatedBy")] User user)
        {
            //Security.LoginCheck(HttpContext);
            if (ModelState.IsValid)
            {
                user.CreateDate = DateTime.Now;
                user.CreatedBy = User.Identity.Name;
                user.UpdateDate = DateTime.Now;
                user.UpdatedBy = User.Identity.Name;
                user.Password = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(user.Password)));
                try
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // herhangi bir hata olursa burası çalışır
                    ModelState.AddModelError("Email", "Bu e-posta adresi daha önce kullanılmış.");
                }
              
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            Security.LoginCheck(HttpContext);

            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Email,Password,ConfirmPassword,CreateDate,CreatedBy,UpdateDate,UpdatedBy")] User user)
        {
            Security.LoginCheck(HttpContext);
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try {
                    user.UpdateDate = DateTime.Now;
                    user.UpdatedBy = User.Identity.Name;
                    user.Password = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(user.Password)));
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            Security.LoginCheck(HttpContext);
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Security.LoginCheck(HttpContext);
            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
