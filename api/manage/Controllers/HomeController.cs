using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using manage.Models;
using manage.Common;
using Model.DB;
using Microsoft.AspNetCore.Http;

namespace manage.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            var loginuser=HttpContext.Session.GetObject<User>("loginuser");
            if (loginuser == null)
            {
                Response.Redirect("/Login");
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            string s = "Your contact page.";
            ViewData["Message"] = s;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult aaa()
        {
            return Json(new { dd="sdf"});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
