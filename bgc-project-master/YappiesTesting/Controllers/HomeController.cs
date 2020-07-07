using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YappiesTesting.Data;
using YappiesTesting.Models;

namespace YappiesTesting.Controllers
{
    public class HomeController : Controller
    {
        private readonly yappiesTestingContext _context;

        public HomeController(yappiesTestingContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, int? programID, int? childID)
        {
            if (User.IsInRole("Admin"))
            {
                var yappiesTestingContext = _context.Programs;
                return View("~/Views/Home/IndexAdmin.cshtml", await yappiesTestingContext.ToListAsync());
            }
            else if (User.IsInRole("Parent"))
            {
                var yappiesTestingContext = _context.Parents
                    .Include(p => p.Programs).ThenInclude(pp => pp.Program)
                                             .ThenInclude(pp => pp.ProgramSupervisor)
                    .Include(p => p.Programs).ThenInclude(pp => pp.Program)
                                             .ThenInclude(pp => pp.Announcements)
                    .Where(p => p.Email == User.Identity.Name)
                    .FirstOrDefault();

                var displayAnnouncements = _context.Announcements.Where(p => p.IsGlobal == true);

                ViewBag.Announcements = displayAnnouncements;

                return View("Index", yappiesTestingContext);
            }
            else if (User.IsInRole("Program Supervisor"))
            {
                var yappiesTestingContext = _context.ProgramSupervisors
                    .Include(ps => ps.Programs)
                    .Where(p => p.Email == User.Identity.Name)
                    .FirstOrDefault();

                var displayAnnouncements = _context.Announcements.Where(p => p.IsGlobal == true);

                ViewBag.Announcements = displayAnnouncements;

                return View("IndexSupervisor", yappiesTestingContext);
            }
            else
            {
                ViewData["Title"] = "Log in";
                return View("~/Views/Shared/_LoginPartial.cshtml");
            }

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

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
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
