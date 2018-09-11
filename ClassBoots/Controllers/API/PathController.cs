using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassBoots.Models;
using Microsoft.AspNetCore.Authorization;


namespace ClassBoots.Controllers
{
    [AllowAnonymous]
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

        [HttpGet("{institution_id}/{school_id}")]
        public IEnumerable<Subject> GetSubjects([FromRoute] int school_id)
        {
            return _context.Subject.Where(s => s.SchoolID == school_id).ToList();
        }

        [HttpGet("{institution_id}/{school_id}/{subject_id}")]
        public IEnumerable<Lecture> GetLectures([FromRoute] int subject_id)
        {
            return _context.Lecture.Where(s => s.SubjectID == subject_id).ToList();
        }

        [HttpGet("{institution_id}/{school_id}/{subject_id}/{lecture_id}")]
        public IEnumerable<Video> GetVideos([FromRoute] int lecture_id)
        {
            return _context.Video.Where(s => s.LectureID == lecture_id).ToList();
        }


    }
}