using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EpitomelHotel.Areas.Identity.Data;
using EpitomelHotel.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EpitomelHotel.Controllers
{
    public class BookingsController : Controller
    {
        private readonly EpitomelHotelDbContext _context;

        public BookingsController(EpitomelHotelDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index(string searchString, int? pageNumber, int pageSize = 5)
        {
            var bookings = _context.Bookings
                .Include(b => b.ApplUser)
                .Include(b => b.Room)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(b => b.ApplUser.Firstname.Contains(searchString));
            }

            return View(await bookings.ToListAsync());
        }
        [Authorize]
        public IActionResult Create(DateTime? checkIn, DateTime? checkOut, int? roomId)
        {
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomType");

            if (User.IsInRole("Admin"))
            {
                ViewData["ApplUserID"] = new SelectList(_context.ApplUser, "Id", "Firstname");
            }

            var model = new Bookings();

            if (checkIn.HasValue)
                model.CheckIn = checkIn.Value;

            if (checkOut.HasValue)
                model.CheckOut = checkOut.Value;

            if (roomId.HasValue)
                model.RoomID = roomId.Value;

            // Safely calculate duration only if both dates are provided
            if (checkIn.HasValue && checkOut.HasValue && roomId.HasValue)
            {
                TimeSpan span = checkOut.Value - checkIn.Value;
                int duration = span.Days;

                if (duration > 0)
                {
                    const decimal dailyRate = 75m;
                    model.TotalAmount = dailyRate * duration;
                }
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingID,CheckIn,CheckOut,TotalAmount,PaymentStatus,RoomID")] Bookings bookings)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bookings.ApplUserID = userId;

            if (bookings.CheckIn != default && bookings.CheckOut != default)
            {
                TimeSpan span = (TimeSpan)(bookings.CheckOut - bookings.CheckIn);
                int duration = span.Days;

                if (duration <= 0)
                {
                    ModelState.AddModelError("CheckOut", "Check-out must be after check-in.");
                }
                else
                {
                    const decimal dailyRate = 75m;
                    bookings.TotalAmount = dailyRate * duration;
                }
            }
            else
            {
                ModelState.AddModelError("CheckIn", "Both check-in and check-out dates are required.");
            }

            // Check if room is available
            bool roomUnavailable = await _context.Bookings.AnyAsync(b =>
                b.RoomID == bookings.RoomID &&
                (
                    (bookings.CheckIn >= b.CheckIn && bookings.CheckIn < b.CheckOut) ||
                    (bookings.CheckOut > b.CheckIn && bookings.CheckOut <= b.CheckOut) ||
                    (bookings.CheckIn <= b.CheckIn && bookings.CheckOut >= b.CheckOut)
                ));

            if (roomUnavailable)
            {
                ModelState.AddModelError(string.Empty, "The selected room is not available during the chosen dates.");
            }

            if (string.IsNullOrEmpty(bookings.PaymentStatus))
            {
                bookings.PaymentStatus = "Pending";
            }

            if (!ModelState.IsValid)
            {
                _context.Add(bookings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Confirmation), new { id = bookings.BookingID });
            }

            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomType", bookings.RoomID);
            ViewData["ApplUserID"] = new SelectList(_context.ApplUser, "Id", "Firstname", bookings.ApplUserID);

            return View(bookings);
        }


        [Authorize]
        public async Task<IActionResult> RedirectToMyBookings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction(nameof(Index));
            }

            var hasBookings = await _context.Bookings.AnyAsync(b => b.ApplUserID == userId);
            return hasBookings ? RedirectToAction(nameof(Index)) : RedirectToAction(nameof(Create));
        }

        public async Task<IActionResult> Confirmation(int? id)
        {
            if (id == null)
                return NotFound();

            var booking = await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.ApplUser)
                .FirstOrDefaultAsync(b => b.BookingID == id);

            if (booking == null)
                return NotFound();

            return View(booking);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings == null)
                return NotFound();

            ViewData["ApplUserID"] = new SelectList(_context.ApplUser, "Id", "Id", bookings.ApplUserID);
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomType", bookings.RoomID);
            return View(bookings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingID,CheckIn,CheckOut,TotalAmount,PaymentStatus,ApplUserID,RoomID")] Bookings bookings)
        {
            if (id != bookings.BookingID)
                return NotFound();

            if (bookings.CheckIn != default && bookings.CheckOut != default)
            {
                TimeSpan span = (TimeSpan)(bookings.CheckOut - bookings.CheckIn);
                int duration = span.Days;

                if (duration <= 0)
                {
                    ModelState.AddModelError("CheckOut", "Check-out must be after check-in.");
                }
                else
                {
                    const decimal dailyRate = 75m;
                    bookings.TotalAmount = dailyRate * duration;
                }
            }
            else
            {
                ModelState.AddModelError("CheckIn", "Both check-in and check-out dates are required.");
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingsExists(bookings.BookingID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["ApplUserID"] = new SelectList(_context.ApplUser, "Id", "Id", bookings.ApplUserID);
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomType", bookings.RoomID);
            return View(bookings);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var bookings = await _context.Bookings
                .Include(b => b.ApplUser)
                .Include(b => b.Room)
                .FirstOrDefaultAsync(m => m.BookingID == id);

            if (bookings == null)
                return NotFound();

            return View(bookings);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings != null)
            {
                _context.Bookings.Remove(bookings);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BookingsExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingID == id);
        }
    }
}
