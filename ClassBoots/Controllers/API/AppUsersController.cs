using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClassBoots.Data;
using ClassBoots.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ClassBoots.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AppUsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {

            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public ActionResult<string> GetCurrentUserEmail()
        {
            //var item = _context.AppUser.Where(o => o.UserID.Equals(_userManager.GetUserId(HttpContext.User))).ToList()[0];
            //4131dd9b-19e4-4471-9c5c-838f9bdbf88f
            //if (item == null)
            //{
            //    return NotFound();
            //}

            return _userManager.GetUserName(HttpContext.User);
        }
        [HttpGet("all")]
        public ActionResult<AppUser> GetSubjects()
        {
            var item = _context.AppUser.Where(o => o.Email.Equals(_userManager.GetUserName(HttpContext.User)));
            if (item == null)
            {
                return NotFound();
            }
            return item.ToList()[0];
        }

        //[HttpGet("{id}")]
        //public ActionResult<AppUser> GetById(string id)
        //{
        //    var item = _context.AppUser.Find(id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }
        //    return item;
        //}
    }
}