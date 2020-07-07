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
    [Authorize]
    public class ProgramSupervisorsController : Controller
    {
        private readonly yappiesTestingContext _context;

        public ProgramSupervisorsController(yappiesTestingContext context)
        {
            _context = context;
        }

        // GET: ProgramSupervisors
        public async Task<IActionResult> Index(string SearchString, string SearchEmail, string SearchPhone, string actionButton, string sortDirection, string sortField)
        {
            //PopulateDropDownLists();
            ViewData["Filtering"] = "";

            var programSup = from a in _context.ProgramSupervisors
                             .Include(a => a.Programs)
                             select a;

            if (!String.IsNullOrEmpty(SearchString))
            {
                programSup = programSup.Where(a => a.LastName.ToUpper().Contains(SearchString.ToUpper())
                || a.FirstName.ToUpper().Contains(SearchString.ToUpper()));
                ViewData["Filtering"] = "in";
            }
            if (!String.IsNullOrEmpty(SearchEmail))
            {
                programSup = programSup.Where(a => a.Email.Contains(SearchEmail));
                ViewData["Filtering"] = "in";
            }
            if (!String.IsNullOrEmpty(SearchPhone))
            {
                programSup = programSup.Where(a => a.Phone.ToString().Contains(SearchPhone));
                ViewData["Filtering"] = "in";
            }

            if (User.IsInRole("Admin"))
            {
                return View("IndexAdmin", await _context.ProgramSupervisors.ToListAsync());
            }
            else if (User.IsInRole("Program Supervisor"))
            {
                return View("IndexPS", await _context.ProgramSupervisors.ToListAsync());
            }
            else
            {
                return View(await _context.ProgramSupervisors.ToListAsync());
            }
        }

        // GET: ProgramSupervisors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programSupervisor = await _context.ProgramSupervisors
                .Include(p => p.Programs)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (programSupervisor == null)
            {
                return NotFound();
            }

            return View(programSupervisor);
        }

        // GET: ProgramSupervisors/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProgramSupervisors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Email,Phone")] ProgramSupervisor programSupervisor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(programSupervisor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException dex)
            {
                if (dex.InnerException.Message.Contains("IX_ProgramSupervisors_Email"))
                {
                    ModelState.AddModelError("Email", "Unable to save changes. Remember, you cannot have duplicate Email Addresses");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system admin.");
                }
            }
            return View(programSupervisor);
        }

        // GET: ProgramSupervisors/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programSupervisor = await _context.ProgramSupervisors.FindAsync(id);
            if (programSupervisor == null)
            {
                return NotFound();
            }
            return View(programSupervisor);
        }

        // POST: ProgramSupervisors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Byte[] RowVersion)
        {
            var programSupToUpdate = await _context.ProgramSupervisors.SingleOrDefaultAsync(p => p.ID == id);
            if (programSupToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<ProgramSupervisor>(programSupToUpdate, "",
                p => p.FirstName, p => p.LastName, p => p.Email, p => p.Phone))
            {
                try
                {
                    _context.Entry(programSupToUpdate).Property("RowVersion").OriginalValue = RowVersion;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException dex)
                {
                    var exceptionEntry = dex.Entries.Single();
                    var clientValues = (ProgramSupervisor)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError("",
                            "Unable to save changes. The Program Supervisor was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (ProgramSupervisor)databaseEntry.ToObject();
                        if (databaseValues.FirstName != clientValues.FirstName)
                            ModelState.AddModelError("FirstName", "Current value: "
                                + databaseValues.FirstName);
                        if (databaseValues.LastName != clientValues.LastName)
                            ModelState.AddModelError("LastName", "Current value: "
                                + databaseValues.LastName);
                        if (databaseValues.Email != clientValues.Email)
                            ModelState.AddModelError("Email", "Current value: "
                                + databaseValues.Email);
                        if (databaseValues.Phone != clientValues.Phone)
                            ModelState.AddModelError("Phone", "Current value: "
                                + String.Format("{0:(###) ###-####}", databaseValues.Phone));
                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you received your values. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to save your version of this record, click "
                                + "the Save button again. Otherwise click the 'Back to List' hyperlink.");
                        programSupToUpdate.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
                catch (DbUpdateException dex)
                {
                    if (dex.InnerException.Message.Contains("IX_ProgramSupervisors_Email"))
                    {
                        ModelState.AddModelError("Email", "Unable to save changes. Remember, you cannot have duplicate Email Addresses.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system admin.");
                    }
                }
            }
            return View(programSupToUpdate);
        }

        // GET: ProgramSupervisors/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programSupervisor = await _context.ProgramSupervisors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (programSupervisor == null)
            {
                return NotFound();
            }

            return View(programSupervisor);
        }

        // POST: ProgramSupervisors/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var programSupervisor = await _context.ProgramSupervisors.FindAsync(id);
            try
            {
                _context.ProgramSupervisors.Remove(programSupervisor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dex)
            {
                if (dex.InnerException.Message.Contains("FK_Programs_ProgramSupervisorID"))
                {
                    ModelState.AddModelError("", "Unable to save changes. Remember, you cannot delete a Program Supervisor that has Programs.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system admin.");
                }
            }
            return View(programSupervisor);
        }

        //private SelectList ProgramSelectList(int? id)
        //{
        //    var dQuery = from i in _context.Programs
        //                 orderby i.ProgramName
        //                 select i;
        //    return new SelectList(dQuery, "ID", "ProgramName", id);
        //}

        //private void PopulateDropDownLists(ProgramSupervisor programSup = null)
        //{
        //    ViewData["ProgramID"] = ProgramSelectList(programSup?.ProgramID);
        //}

        private bool ProgramSupervisorExists(int id)
        {
            return _context.ProgramSupervisors.Any(e => e.ID == id);
        }
    }
}
