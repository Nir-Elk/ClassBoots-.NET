using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassBoots.Models;

namespace ClassBoots.Controllers
{
    public class SchoolController : Controller
    {
        private readonly ModelContext _context;

        public SchoolController(ModelContext context)
        {
            _context = context;
        }

        // GET: School
        public async Task<IActionResult> Index()
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                return View(await _context.School.ToListAsync());
            }
            else
                return NotFound("Access Dinied");
        }

        // GET: School/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var school = await _context.School
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (school == null)
                {
                    return NotFound();
                }

                return View(school);
            }
            else
                return NotFound("Access Dinied");

        }

        // GET: School/Create
        public IActionResult Create()
        {

            if (User.FindFirst("Role").Value == "Admin")
            {
                return View();
            }
            else
                return NotFound("Access Dinied");
        }

        // POST: School/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,InstitutionID,Name")] School school)
        {

            if (User.FindFirst("Role").Value == "Admin")
            {
                if (ModelState.IsValid)
                {
                    _context.Add(school);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(school);
            }
            else
                return NotFound("Access Dinied");

        }

        // GET: School/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {

                if (id == null)
                {
                    return NotFound();
                }

                var school = await _context.School.FindAsync(id);
                if (school == null)
                {
                    return NotFound();
                }
                return View(school);
            }
            else
                return NotFound("Access Dinied");
        }

        // POST: School/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,InstitutionID,Name")] School school)
        {

            if (User.FindFirst("Role").Value == "Admin")
            {
                if (id != school.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(school);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SchoolExists(school.ID))
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
                return View(school);
            }
            else
                return NotFound("Access Dinied");

        }

        // GET: School/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var school = await _context.School
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (school == null)
                {
                    return NotFound();
                }

                return View(school);
            }
            else
                return NotFound("Access Dinied");

        }

        // POST: School/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                var school = await _context.School.FindAsync(id);
                _context.School.Remove(school);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
                return NotFound("Access Dinied");

        }

        private bool SchoolExists(int id)
        {

            if (User.FindFirst("Role").Value == "Admin")
            {
                return _context.School.Any(e => e.ID == id);
            }
            else
                return false;
        }
    }
}
