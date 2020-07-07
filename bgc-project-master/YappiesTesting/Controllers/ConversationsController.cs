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
    public class ConversationsController : Controller
    {
        private readonly yappiesTestingContext _context;

        public ConversationsController(yappiesTestingContext context)
        {
            _context = context;
        }

        // GET: Conversations
        public IActionResult Index()
        {
            var yappiesTestingContext = _context.Parents
                .Include(p => p.Conversations)
                .ThenInclude(c => c.ProgramSupervisor)
                .Where(p => p.Email == User.Identity.Name)
                .FirstOrDefault();
            return View(yappiesTestingContext);
        }

        public IActionResult IndexPS()
        {
            var yappiesTestingContext = _context.ProgramSupervisors
                .Include(p => p.Conversations)
                .ThenInclude(c => c.Parent)
                .Where(p => p.Email == User.Identity.Name)
                .FirstOrDefault();
            return View(yappiesTestingContext);
        }

        // GET: Conversations/Create
        public IActionResult Create(int id)
        {
            ViewData["ProgramSupervisorID"] = new SelectList(_context.ProgramSupervisors, "ID", "Supervisor");
            ViewData["ParentID"] = id;
            return View();
        }

        public async Task<IActionResult> CreateFromProgramDetails(int ProgramSupervisorID, int ParentID)
        {
            var conversation = new Conversation();
            conversation.ParentID = ParentID;
            conversation.ProgramSupervisorID = ProgramSupervisorID;

            _context.Add(conversation);
            await _context.SaveChangesAsync();

            conversation = _context.Conversations
                .Include(c => c.Messages)
                .Include(c => c.Parent)
                .Include(c => c.ProgramSupervisor)
                .Where(c => c.ProgramSupervisorID == ProgramSupervisorID && c.ParentID == ParentID)
                .FirstOrDefault();

            return View("Messages", conversation);
        }

        public async Task<IActionResult> CreateFromProgramDetailsPS(int ProgramSupervisorID, int ParentID)
        {
            var conversation = new Conversation();
            conversation.ParentID = ParentID;
            conversation.ProgramSupervisorID = ProgramSupervisorID;

            _context.Add(conversation);
            await _context.SaveChangesAsync();

            conversation = _context.Conversations
                .Include(c => c.Messages)
                .Include(c => c.Parent)
                .Include(c => c.ProgramSupervisor)
                .Where(c => c.ProgramSupervisorID == ProgramSupervisorID && c.ParentID == ParentID)
                .FirstOrDefault();

            return View("MessagesPS", conversation);
        }

        // POST: Conversations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int ProgramSupervisorID, int ParentID)
        {
            var conversation = new Conversation();
            conversation.ParentID = ParentID;
            conversation.ProgramSupervisorID = ProgramSupervisorID;

            _context.Add(conversation);
            await _context.SaveChangesAsync();

            conversation = _context.Conversations
                    .Include(c => c.Messages)
                    .Include(c => c.Parent)
                    .Include(c => c.ProgramSupervisor)
                    .Where(c => c.ProgramSupervisorID == ProgramSupervisorID && c.ParentID == ParentID)
                    .FirstOrDefault();

            return View("Messages", conversation);
        }

        public IActionResult Messages(int ParentID, int ProgramSupervisorID)
        {
            var convervsation = _context.Conversations
                .Include(c => c.Messages)
                .Include(c => c.Parent)
                .Include(c => c.ProgramSupervisor)
                .Where(c => c.ProgramSupervisorID == ProgramSupervisorID && c.ParentID == ParentID)
                .FirstOrDefault();

            return View("Messages", convervsation);
        }

        public IActionResult MessagesPS(int ParentID, int ProgramSupervisorID)
        {
            var convervsation = _context.Conversations
                .Include(c => c.Messages)
                .Include(c => c.Parent)
                .Include(c => c.ProgramSupervisor)
                .Where(c => c.ProgramSupervisorID == ProgramSupervisorID && c.ParentID == ParentID)
                .FirstOrDefault();

            return View("MessagesPS", convervsation);
        }

        public async Task<IActionResult> NewMessage(int ParentID, int ProgramSupervisorID, string MessageText)
        {
            var message = new Message();

            message.MessageText = MessageText;
            message.ParentID = ParentID;
            message.ProgramSupervisorID = ProgramSupervisorID;

            if (User.IsInRole("Parent"))
            {
                message.SentByParent = true;
            }
            else
            {
                message.SentByParent = false;
            }

            _context.Add(message);

            await _context.SaveChangesAsync();

            var convervsation = _context.Conversations
                .Include(c => c.Messages)
                .Include(c => c.Parent)
                .Include(c => c.ProgramSupervisor)
                .Where(c => c.ProgramSupervisorID == ProgramSupervisorID && c.ParentID == ParentID)
                .FirstOrDefault();

            if (User.IsInRole("Parent"))
            {
                return View("Messages", convervsation);
            }
            else
            {
                return View("MessagesPS", convervsation);
            }
        }

        private bool ConversationExists(int id)
        {
            return _context.Conversations.Any(e => e.ParentID == id);
        }
    }
}
