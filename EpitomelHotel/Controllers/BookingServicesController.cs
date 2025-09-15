using EpitomelHotel.Areas.Identity.Data;
using EpitomelHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EpitomelHotel.Controllers
{
    public class BookingServicesController : Controller
    {
        private readonly EpitomelHotelDbContext _context;

        public BookingServicesController(EpitomelHotelDbContext context)
        {
            _context = context;
        }

        // GET: BookingServices
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Get all rooms that the user has bookings for
            var userRoomIds = await _context.Bookings
                .Where(b => b.ApplUserID == userId)
                .Select(b => b.RoomID)
                .Distinct()
                .ToListAsync();

            // Get all services linked to those rooms
            var bookingServices = await _context.BookingService
                .Include(bs => bs.Room)
                .Include(bs => bs.Service)
                .Where(bs => userRoomIds.Contains(bs.RoomID))
                .ToListAsync();

            return View(bookingServices);
        }





        // GET: BookingServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var bookingService = await _context.BookingService
                .Include(b => b.Room)
                .Include(b => b.Service)
                .FirstOrDefaultAsync(m => m.BookingServiceID == id);

            if (bookingService == null) return NotFound();

            return View(bookingService);
        }

        // GET: BookingServices/Create
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Get only the rooms booked by this user
            var userRooms = _context.Rooms
                .Where(r => r.Booking.Any(b => b.ApplUserID == userId))
                .Select(r => new { r.RoomID, r.RoomNumber }) // assuming RoomNumber is the display field
                .ToList();

            ViewData["RoomID"] = new SelectList(userRooms, "RoomID", "RoomNumber");

            // All services can still be listed
            ViewData["ServiceID"] = new SelectList(_context.Services, "ServiceID", "ServiceName");

            // Also pass down whether the user has services already
            ViewBag.HasServices = _context.BookingService
                .Any(bs => bs.Room.Booking.Any(b => b.ApplUserID == userId));

            return View();
        }


        // POST: BookingServices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingService bookingService)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Ensure the room belongs to the current user
            var roomValid = await _context.Rooms
                .AnyAsync(r => r.RoomID == bookingService.RoomID &&
                               r.Booking.Any(b => b.ApplUserID == userId));

            if (!roomValid)
            {
                ModelState.AddModelError("RoomID", "You can only select your own booked rooms.");
            }

            if (!ModelState.IsValid)
            {
                _context.Add(bookingService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // repopulate dropdowns on error
            var userRooms = _context.Rooms
                .Where(r => r.Booking.Any(b => b.ApplUserID == userId))
                .Select(r => new { r.RoomID, r.RoomNumber })
                .ToList();

            ViewData["RoomID"] = new SelectList(userRooms, "RoomID", "RoomNumber", bookingService.RoomID);
            ViewData["ServiceID"] = new SelectList(_context.Services, "ServiceID", "ServiceName", bookingService.ServiceID);

            return View(bookingService);
        }


        // GET: BookingServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var bookingService = await _context.BookingService.FindAsync(id);
            if (bookingService == null) return NotFound();

            ViewData["ServiceID"] = new SelectList(_context.Services, "ServiceID", "ServiceName", bookingService.ServiceID);
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomNumber", bookingService.RoomID);

            return View(bookingService);
        }

        // POST: BookingServices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingServiceID,ServiceCost,RoomID,ServiceID")] BookingService bookingService)
        {
            if (id != bookingService.BookingServiceID) return NotFound();

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingServiceExists(bookingService.BookingServiceID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["ServiceID"] = new SelectList(_context.Services, "ServiceID", "ServiceName", bookingService.ServiceID);
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomNumber", bookingService.RoomID);

            return View(bookingService);
        }

        // GET: BookingServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var bookingService = await _context.BookingService
                .Include(b => b.Room)
                .Include(b => b.Service)
                .FirstOrDefaultAsync(m => m.BookingServiceID == id);

            if (bookingService == null) return NotFound();

            return View(bookingService);
        }

        // POST: BookingServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookingService = await _context.BookingService.FindAsync(id);
            if (bookingService != null)
            {
                _context.BookingService.Remove(bookingService);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BookingServiceExists(int id)
        {
            return _context.BookingService.Any(e => e.BookingServiceID == id);
        }
    }
}
