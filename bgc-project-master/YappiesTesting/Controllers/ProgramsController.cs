using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using YappiesTesting.Data;
using YappiesTesting.Models;

namespace YappiesTesting.Controllers
{
    public class ProgramsController : Controller
    {
        private readonly yappiesTestingContext _context;

        public ProgramsController(yappiesTestingContext context)
        {
            _context = context;
        }

        // GET: Programs
        public async Task<IActionResult> Index(string SearchString, string actionButton, string sortDirection, string sortField, int? ChildID)
        {
            ViewData["Filtering"] = "";

            var parent = _context.Parents.Where(p => p.Email == User.Identity.Name).FirstOrDefault();

            ViewData["ChildID"] = new SelectList(_context.Children.Where(c => c.ParentID == parent.ID).OrderBy(c => c.FirstName), "ID", "FirstName");

            var programs = from p in _context.Programs
                           where p.Parents.Any(A => A.ParentID == parent.ID)
                           select p;

            if (!String.IsNullOrEmpty(SearchString))
            {
                programs = programs.Where(a => a.ProgramName.ToUpper().Contains(SearchString.ToUpper()));
                ViewData["Filtering"] = "in";
            }
            if (ChildID.HasValue)
            {
                programs = programs.Where(a => a.Program_Children.Any(c => c.ChildID == ChildID));
                ViewData["Filtering"] = "in";
            }

            if (User.IsInRole("Admin"))
            {
                return View("IndexAdmin", await programs.ToListAsync());
            }
            else if (User.IsInRole("Program Supervisor"))
            {
                return View("IndexPS", await programs.ToListAsync());
            }
            else
            {
                return View(await programs.ToListAsync());
            }
        }

        // GET: Programs/Details/5
        public async Task<IActionResult> Details(int? id, int? ParentID, int? ProgramSupervisorID)
        {
            if (id == null)
            {
                return NotFound();
            }

            var program = await _context.Programs
                .Include(p => p.ProgramSupervisor)
                .Include(p => p.Activities)
                .Include(p => p.Announcements).OrderBy(a => a.CreatedOn)
                .Include(p => p.Parents).ThenInclude(pa => pa.Parent)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (program == null)
            {
                return NotFound();
            }

            ViewData["ProgramSupervisorID"] = ProgramSupervisorID;
            ViewData["ParentID"] = ParentID;
            return View(program);
        }

        // GET: Programs/Create
        [Authorize(Roles = "Program Supervisor, Admin")]
        public IActionResult AdminCreate()
        {
            ViewData["ProgramSupervisorID"] = new SelectList(_context.ProgramSupervisors, "ID", "Supervisor");
            return View("AdminCreate");
        }

        [Authorize(Roles = "Program Supervisor, Admin")]
        public IActionResult Create(int? ProgramSupervisorID)
        {
            ViewData["ProgramSupervisorID"] = ProgramSupervisorID;
            return View();
        }


        // POST: Programs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Program Supervisor, Admin")]
        public async Task<IActionResult> Create([Bind("ID,ProgramName,ProgramDescription,ProgramJoinCode,ProgramSupervisorID")] Models.Program program)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(program);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (DbUpdateException dex)
            {
                if (dex.InnerException.Message.Contains("IX_Programs_ProgramJoinCode"))
                {
                    ModelState.AddModelError("ProgramJoinCode", "Unable to save changes. Remember, you cannot have duplicate Program Join Codes.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system admin.");
                }
            }
            ViewData["ProgramSupervisorID"] = new SelectList(_context.ProgramSupervisors, "ID", "Email", program.ProgramSupervisorID);
            return View(program);
        }

