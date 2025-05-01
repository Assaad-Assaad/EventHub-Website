using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventHubWebsite.Data;
using EventHubWebsite.Entities;
using Microsoft.AspNetCore.Identity;

namespace EventHubWebsite.Controllers
{
    public class UserEventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public UserEventsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserEvents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserEvents.Include(u => u.Event).Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEvent = await _context.UserEvents
                .Include(u => u.Event)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userEvent == null)
            {
                return NotFound();
            }

            return View(userEvent);
        }

        // GET: UserEvents/Create
        public IActionResult Create(int eventId)
        {
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description", eventId);
            return View();
        }

        // POST: UserEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,EventId,Event,AppUser")] UserEvent userEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description", userEvent.EventId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userEvent.UserId);
            return View(userEvent);
        }

        // GET: UserEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEvent = await _context.UserEvents.FindAsync(id);
            if (userEvent == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description", userEvent.EventId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userEvent.UserId);
            return View(userEvent);
        }

        // POST: UserEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,EventId")] UserEvent userEvent)
        {
            if (id != userEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserEventExists(userEvent.Id))
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
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description", userEvent.EventId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userEvent.UserId);
            return View(userEvent);
        }

        // GET: UserEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEvent = await _context.UserEvents
                .Include(u => u.Event)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userEvent == null)
            {
                return NotFound();
            }

            return View(userEvent);
        }

        //POST: UserEvents/Delete/5/ By The Admin
        //To redirect the admin to the index page of all events
        [HttpPost, ActionName("DeleteAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAdmin(int id)
        {
            var userEvent = await _context.UserEvents.FindAsync(id);
            if (userEvent != null)
            {
                _context.UserEvents.Remove(userEvent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Handle the user registration for an event.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignInForEvent(int eventId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized();
            }

            var userEvent = new UserEvent
            {
                UserId = user.Id,
                EventId = eventId
            };

            _context.UserEvents.Add(userEvent);
            await _context.SaveChangesAsync();

            return RedirectToAction("MyEvents"); // Redirect to MyEvents view
        }


        // GET: UserEvents/MyEvents
        public async Task<IActionResult> MyEvents()
        {
            var user = await _userManager.GetUserAsync(User);
            var userEvents = await _context.UserEvents
                                           .Include(ue => ue.Event)
                                           .Where(ue => ue.UserId == user.Id)
                                           .ToListAsync();
            return View(userEvents);
        }

        //POST: UserEvents/Delete/5/By The User 
        //To redirect the user to his events
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedUser(int id)
        {
            var userEvent = await _context.UserEvents.FindAsync(id);
            if (userEvent != null)
            {
                _context.UserEvents.Remove(userEvent);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(MyEvents));
        }



        private bool UserEventExists(int id)
        {
            return _context.UserEvents.Any(e => e.Id == id);
        }
    }
}
