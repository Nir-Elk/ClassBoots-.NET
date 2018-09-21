using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassBoots.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ClassBoots.Controllers
{
    [Authorize]
    public class VideoController : Controller
    {
        private readonly ModelContext _context;

        public VideoController(ModelContext context)
        {
            _context = context;
        }

        // GET: Video
        public async Task<IActionResult> Index()
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                return View(await _context.Video.ToListAsync());
            }
            else
                return NotFound("Access Dinied");

        }

        // GET: Video/View/5 with full layout!
        public async Task<IActionResult> View(int? id)
        {
            var video = await _context.Video
                                .FirstOrDefaultAsync(m => m.ID == id);
                if (id == null)
                {
                    return NotFound();
                }

                if (video == null)
                {
                    return NotFound();
                }
                video.Views++;
                _context.Update(video);
                await _context.SaveChangesAsync();
                return View(video);
        }

        // GET: Video/Details/5 lightweight layout
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                {
                    return NotFound();
                }
            var video = await _context.Video
                    .FirstOrDefaultAsync(m => m.ID == id);
            video.Views++;
            _context.Update(video);
            await _context.SaveChangesAsync();
            if (video == null)
                {
                    return NotFound();
                }

                return View(video);
        }

        // GET: Video/Create
        public IActionResult Create()
        {
            if (User.FindFirst("Role").Value != null)
            {
                return View();
            }
            else
                return NotFound("Access Dinied");
        }

        // POST: Video/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,LectureID,URL,Views,Position,OwnerID")] Video video)
        {
            if (User.FindFirst("Role").Value != null)
            {
                if (ModelState.IsValid)
            {
                video.OwnerID = User.FindFirst(ClaimTypes.Name).Value;
                _context.Add(video);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(video);
            }
            else
                return NotFound("Access Dinied");
        }

        // GET: Video/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var video = await _context.Video.FindAsync(id);
            if (User.FindFirst("Role").Value == "Admin" || video.OwnerID == User.FindFirst(ClaimTypes.Name).Value)
            {
                if (id == null)
                {
                    return NotFound();
                }
                if (video == null)
                {
                    return NotFound();
                }
                return View(video);
            }
            else
                return NotFound("Access Dinied");

            }

        // POST: Video/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,LectureID,URL,Views,Position,OwnerID")] Video video)
        {
            if (User.FindFirst("Role").Value == "Admin" || video.OwnerID == User.FindFirst(ClaimTypes.Name).Value)
            {
                    if (id != video.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(video);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!VideoExists(video.ID))
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
                return View(video);
            }
            else
                return NotFound("Access Dinied");
        }

        // GET: Video/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var video = await _context.Video
    .FirstOrDefaultAsync(m => m.ID == id);
            if (User.FindFirst("Role").Value == "Admin" || video.OwnerID == User.FindFirst(ClaimTypes.Name).Value)
            {
                if (id == null)
                {
                    return NotFound();
                }
                if (video == null)
                {
                    return NotFound();
                }

                return View(video);
            }
            else
                return NotFound("Access Dinied");

        }

        // POST: Video/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var video = await _context.Video.FindAsync(id);
            if (User.FindFirst("Role").Value == "Admin" || video.OwnerID == User.FindFirst(ClaimTypes.Name).Value)
            {
                _context.Video.Remove(video);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
                return NotFound("Access Dinied");
        }

        private bool VideoExists(int id)
        {
                return _context.Video.Any(e => e.ID == id);
        }
    }
}