        // GET: Programs/Edit/5
        [Authorize(Roles = "Program Supervisor, Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var program = await _context.Programs.FindAsync(id);
            if (program == null)
            {
                return NotFound();
            }
            ViewData["ProgramSupervisorID"] = new SelectList(_context.ProgramSupervisors, "ID", "Email", program.ProgramSupervisorID);
            return View(program);
        }

        // POST: Programs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Program Supervisor, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Byte[] RowVersion, [Bind("ID,ProgramName,ProgramDescription,ProgramJoinCode,ProgramSupervisorID")] Models.Program program)
        {
            var programToUpdate = await _context.Programs.SingleOrDefaultAsync(p => p.ID == id);
            if (programToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Models.Program>(programToUpdate, "",
                p => p.ProgramName, p => p.ProgramDescription, p => p.ProgramJoinCode, p => p.ProgramSupervisorID))
            {
                try
                {
                    _context.Entry(programToUpdate).Property("RowVersion").OriginalValue = RowVersion;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException dex)
                {
                    var exceptionEntry = dex.Entries.Single();
                    var clientValues = (Models.Program)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError("",
                            "Unable to save changes. The Program was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Models.Program)databaseEntry.ToObject();
                        if (databaseValues.ProgramName != clientValues.ProgramName)
                            ModelState.AddModelError("ProgramName", "Current value: "
                                + databaseValues.ProgramName);
                        if (databaseValues.ProgramDescription != clientValues.ProgramDescription)
                            ModelState.AddModelError("ProgramDescription", "Current value: "
                                + databaseValues.ProgramDescription);
                        if (databaseValues.ProgramJoinCode != clientValues.ProgramJoinCode)
                            ModelState.AddModelError("ProgramJoinCode", "Current value: "
                                + databaseValues.ProgramJoinCode);
                        if (databaseValues.ProgramSupervisorID != clientValues.ProgramSupervisorID)
                        {
                            ProgramSupervisor databaseAssignment = await _context.ProgramSupervisors.SingleOrDefaultAsync(i => i.ID == databaseValues.ProgramSupervisorID);
                            ModelState.AddModelError("ProgramSupervisorID", $"Current value: {databaseAssignment?.Supervisor}");
                        }
                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you received your values. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to save your version of this record, click "
                                + "the Save button again. Otherwise click the 'Back to List' hyperlink.");
                        programToUpdate.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
                catch (DbUpdateException dex)
                {
                    if (dex.InnerException.Message.Contains("IX_Programs_ProgramJoinCode"))
                    {
                        ModelState.AddModelError("Email", "Unable to save changes. Remember, you cannot have duplicate Program Join Codes.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    }
                }
            }
            ViewData["ProgramSupervisorID"] = new SelectList(_context.ProgramSupervisors, "ID", "Supervisor", program.ProgramSupervisorID);
            return View(program);
        }

        // GET: Programs/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var program = await _context.Programs
                .Include(p => p.ProgramSupervisor)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (program == null)
            {
                return NotFound();
            }

            return View(program);
        }

        // POST: Programs/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var program = await _context.Programs.FindAsync(id);
            try
            {
                _context.Programs.Remove(program);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dex)
            {
                if (dex.InnerException.Message.Contains("FK_Activities_ProgramID"))
                {
                    ModelState.AddModelError("", "Unable to save changes. Remember, you cannot delete a Program that has Activities.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(program);
        }

        public async Task<IActionResult> CreateProgramAnnouncement (string title, string body, int programid)
        {
            var announcement = new Models.Announcement();
            announcement.Title = title;
            announcement.Body = body;
            announcement.ProgramID = programid;
            announcement.CreatedOn = DateTime.Now;

            _context.Add(announcement);
            await _context.SaveChangesAsync();

            string redirectAction = "Details/" + programid;
            return RedirectToAction(redirectAction);
        }

        public async Task<IActionResult> AddParentToCurrentProgram(string email, int programid)
        {
            var parent = _context.Parents.SingleOrDefault(p => p.Email == email);

            if (parent == null)
            {
                parent = new Models.Parent();
                parent.Email = email;
                parent.FirstName = "Placeholder";
                parent.LastName = "Placeholder";
                parent.Phone = 1234567890;

                var programs = new List<Program_Parent>();
                var allPrograms = _context.Programs;
                foreach (var prog in allPrograms)
                {
                    if (prog.ID == programid)
                    {
                        parent.Programs.Add(new Program_Parent
                        {
                            ProgramID = programid,
                            ParentID = parent.ID
                        });
                    }
                }

                _context.Add(parent);
                await _context.SaveChangesAsync();
            }
            else if (parent != null)
            {
                var allPrograms = _context.Programs;
                foreach (var prog in allPrograms)
                {
                    if (prog.ID == programid)
                    {
                        parent.Programs.Add(new Program_Parent
                        {
                            ProgramID = programid,
                            ParentID = parent.ID
                        });
                    }
                }
                await _context.SaveChangesAsync();
            }
            string redirectAction = "Details/" + programid;
            return RedirectToAction(redirectAction);
        }

        public async Task<IActionResult> AddParentsFromExcel(IFormFile formFile, int programid)
        {
            ExcelPackage excel;
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                excel = new ExcelPackage(memoryStream);
            }
            // what sheet we are using (first sheet of excel file)
            var workSheet = excel.Workbook.Worksheets[0];
            // where the viewer starts in the sheet
            var start = workSheet.Dimension.Start;
            var end = workSheet.Dimension.End;
            for (int row = start.Row; row <= end.Row; row++)
            {
                // going through each row of the data, create parents with excel data
                var parent = new Parent();
                parent.Email = workSheet.Cells[row, 1].Text;
                parent.FirstName = workSheet.Cells[row, 2].Text;
                parent.LastName = workSheet.Cells[row, 3].Text;


                // if the parent doesnt have a matching user account
                if (_context.Parents.SingleOrDefault(p => p.Email == parent.Email) == null)
                {
                    var allPrograms = _context.Programs;
                    foreach (var prog in allPrograms)
                    {
                        if (prog.ID == programid)
                        {
                            // create the relationship
                            parent.Programs.Add(new Program_Parent
                            {
                                ProgramID = programid,
                                ParentID = parent.ID
                            });
                        }
                    }
                    // add the parent to the table
                    _context.Parents.Add(parent);
                }
                // if the parent does exist, only create the relationship
                if (_context.Parents.SingleOrDefault(p => p.Email == parent.Email) != null)
                {
                    var allPrograms = _context.Programs;
                    foreach (var prog in allPrograms)
                    {
                        if (prog.ID == programid)
                        {
                            parent.Programs.Add(new Program_Parent
                            {
                                ProgramID = programid,
                                ParentID = parent.ID
                            });
                        }
                    }
                }
            }
            _context.SaveChanges();

            string redirectAction = "Details/" + programid;
            return RedirectToAction(redirectAction);

        }


        private bool ProgramExists(int id)
        {
            return _context.Programs.Any(e => e.ID == id);
        }
    }
}
