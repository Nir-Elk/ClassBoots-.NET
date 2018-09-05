using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("{id}")]
        public ActionResult<Lecture> GetById(int id)
        {
            var item = _context.Lecture.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
        [HttpGet("{id}/Videos")]
        public ActionResult<List<Video>> GetSubjects(int id)
        {
            var item = _context.Video.Where(o => o.LectureID.Equals(id));
            if (item == null)
            {
                return NotFound();
            }
            return item.ToList();
        }
    }
}