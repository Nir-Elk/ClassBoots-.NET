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

        [HttpGet("{id}")]
        public ActionResult<School> GetById(int id)
        {
            var item = _context.School.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
    }
}