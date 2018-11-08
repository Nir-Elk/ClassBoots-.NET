using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassBoots.Models;
using Microsoft.AspNetCore.Authorization;

namespace ClassBoots.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly ModelContext _context;

        public SchoolsController(ModelContext context)
        {
            _context = context;
        }

        // GET: api/Schools
        [HttpGet]
        public IEnumerable<School> GetSchool()
        {
            return _context.School;
        }

        // GET: api/Schools/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchool([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var school = await _context.School.FindAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            return Ok(school);
        }

        // PUT: api/Schools/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchool([FromRoute] int id, [FromBody] School school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != school.ID)
            {
                return BadRequest();
            }

            _context.Entry(school).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolExists(id))
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

        // POST: api/Schools
        [HttpPost]
        public async Task<IActionResult> PostSchool([FromBody] School school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.School.Add(school);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchool", new { id = school.ID }, school);
        }

        // DELETE: api/Schools/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var school = await _context.School.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }

            _context.School.Remove(school);
            await _context.SaveChangesAsync();

            return Ok(school);
        }

        private bool SchoolExists(int id)
        {
            return _context.School.Any(e => e.ID == id);
        }
    }
}