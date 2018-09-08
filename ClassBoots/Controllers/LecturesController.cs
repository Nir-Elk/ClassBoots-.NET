using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassBoots.Models;

namespace ClassBoots.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturesController : ControllerBase
    {
        private readonly ModelContext _context;

        public LecturesController(ModelContext context)
        {
            _context = context;
        }

        // GET: api/Lectures
        [HttpGet]
        public IEnumerable<Lecture> GetLecture()
        {
            return _context.Lecture;
        }

        // GET: api/Lectures/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLecture([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lecture = await _context.Lecture.FindAsync(id);

            if (lecture == null)
            {
                return NotFound();
            }

            return Ok(lecture);
        }

        // PUT: api/Lectures/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLecture([FromRoute] int id, [FromBody] Lecture lecture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lecture.ID)
            {
                return BadRequest();
            }

            _context.Entry(lecture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LectureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Lectures
        [HttpPost]
        public async Task<IActionResult> PostLecture([FromBody] Lecture lecture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Lecture.Add(lecture);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLecture", new { id = lecture.ID }, lecture);
        }

        // DELETE: api/Lectures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLecture([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lecture = await _context.Lecture.FindAsync(id);
            if (lecture == null)
            {
                return NotFound();
            }

            _context.Lecture.Remove(lecture);
            await _context.SaveChangesAsync();

            return Ok(lecture);
        }

        private bool LectureExists(int id)
        {
            return _context.Lecture.Any(e => e.ID == id);
        }
    }
}