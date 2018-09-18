using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassBoots.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;

namespace ClassBoots.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly ModelContext _context;

        public VideosController(ModelContext context)
        {
            _context = context;
        }

        // GET: api/Videos
        [HttpGet]
        public IEnumerable<Video> GetVideo()
        {
            return _context.Video;
        }

        // GET: api/Videos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVideo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var video = await _context.Video.FindAsync(id);

            if (video == null)
            {
                return NotFound();
            }

            return Ok(video);
        }

        //// GET: api/Videos/5/getPathss
        //[HttpGet("{id}/getpath")]
        //public Path GetPath([FromRoute] int id)
        //{
        //    Path path = new Path();
        //    var query = from video in _context.Video
        //                join lecture in _context.Lecture on video.LectureID equals lecture.ID
        //                join subject in _context.Subject on lecture.SubjectID equals subject.ID
        //                join school in _context.School on subject.SchoolID equals school.ID
        //                join institution in _context.Institution on school.InstitutionID equals institution.ID
        //                where video.ID == id
        //                select new
        //                {
        //                    videoName = video.Name,
        //                    lectureName = lecture.Name,
        //                    subjectName = subject.Name,
        //                    schoolName = school.Name,
        //                    institutionName = institution.Name,
        //                };


        //    var result = query.ToList().First();
        //    path.video = result.videoName;
        //    path.lecture = result.lectureName;
        //    path.subject = result.subjectName;
        //    path.school = result.schoolName;
        //    path.institution = result.institutionName;
        //    return path;
        //}

        // GET: Search & Filters by - Nir & Amit code, aviv not included.
        [HttpGet("Search/{keyword}/{filters?}")]
        public Object Search([FromRoute] string keyword, [FromRoute] string filters = "{}")
        {
            JObject json = JObject.Parse(filters);
            var result = _context.Video.Join(_context.Lecture, v => v.LectureID, l => l.ID, (v, l) => new { v, l })
                .Join(_context.Subject, vl => vl.l.SubjectID, s => s.ID, (vl, s) => new { vl, s })
                .Join(_context.School, vls => vls.s.SchoolID, s => s.ID, (vls, s) => new { vls, s })
                .Join(_context.Institution, vlss => vlss.s.InstitutionID, i => i.ID, (vlss, i) => new { vlss, i })
                .Where(vlssi => vlssi.vlss.vls.vl.v.Name.Contains(keyword))
                .Where(vlssi => json.ContainsKey("subject") ? vlssi.vlss.vls.s.Name.Equals(json.GetValue("subject")) : true)
                .Where(vlssi => json.ContainsKey("school") ? vlssi.vlss.s.Name.Equals(json.GetValue("school")) : true)
                .Where(vlssi => json.ContainsKey("institution") ? vlssi.i.Name.Equals(json.GetValue("institution")) : true)
                .Select(vlssi => new Path(vlssi.i, vlssi.vlss.s, vlssi.vlss.vls.s, vlssi.vlss.vls.vl.l, vlssi.vlss.vls.vl.v)).ToList();
            return result;

            //Aviv bad code -> very very bad after first looking..
            //var result = from item in _context.Video
            //             join lecture in _context.Lecture on item.LectureID equals lecture.ID
            //             join subject in _context.Subject on lecture.SubjectID equals subject.ID
            //             join school in _context.School on subject.SchoolID equals school.ID
            //             join institution in _context.Institution on school.InstitutionID equals institution.ID
            //             where item.Name.Contains(keyword)
            //             select item;

        }
        // PUT: api/Videos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVideo([FromRoute] int id, [FromBody] Video video)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != video.ID)
            {
                return BadRequest();
            }

            _context.Entry(video).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoExists(id))
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

        // POST: api/Videos
        [HttpPost]
        public async Task<IActionResult> PostVideo([FromBody] Video video)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Video.Add(video);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVideo", new { id = video.ID }, video);
        }

        // DELETE: api/Videos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var video = await _context.Video.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }

            _context.Video.Remove(video);
            await _context.SaveChangesAsync();

            return Ok(video);
        }

        private bool VideoExists(int id)
        {
            return _context.Video.Any(e => e.ID == id);
        }
    }
}