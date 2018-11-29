using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClassBoots.Models;
using Microsoft.AspNetCore.Authorization;

namespace ClassBoots.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Hi this is us.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Map()
        {
            ViewData["Message"] = "Hi. here is our supported places, feel free to add one!";

            return View();
        }

        public IActionResult Error()
        {
            ViewData["Message"] = "Error";
            List<String> dogs = new List<String>();
            dogs.Add("/images/dogs/yuli.jpg");
            dogs.Add("/images/dogs/sandy.jpeg");
            dogs.Add("/images/dogs/vaitzman.jpeg");
            dogs.Add("/images/dogs/bruno.jpg");
            dogs.Add("/images/dogs/annie.jpg");
            dogs.Add("/images/dogs/allen.jpeg");
            Random rand = new Random();
            int index = rand.Next(dogs.Count);

            ViewData["Dog"] = dogs[index];

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult TermsAndConditions()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
