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
    public class LecturesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LecturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Lecture>> GetAll()
        {
            return _context.Lecture.ToList();
        }

        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Lecture> GetById(int id)
        {
            var item = _context.Lecture.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
    }
}