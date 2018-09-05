using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ClassBoots.Data;
using ClassBoots.Models;

namespace ClassBoots.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InstitutionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Institution>> GetAll()
        {
            return _context.Institution.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Institution> GetById(int id)
        {
            var item = _context.Institution.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpGet("{id}/Schools")]
        public ActionResult<List<School>> GetSchools(int id)
        {
            var item = _context.School.Where(row => row.InstitutionID.Equals(id));

            if (item == null)
            {
                return NotFound();
            }
            return item.ToList();
        }
    }
}