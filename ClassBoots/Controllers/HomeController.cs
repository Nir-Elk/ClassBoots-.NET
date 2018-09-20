using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClassBoots.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;


namespace ClassBoots.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        public HomeController(IConfiguration configuration, ModelContext context)
        {
            Configuration = configuration;
            this.context = context;
        }

        public IConfiguration Configuration { get; }
        public ModelContext context { get; }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            ViewData["mapURL"] = "https://maps.googleapis.com/maps/api/js?key=" + "AIzaSyBy6yBkOFkfGLpxtBAzZ-pM6eGZa2yNLDA" /*Configuration["google:key"]*/+ "&callback=initMap";

            /*
             * 1) Get all points (lat, long and their name...)
             * 2) ViewData["Points"] = [{},{}]
             * 3) in about.cshtml - loop over ViewData["Points"] and add them to map by creating a new marker....
             */

            var institutes = getAllInstitutions();

            List<GeoData> geoData = new List<GeoData>();

            foreach (var inst in institutes)
            {
                var latLng = inst.GeoLocation.Replace(" ", "").Split(',');

                if (latLng.Length == 2)
                {
                    geoData.Add(new GeoData
                    {
                        Lat = latLng[0],
                        Long = latLng[1],
                        Title = inst.Name,
                        Image = inst.Image
                    });
                }
            }

            ViewData["institutes"] = geoData;

            return View();
        }

        private List<Institution> getAllInstitutions()
        {
            return context.Institution.ToList();
        }

        public IActionResult Search()
        {
            ViewData["Message"] = "Search your ask:";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class GeoData
    {
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }
}
