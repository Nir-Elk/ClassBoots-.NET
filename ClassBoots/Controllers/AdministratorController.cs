using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassBoots.Areas.Identity.Data;
using ClassBoots.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassBoots.Controllers
{
    [Authorize]
    public class AdministratorController : Controller
    {
        private readonly UserContext _context;
        private readonly UserManager<User> _userManager;


        public AdministratorController(UserContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
			if (User.FindFirst("Role").Value == "Admin")
			{
				return View();
			}
			else
				return NotFound("Access Denied.");
        }
        // GET: Administrator/Statistics
        public IActionResult Statistics()
        {
			if (User.FindFirst("Role").Value == "Admin")
			{
				return View();
			}
			else
				return NotFound("Access Denied.");
		}
        // GET: Administrator/Users
        public IActionResult Users()
		{
			if (User.FindFirst("Role").Value == "Admin")
			{
				return View();
			}
			else
				return NotFound("Access Denied.");
		}
        // GET: Administrator/EditUser/5
        //[HttpGet("{id}")]
        public async Task<IActionResult> EditUser(string id)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {

                if (id == null)
                {
                    return NotFound();
                }
                var userToEdit = _context.Users.FirstOrDefault(u => u.Id == id);
                if (userToEdit == null)
                {
                    return NotFound();
                }
                return View(userToEdit);
            }
            else
                return NotFound("Access Dinied");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(string id, [Bind("Id,Name,Role")] User editedUser)
        {
            if (User.FindFirst("Role").Value == "Admin")
            {
                if (id != editedUser.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {

                    var usertmp = await _userManager.GetUserAsync(User);

                    // Update it with the values from the view model
                    usertmp.Name = editedUser.Name;
                    usertmp.Role = editedUser.Role;

                    await _userManager.UpdateAsync(usertmp);

                    return RedirectToAction(nameof(Index));
                }
                return View(User);
            }
            else
                return NotFound("Access Dinied");
        }


    }
}