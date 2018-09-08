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
    public class PathController : ControllerBase
    {
        private readonly ModelContext _context;

        public PathController(ModelContext context)
        {
            _context = context;
        }

        // GET: api/Path
        [HttpGet]
        public IEnumerable<Institution> GetInstitutions()
        {
            return _context.Institution;
        }

        [HttpGet("{institution_id}")]
        public IEnumerable<School> GetSchools([FromRoute] int institution_id)
        {
            return _context.School.Where(s => s.InstitutionID == institution_id).ToList();
        }




    }
}