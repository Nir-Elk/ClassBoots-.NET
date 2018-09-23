using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassBoots.Areas.Identity.Data;
using ClassBoots.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClassBoots.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
		private readonly ModelContext _context;
		private readonly UserManager<User> _userManager;

		public AdministratorController(UserManager<User> userManager,ModelContext context)
		{
			_context = context;
			_userManager = userManager;
		}


		// GET: api/Users
		[HttpGet]
		public IEnumerable<User> GetUsers()
		{
			if (User.FindFirst("Role").Value == "Admin")
			{
				return _userManager.Users;
			}
			else
			{
				return null;
			}
		}
	}
}