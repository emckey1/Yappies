using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YappiesTesting.Data;
using YappiesTesting.Models;

namespace YappiesTesting.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly yappiesTestingContext _context;

        public AnnouncementsController(yappiesTestingContext context)
        {
            _context = context;
        }

        // GET: Announcements
        public async Task<IActionResult> Index(int? ChildID)
        {
            ViewData["Filtering"] = "";

            var parent = _context.Parents.Where(p => p.Email == User.Identity.Name).FirstOrDefault();

            ViewData["ChildID"] = new SelectList(_context.Children.Where(c => c.ParentID == parent.ID).OrderBy(c => c.FirstName), "ID", "FirstName");

            var yappiesTestingContext = from a in _context.Announcements
                                        .Include(a => a.Program)
                                        where a.Program.Parents.Any(A => A.ParentID == parent.ID)
                                        select a;

            if (ChildID.HasValue)
            {
                yappiesTestingContext = yappiesTestingContext.Where(a => a.Program.Program_Children.Any(c => c.ChildID == ChildID));
                ViewData["Filtering"] = "in";
            }

            return View(await yappiesTestingContext.ToListAsync());
        }

        // GET: Announcements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements
                .Include(a => a.Program)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // GET: Announcements/Create
        public IActionResult Create()
        {
            ViewData["ProgramID"] = new SelectList(_context.Programs, "ID", "ProgramDescription");
            return View();
        }

        // POST: Announcements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Body,ProgramID")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(announcement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProgramID"] = new SelectList(_context.Programs, "ID", "ProgramDescription", announcement.ProgramID);
            return View(announcement);
        }

        // GET: Announcements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }
            ViewData["ProgramID"] = new SelectList(_context.Programs, "ID", "ProgramDescription", announcement.ProgramID);
            return View(announcement);
        }

        // POST: Announcements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Body,ProgramID")] Announcement announcement)
        {
            if (id != announcement.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(announcement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnouncementExists(announcement.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProgramID"] = new SelectList(_context.Programs, "ID", "ProgramDescription", announcement.ProgramID);
            return View(announcement);
        }

        // GET: Announcements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements
                .Include(a => a.Program)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // POST: Announcements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnouncementExists(int id)
        {
            return _context.Announcements.Any(e => e.ID == id);
        }
    }
}
