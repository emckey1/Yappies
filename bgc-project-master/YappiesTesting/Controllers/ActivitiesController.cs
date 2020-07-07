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
    public class ActivitiesController : Controller
    {
        private readonly yappiesTestingContext _context;

        public ActivitiesController(yappiesTestingContext context)
        {
            _context = context;
        }

        // GET: Activities
        public async Task<IActionResult> Index(int? ProgramID, string SearchString, string actionButton, string sortDirection, string sortField)
        {
            PopulateDropDownLists();
            ViewData["Filtering"] = "";

            var activities = from a in _context.Activities
                             .Include(a => a.Program)
                             select a;

            if (!String.IsNullOrEmpty(SearchString))
            {
                activities = activities.Where(a => a.Title.ToUpper().Contains(SearchString.ToUpper()));
                ViewData["Filtering"] = "in";
            }
            if (ProgramID.HasValue)
            {
                activities = activities.Where(p => p.ProgramID == ProgramID);
                ViewData["Filtering"] = "in";
            }
            if (!String.IsNullOrEmpty(actionButton))
            {
                if (actionButton != "Filter")
                {
                    if (actionButton == sortField)
                    {
                        sortDirection = String.IsNullOrEmpty(sortDirection) ? "desc" : "";
                    }
                    sortField = actionButton;
                }
            }
            return View(await activities.ToListAsync());
        }

        // GET: Activities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities
                .Include(a => a.Program)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // GET: Activities/Create
        public IActionResult Create(int? programid)
        {
            ViewData["ProgramID"] = programid;
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,Date,ProgramID")] Activity activity, int programID)
        {
            string redirectAction;
            if (ModelState.IsValid)
            {
                _context.Add(activity);
                await _context.SaveChangesAsync();
                redirectAction = "Details/" + activity.ProgramID;
                return RedirectToAction(redirectAction, "Programs");
            }
            ViewData["ProgramID"] = new SelectList(_context.Programs, "ID", "ProgramName", activity.ProgramID);
            redirectAction = "Details/" + activity.ProgramID;
            return RedirectToAction(redirectAction, "Programs");
        }

        // GET: Activities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            ViewData["ProgramID"] = new SelectList(_context.Programs, "ID", "ProgramName", activity.ProgramID);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,Date,ProgramID")] Activity activity)
        {
            if (id != activity.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityExists(activity.ID))
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
            ViewData["ProgramID"] = new SelectList(_context.Programs, "ID", "ProgramName", activity.ProgramID);
            return View(activity);
        }

        // GET: Activities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities
                .Include(a => a.Program)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private SelectList ProgramSelectList(int? id)
        {
            var dQuery = from i in _context.Programs
                         orderby i.ProgramName
                         select i;
            return new SelectList(dQuery, "ID", "ProgramName", id);
        }

        private void PopulateDropDownLists(Activity activity = null)
        {
            ViewData["ProgramID"] = ProgramSelectList(activity?.ProgramID);
        }

        private bool ActivityExists(int id)
        {
            return _context.Activities.Any(e => e.ID == id);
        }
    }
}
