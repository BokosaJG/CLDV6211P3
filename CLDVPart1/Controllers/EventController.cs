using System.Reflection;
using CLDVPart1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

namespace CLDVPart1.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchType, int? venueID, DateTime? startDate, DateTime? endDate)
        {
            var Event = _context.Event
                .Include(e => e.Venue)
                .Include(e => e.EventType)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchType))
                Event = Event.Where(e => e.EventType.Name == searchType);

            if (venueID.HasValue)
                Event = Event.Where(e => e.VenueID == venueID);

            if (startDate.HasValue && endDate.HasValue)
                Event = Event.Where(e => e.EventDate >= startDate && e.EventDate <= endDate);

            ViewBag.EventTypes = _context.EventType.ToList();
            ViewBag.Venues = _context.Venue.ToList();

            return View(await Event.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Venues = _context.Venue.ToList();

            ViewBag.EventTypes = _context.EventType.ToList();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event @event)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Add the new event to the database
                    _context.Add(@event);
                    await _context.SaveChangesAsync();

                    // Set the success message
                    TempData["SuccessMessage"] = "Event created successfully.";

                    // Redirect to the Index view
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    // Handle database errors
                    ModelState.AddModelError("", "An error occurred while creating the event.");
                }
            }

            ViewBag.Venues = _context.Venue.ToList();
            ViewBag.EventTypes = _context.EventType.ToList();
            return View(@event);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.Event
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(m => m.EventID == id);

            if (@event == null) return NotFound();

            return View(@event);
        }

        //Confirm Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.Event
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(e => e.EventID == id);

            if (@event == null) return NotFound();

            return View(@event);
        }
        //Perform Deletion (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            if (@event == null) return NotFound();

            var isBooked = await _context.Booking.AnyAsync(b => b.EventID == id);
            if (isBooked)
            {
                TempData["ErrorMessage"] = "Cannot delete event because it has existing bookings.";
                return RedirectToAction(nameof(Index));
            }
            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Event deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.Event.FindAsync(id);
            if (@event == null) return NotFound();

            ViewBag.Venues = _context.Venue.ToList();
            ViewBag.EventTypes = _context.EventType.ToList();
            return View(@event);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event @event)
        {
            if (id != @event.EventID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(@event);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Event updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Venues = _context.Venue.ToList();
            ViewBag.EventTypes = _context.EventType.ToList();
            return View(@event);
        }

    }
}



