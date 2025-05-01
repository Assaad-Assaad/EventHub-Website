using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventHubWebsite.Data;
using EventHubWebsite.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlTypes;

namespace EventHubWebsite.Controllers
{
    
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> Index(int categoryId)
        {
            if(categoryId == 0)
            {
                return View(await _context.Events.ToListAsync());
            }
            else
            {
                List<Event> list = await (from e in _context.Events
                                          where e.CategoryId == categoryId
                                          select new Event
                                          {
                                              Id = e.Id,
                                              Name = e.Name,
                                              ImagePath = e.ImagePath,
                                              Description = e.Description,
                                              HTMLContent = e.HTMLContent,
                                              Added = e.Added,
                                              CategoryId = categoryId
                                          }).ToListAsync();

                ViewBag.CategoryId = categoryId;
                return View(list);

            }
            
        }

        // GET: Events/Details/5
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        [Authorize(Roles = ("Admin"))]
        public IActionResult Create(int categoryId)
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            Event @event = new Event
            {
                CategoryId = categoryId,
                Added = DateTime.Now

            };
            return View(@event);

        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [Authorize(Roles = ("Admin"))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImagePath,Description,HTMLContent,Added,CategoryId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { categoryId = @event.CategoryId });
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [Authorize(Roles = ("Admin"))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImagePath,Description,HTMLContent,Added,CategoryId")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { categoryId = @event.CategoryId });
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [Authorize(Roles = ("Admin"))]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { categoryId = @event.CategoryId });
        }



        //Get all events
        [HttpGet]
        public async Task<IActionResult> AllEvents(int categoryId)
        {
            if (categoryId == 0)
            {
                return View(await _context.Events.ToListAsync());
            }
            else
            {
                List<Event> list = await (from e in _context.Events
                                          where e.CategoryId == categoryId
                                          select new Event
                                          {
                                              Id = e.Id,
                                              Name = e.Name,
                                              ImagePath = e.ImagePath,
                                              Description = e.Description,
                                              HTMLContent = e.HTMLContent,
                                              Added = e.Added,
                                              CategoryId = categoryId
                                          }).ToListAsync();

                ViewBag.CategoryId = categoryId;
                return View(list);

            }

        }


        // GET: Event Detail
        [HttpGet]
        public async Task<IActionResult> EventDetail(int id)
        {
            var eventDetails = await _context.Events.FindAsync(id);

            if (eventDetails == null)
            {
                return NotFound();
            }

            return PartialView("_EventDetailPartial", eventDetails);
        }




        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
