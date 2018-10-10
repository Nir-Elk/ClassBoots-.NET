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
    public class LecturerController : Controller
    {
        private readonly ModelContext _context;

        public LecturerController(ModelContext context)
        {
            _context = context;
        }

        // GET: Lecturer
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lecturer.ToListAsync());
        }

        // GET: Lecturer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturer
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lecturer == null)
            {
                return NotFound();
            }

            return View(lecturer);
        }

        // GET: Lecturer/Create
        public IActionResult Create()
        {
            if (User.FindFirst("Role").Value != "Admin")
                return NotFound("Access Dinied");
            return View();
        }

        // POST: Lecturer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Lecturer lecturer)
        {
            if (User.FindFirst("Role").Value != "Admin")
                return NotFound("Access Dinied");
            if (ModelState.IsValid)
            {
                _context.Add(lecturer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lecturer);
        }

        // GET: Lecturer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.FindFirst("Role").Value != "Admin")
                return NotFound("Access Dinied");
            if (id == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturer.FindAsync(id);
            if (lecturer == null)
            {
                return NotFound();
            }
            return View(lecturer);
        }

        // POST: Lecturer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] Lecturer lecturer)
        {
            if (User.FindFirst("Role").Value != "Admin")
                return NotFound("Access Dinied");
            if (id != lecturer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lecturer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LecturerExists(lecturer.ID))
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
            return View(lecturer);
        }

        // GET: Lecturer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.FindFirst("Role").Value != "Admin")
                return NotFound("Access Dinied");
            if (id == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturer
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lecturer == null)
            {
                return NotFound();
            }

            return View(lecturer);
        }

        // POST: Lecturer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.FindFirst("Role").Value != "Admin")
                return NotFound("Access Dinied");
            var lecturer = await _context.Lecturer.FindAsync(id);
            _context.Lecturer.Remove(lecturer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LecturerExists(int id)
        {
            return _context.Lecturer.Any(e => e.ID == id);
        }
    }
}
