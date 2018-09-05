using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClassBoots.Data;
using ClassBoots.Models;

namespace ClassBoots.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Subject>> GetAll()
        {
            return _context.Subject.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Subject> GetById(int id)
        {
            var item = _context.Subject.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
        [HttpGet("{id}/Lectures")]
        public ActionResult<List<Lecture>> GetSubjects(int id)
        {
            var item = _context.Lecture.Where(o => o.LecturerID.Equals(id));
            if (item == null)
            {
                return NotFound();
            }
            return item.ToList();
        }
    }
}