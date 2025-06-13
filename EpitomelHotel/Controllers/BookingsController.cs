using System;
using System.Collections.Generic;
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

        // GET: Bookings
        public async Task<IActionResult> Index(string searchString, int? pageNumber, int pageSize = 5)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            IQueryable<Bookings> bookingsQuery = _context.Bookings
                .Include(b => b.ApplUser)
                .Include(b => b.Room);

            if (!User.IsInRole("Admin"))
            {
                bookingsQuery = bookingsQuery.Where(b => b.ApplUserID == userId);
            }

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                string lowerSearch = searchString.ToLower();
                bookingsQuery = bookingsQuery.Where(b =>
                    (b.ApplUser.Firstname != null && b.ApplUser.Firstname.ToLower().Contains(lowerSearch)) ||
                    (b.PaymentStatus != null && b.PaymentStatus.ToLower().Contains(lowerSearch)));
            }

            var paginatedBookings = await PaginatedList<Bookings>.CreateAsync(bookingsQuery.AsNoTracking(), pageNumber ?? 1, pageSize);

            ViewData["CurrentFilter"] = searchString;

            return View(paginatedBookings);
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .Include(b => b.ApplUser)
                .Include(b => b.Room)
                .FirstOrDefaultAsync(m => m.BookingID == id);

            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        
        // GET: Bookings/Create
        public IActionResult Create(DateTime? checkIn, DateTime? checkOut, int? roomId)
        {
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomType");
            ViewData["ApplUserID"] = new SelectList(_context.ApplUser, "Id", "Firstname");

            var model = new Bookings();

            if (checkIn.HasValue)
                model.CheckIn = checkIn.Value;
            if (checkOut.HasValue)
                model.CheckOut = checkOut.Value;
            if (roomId.HasValue)
                model.RoomID = roomId.Value;

            if (model.CheckIn != default && model.CheckOut != default && model.RoomID != 0)
            {
                int duration = (model.CheckOut - model.CheckIn).Days;
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
            bool isRoomAvailable = !_context.Bookings.Any(b =>
    b.RoomID == bookings.RoomID &&
    ((bookings.CheckIn >= b.CheckIn && bookings.CheckIn < b.CheckOut) ||
     (bookings.CheckOut > b.CheckIn && bookings.CheckOut <= b.CheckOut) ||
     (bookings.CheckIn <= b.CheckIn && bookings.CheckOut >= b.CheckOut))
);

            if (!isRoomAvailable)
            {
                ModelState.AddModelError("", "Room is already booked for the selected dates.");
            }

            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bookings.ApplUserID = userId; // assign current user id

            int duration = (bookings.CheckOut - bookings.CheckIn).Days;
            if (duration <= 0)
            {
                ModelState.AddModelError("CheckOut", "Check-out must be after check-in.");
            }
            else
            {
                const decimal dailyRate = 75m;
                bookings.TotalAmount = dailyRate * duration;
            }

            // Assign default PaymentStatus until user has paid
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
            else
            {
                var hasBookings = await _context.Bookings
                    .AnyAsync(b => b.ApplUserID == userId);

                if (hasBookings)
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction(nameof(Create));
            }
        }

        // GET: Bookings/Confirmation/5
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

        [AllowAnonymous]
        [HttpPost]
        public IActionResult StartCreate(DateTime checkIn, DateTime checkOut, int roomId)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Create), new { checkIn, checkOut, roomId });
            }

            // Store the values for use after login
            TempData["CheckIn"] = checkIn.ToString("o");
            TempData["CheckOut"] = checkOut.ToString("o");
            TempData["RoomID"] = roomId.ToString();

            // Redirect to login with return URL to ResumeCreate
            var returnUrl = Url.Action("ResumeCreate", "Bookings");
            return RedirectToPage("/Account/Login", new { area = "Identity", returnUrl });
        }

        [Authorize]
        public async Task<IActionResult> ResumeCreate()
        {
            if (TempData["CheckIn"] == null || TempData["CheckOut"] == null || TempData["RoomID"] == null)
                return RedirectToAction("Index", "Home");

            DateTime checkIn = DateTime.Parse(TempData["CheckIn"].ToString());
            DateTime checkOut = DateTime.Parse(TempData["CheckOut"].ToString());
            int roomId = int.Parse(TempData["RoomID"].ToString());

            // Auto-submit booking
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var booking = new Bookings
            {
                CheckIn = checkIn,
                CheckOut = checkOut,
                RoomID = roomId,
                ApplUserID = userId,
                PaymentStatus = "Pending"
            };

            int duration = (checkOut - checkIn).Days;
            if (duration > 0)
            {
                const decimal dailyRate = 75m;
                booking.TotalAmount = dailyRate * duration;
            }
            else
            {
                TempData["Error"] = "Invalid booking dates.";
                return RedirectToAction("Index", "Home");
            }

            bool isRoomAvailable = !_context.Bookings.Any(b =>
                b.RoomID == roomId &&
                ((checkIn >= b.CheckIn && checkIn < b.CheckOut) ||
                 (checkOut > b.CheckIn && checkOut <= b.CheckOut) ||
                 (checkIn <= b.CheckIn && checkOut >= b.CheckOut))
            );

            if (!isRoomAvailable)
            {
                TempData["Error"] = "Room is already booked for the selected dates.";
                return RedirectToAction("Index", "Home");
            }

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Confirmation), new { id = booking.BookingID });
        }


        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }
            ViewData["ApplUserID"] = new SelectList(_context.ApplUser, "Id", "Id", bookings.ApplUserID);
            return View(bookings);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingID,CheckIn,CheckOut,TotalAmount,PaymentStatus,ApplUserID")] Bookings bookings)
        {
            if (id != bookings.BookingID)
            {
                return NotFound();
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
            ViewData["ApplUserID"] = new SelectList(_context.ApplUser, "Id", "Id", bookings.ApplUserID);
            return View(bookings);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .Include(b => b.ApplUser)
                .Include(b => b.Room)
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings != null)
            {
                _context.Bookings.Remove(bookings);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingsExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingID == id);
        }
    }
}
