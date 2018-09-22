using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassBoots.Models;
using Microsoft.AspNetCore.Authorization;

namespace ClassBoots.Controllers
{
    [Authorize]
    public class InstitutionController : Controller
    {
        private readonly ModelContext _context;

        public InstitutionController(ModelContext context)
        {
            _context = context;
        }

        // GET: Institution
        public async Task<IActionResult> Index()
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                return View(await _context.Institution.ToListAsync());
            }
            return NotFound("Access Dinied");
        }

        // GET: Institution/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var institution = await _context.Institution
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (institution == null)
                {
                    return NotFound();
                }

                return View(institution);
            }
            return NotFound("Access Dinied");
        }

        // GET: Institution/Create
        public IActionResult Create()
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                return View();
            }
            return NotFound("Access Dinied");
        }

        // POST: Institution/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Address,GeoLocation,Image")] Institution institution)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                if (ModelState.IsValid)
                {
                    _context.Add(institution);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(institution);
            }
            return NotFound("Access Dinied");
        }

        // GET: Institution/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var institution = await _context.Institution.FindAsync(id);
                if (institution == null)
                {
                    return NotFound();
                }
                return View(institution);
            }
            return NotFound("Access Dinied");
        }

        // POST: Institution/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Address,GeoLocation,Image")] Institution institution)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                if (id != institution.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(institution);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!InstitutionExists(institution.ID))
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
                return View(institution);
            }
            return NotFound("Access Dinied");
        }

        // GET: Institution/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var institution = await _context.Institution
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (institution == null)
                {
                    return NotFound();
                }

                return View(institution);
            }
            return NotFound("Access Dinied");
        }

        // POST: Institution/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                var institution = await _context.Institution.FindAsync(id);
                _context.Institution.Remove(institution);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return NotFound("Access Dinied");
        }

        private bool InstitutionExists(int id)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                return _context.Institution.Any(e => e.ID == id);
            }
            return false;
        }
    }
}
