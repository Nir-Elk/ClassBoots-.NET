using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassBoots.Data;
using ClassBoots.Models;
using Microsoft.AspNetCore.Identity;


namespace ClassBoots.Controllers
{
    public class AppUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public AppUsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: AppUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.AppUser.ToListAsync());
        }

        // GET: AppUsers/Details
        public async Task<IActionResult> Details()
        {
            string mail = _userManager.GetUserName(HttpContext.User);
            if (mail == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUser
                .FirstOrDefaultAsync(m => m.Email == mail);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // GET: AppUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,GroupID,FirstName,LastName")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }

        // GET: AppUsers/Edit/5
        public async Task<IActionResult> Edit(string Email)
        {
            if (Email == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUser.FindAsync(Email);
            if (appUser == null)
            {
                return NotFound();
            }
            return View(appUser);
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Email, [Bind("Email,GroupID,FirstName,LastName")] AppUser appUser)
        {
            if (Email != appUser.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserExists(appUser.Email))
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
            return View(appUser);
        }

        // GET: AppUsers/Delete/5
        public async Task<IActionResult> Delete(string Email)
        {
            if (Email == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUser
                .FirstOrDefaultAsync(m => m.Email == Email);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string Email)
        {
            var appUser = await _context.AppUser.FindAsync(Email);
            _context.AppUser.Remove(appUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserExists(string Email)
        {
            return _context.AppUser.Any(e => e.Email == Email);
        }
    }
}
