using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassBoots.Models;
using System.Security.Claims;

namespace ClassBoots.Controllers
{
    public class LectureController : Controller
    {
        private readonly ModelContext _context;

        public LectureController(ModelContext context)
        {
            _context = context;
        }

        // GET: Lecture
        public async Task<IActionResult> Index()
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                return View(await _context.Lecture.ToListAsync());
            }
            else
                return NotFound("Access Dinied");
            }

            // GET: Lecture/Details/5
            public async Task<IActionResult> Details(int? id)
            {
            var lecture = await _context.Lecture
                                        .FirstOrDefaultAsync(m => m.ID == id);
            if (User.FindFirst("Role").Value == "Admin" || lecture.OwnerID == User.FindFirst(ClaimTypes.Name).Value)
            {
                    if (id == null)
                {
                    return NotFound();
                }
                if (lecture == null)
                {
                    return NotFound();
                }

                return View(lecture);
            }
            else
                return NotFound("Access Dinied");

        }

        // GET: Lecture/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lecture/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LecturerID,SubjectID,Name,Description,Image,Date,OwnerID")] Lecture lecture)
        {
            if (ModelState.IsValid)
            {
                lecture.OwnerID = User.FindFirst(ClaimTypes.Name).Value;
                _context.Add(lecture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lecture);
        }

        // GET: Lecture/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var lecture = await _context.Lecture.FindAsync(id);
            if (User.FindFirst("Role").Value == "Admin" || lecture.OwnerID == User.FindFirst(ClaimTypes.Name).Value)
            {
                if (id == null)
                {
                    return NotFound();
                }
                if (lecture == null)
                {
                    return NotFound();
                }
                return View(lecture);
            }
            else
                return NotFound("Access Dinied");
    
        }

        // POST: Lecture/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LecturerID,SubjectID,Name,Description,Image,Date,OwnerID")] Lecture lecture)
        {
            if (User.FindFirst("Role").Value == "Admin" || lecture.OwnerID == User.FindFirst(ClaimTypes.Name).Value)
            {
                if (id != lecture.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lecture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LectureExists(lecture.ID))
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
            return View(lecture);
            }
            else
                return NotFound("Access Dinied");

        }

        // GET: Lecture/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
 
            var lecture = await _context.Lecture
                .FirstOrDefaultAsync(m => m.ID == id);
            if (User.FindFirst("Role").Value == "Admin" || lecture.OwnerID == User.FindFirst(ClaimTypes.Name).Value)
            {
                    if (id == null)
                {
                    return NotFound();
                }

                if (lecture == null)
                {
                    return NotFound();
                }

                return View(lecture);
            }
            else
                return NotFound("Access Dinied");

        }

        // POST: Lecture/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lecture = await _context.Lecture.FindAsync(id);
            if (User.FindFirst("Role").Value == "Admin" || lecture.OwnerID == User.FindFirst(ClaimTypes.Name).Value)
            {
                _context.Lecture.Remove(lecture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
                return NotFound("Access Dinied");
        }

        private bool LectureExists(int id)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                return _context.Lecture.Any(e => e.ID == id);
            }
            else
                return false;   
        }
    }
}
