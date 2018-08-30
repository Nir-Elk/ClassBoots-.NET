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
    public class VideosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VideosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Video>> GetAll()
        {
            return _context.Video.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Video> GetById(int id)
        {
            var item = _context.Video.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
    }
}