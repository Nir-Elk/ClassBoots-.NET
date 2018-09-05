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
    public class SchoolsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SchoolsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<School>> GetAll()
        {
            return _context.School.ToList();
        }

        [HttpGet("{id}/Subjects")]
        public ActionResult<List<Subject>> GetSubjects(int id)
        {
            var item = _context.Subject.Where(o => o.SchoolID == id);
            if (item == null)
            {
                return NotFound();
            }
            return item.ToList();
        }
    }
}