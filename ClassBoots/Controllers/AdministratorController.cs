using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassBoots.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClassBoots.Controllers
{
    [Authorize]
    public class AdministratorController : Controller
    {

		public IActionResult Index()
        {
			if (User.FindFirst("Role").Value == "Admin")
			{
				return View();
			}
			else
				return NotFound("Access Denied.");
        }
        public IActionResult Statistics()
        {
			if (User.FindFirst("Role").Value == "Admin")
			{
				return View();
			}
			else
				return NotFound("Access Denied.");
			}
		public IActionResult Users()
		{
			if (User.FindFirst("Role").Value == "Admin")
			{
				return View();
			}
			else
				return NotFound("Access Denied.");
		}


	}
}