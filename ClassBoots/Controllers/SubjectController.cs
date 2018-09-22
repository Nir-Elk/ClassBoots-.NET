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
    public class SubjectController : Controller
    {
        private readonly ModelContext _context;

        public SubjectController(ModelContext context)
        {
            _context = context;
        }

        // GET: Subject
        public async Task<IActionResult> Index()
        {

            if (User.FindFirst("Role").Value == "Admin")
            {
                return View(await _context.Subject.ToListAsync());
            }
            else
                return NotFound("Access Dinied");
        }

        // GET: Subject/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var subject = await _context.Subject
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (subject == null)
                {
                    return NotFound();
                }

                return View(subject);
            }
            else
                return NotFound("Access Dinied");   
        }

        // GET: Subject/Create
        public IActionResult Create()
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                return View();
            }
            else
                return NotFound("Access Dinied");

            }

        // POST: Subject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SchoolID,Name,Description")] Subject subject)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                if (ModelState.IsValid)
                {
                    _context.Add(subject);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(subject);
            }
            else
                return NotFound("Access Dinied");
        }

        // GET: Subject/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {

                if (id == null)
                {
                    return NotFound();
                }

                var subject = await _context.Subject.FindAsync(id);
                if (subject == null)
                {
                    return NotFound();
                }
                return View(subject);
            }
            else
                return NotFound("Access Dinied");

        }

        // POST: Subject/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SchoolID,Name,Description")] Subject subject)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                if (id != subject.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(subject);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SubjectExists(subject.ID))
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
                return View(subject);
            }
            else
                return NotFound("Access Dinied");
        }

        // GET: Subject/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var subject = await _context.Subject
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (subject == null)
                {
                    return NotFound();
                }

                return View(subject);
            }
            else
                return NotFound("Access Dinied");
        }

        // POST: Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                var subject = await _context.Subject.FindAsync(id);
            _context.Subject.Remove(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            else
                return NotFound("Access Dinied");

        }

        private bool SubjectExists(int id)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                return _context.Subject.Any(e => e.ID == id);
            }
            else
                return false;
        }
    }
}
