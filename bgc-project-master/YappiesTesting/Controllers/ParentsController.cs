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
    [Authorize(Roles = "Admin, Program Supervisor, Parent")]
    public class ParentsController : Controller
    {
        private readonly yappiesTestingContext _context;

        public ParentsController(yappiesTestingContext context)
        {
            _context = context;
        }

        // GET: Parents
        public async Task<IActionResult> Index(string SearchString, string SearchEmail, string SearchPhone, string actionButton, string sortDirection, string sortField)
        {
            ViewData["Filtering"] = "";

            var parents = from a in _context.Parents
                          select a;

            if (!String.IsNullOrEmpty(SearchString))
            {
                parents = parents.Where(p => p.LastName.ToUpper().Contains(SearchString.ToUpper())
                || p.FirstName.ToUpper().Contains(SearchString.ToUpper()));
                ViewData["Filtering"] = "in";
            }
            if (!String.IsNullOrEmpty(SearchEmail))
            {
                parents = parents.Where(p => p.Email.Contains(SearchEmail));
                ViewData["Filtering"] = "in";
            }
            if (!String.IsNullOrEmpty(SearchPhone))
            {
                parents = parents.Where(p => p.Phone.ToString().Contains(SearchPhone));
                ViewData["Filtering"] = "in";
            }
                return View(await _context.Parents.ToListAsync());
        }

        // GET: Parents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parent = await _context.Parents
                .Include(p => p.Programs).ThenInclude(pp => pp.Program)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parent == null)
            {
                return NotFound();
            }

            return View(parent);
        }

        // GET: Parents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Email,Phone")] Parent parent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(parent);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException dex)
            {
                if (dex.InnerException.Message.Contains("IX_Parents_Email"))
                {
                    ModelState.AddModelError("Email", "Unable to save changes. Remember, you cannot have duplicate Email Addresses");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system admin.");
                }
            }
            return View(parent);
        }

        // GET: Parents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parent = await _context.Parents.FindAsync(id);
            if (parent == null)
            {
                return NotFound();
            }
            return View(parent);
        }

        // POST: Parents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Byte[] RowVersion, [Bind("ID,FirstName,LastName,Email,Phone,NotificationOptIn")] Parent parent)
        {
            var parentToUpdate = await _context.Parents.SingleOrDefaultAsync(p => p.ID == id);
            if (parentToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Parent>(parentToUpdate, "",
                p => p.FirstName, p => p.LastName, p => p.Email, p => p.Phone, p => p.NotificationOptIn))
            {
                try
                {
                    _context.Entry(parentToUpdate).Property("RowVersion").OriginalValue = RowVersion;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException dex)
                {
                    var exceptionEntry = dex.Entries.Single();
                    var clientValues = (Parent)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError("",
                            "Unable to save changes. The Parent was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Parent)databaseEntry.ToObject();
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
                        parentToUpdate.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
                catch (DbUpdateException dex)
                {
                    if (dex.InnerException.Message.Contains("IX_Parents_Email"))
                    {
                        ModelState.AddModelError("Email", "Unable to save changes. Remember, you cannot have duplicate Email Addresses.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system admin.");
                    }
                }
            }
            return View(parent);
        }

        // GET: Parents/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parent = await _context.Parents
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parent == null)
            {
                return NotFound();
            }

            return View(parent);
        }

        // POST: Parents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parent = await _context.Parents.FindAsync(id);
            try
            {
                _context.Parents.Remove(parent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Settings Parents/Settings/5
        public async Task<IActionResult> Settings(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ID == id);

            if(parent == null)
            {
                return NotFound();
            }
            return View(parent);
        }

        // POST: Settings Parents/Settings/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Settings(int id, Byte[] RowVersion, [Bind("ID,FirstName,LastName,Email,Phone,NotificationOptIn,SignOutOptIn,TwoFactorOptIn,DirectMessageOptIn")] Parent parent)
        {
            var parentToUpdate = await _context.Parents.SingleOrDefaultAsync(p => p.ID == id);
            if (parentToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Parent>(parentToUpdate, "",
                p => p.FirstName, p => p.LastName, p => p.Email, p => p.Phone, p => p.NotificationOptIn, p => p.SignOutOptIn, p => p.TwoFactorOptIn, p => p.DirectMessageOptIn))
            {
                try
                {
                    _context.Entry(parentToUpdate).Property("RowVersion").OriginalValue = RowVersion;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException dex)
                {
                    var exceptionEntry = dex.Entries.Single();
                    var clientValues = (Parent)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError("",
                            "Unable to save changes. The Parent was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Parent)databaseEntry.ToObject();
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
                        parentToUpdate.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
                catch (DbUpdateException dex)
                {
                    if (dex.InnerException.Message.Contains("IX_Parents_Email"))
                    {
                        ModelState.AddModelError("Email", "Unable to save changes. Remember, you cannot have duplicate Email Addresses.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system admin.");
                    }
                }
            }
            string redirectAction = "Settings/" + id;
            return RedirectToAction(redirectAction);
        }

        // GET: Profile Parents/Profile/5
        public IActionResult Profile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var children = _context.Children
                .Include(pa => pa.Parent)
                .Include(pr => pr.Programs).ThenInclude(p => p.Program)
                .Where(c => c.ParentID == id);

            ViewData["ParentID"] = id;
            ViewData["ProgramID"] = new SelectList(_context.Programs.Where(p => p.Parents.Any(pa => pa.ParentID == id)), "ID", "ProgramName");
            return View(children);
        }

        //Add Child method
        public async Task<IActionResult> CreateChild(string FirstName, string LastName, int ParentID)
        {
            var child = new Child();
            child.FirstName = FirstName;
            child.ParentID = ParentID;

            _context.Add(child);
            await _context.SaveChangesAsync();

            string redirectAction = "Profile/" + ParentID;
            return RedirectToAction(redirectAction);
        }

        //Add Child To Program Method
        public async Task<IActionResult> AddChildProgram(int ChildID, int ProgramID, int ParentID)
        {
            var childProgram = new Program_Child();

            childProgram.ChildID = ChildID;
            childProgram.ProgramID = ProgramID;

            _context.Add(childProgram);
            await _context.SaveChangesAsync();

            string redirectAction = "Profile/" + ParentID;
            return RedirectToAction(redirectAction);
        }

        private bool ParentExists(int id)
        {
            return _context.Parents.Any(e => e.ID == id);
        }
    }
}
