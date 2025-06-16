using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EpitomelHotel.Areas.Identity.Data;
using EpitomelHotel.Models;

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
            var epitomelHotelDbContext = _context.BookingService.Include(b => b.Room).Include(b => b.Service);
            return View(await epitomelHotelDbContext.ToListAsync());
        }

        // GET: BookingServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingService = await _context.BookingService
                .Include(b => b.Room)
                .Include(b => b.Service)
                .FirstOrDefaultAsync(m => m.BookingServiceID == id);
            if (bookingService == null)
            {
                return NotFound();
            }

            return View(bookingService);
        }

        // GET: BookingServices/Create
        public IActionResult Create()
        {
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomType");
            ViewData["ServiceID"] = new SelectList(_context.Services, "ServiceID", "ServiceName");
            return View();
        }

        // POST: BookingServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingServiceID,ServiceCost,RoomID,ServiceID")] BookingService bookingService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomType", bookingService.RoomID);
            ViewData["ServiceID"] = new SelectList(_context.Services, "ServiceID", "ServiceName", bookingService.ServiceID);
            return View(bookingService);
        }

        // GET: BookingServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingService = await _context.BookingService.FindAsync(id);
            if (bookingService == null)
            {
                return NotFound();
            }
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomType", bookingService.RoomID);
            ViewData["ServiceID"] = new SelectList(_context.Services, "ServiceID", "ServiceName", bookingService.ServiceID);
            return View(bookingService);
        }

        // POST: BookingServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingServiceID,ServiceCost,RoomID,ServiceID")] BookingService bookingService)
        {
            if (id != bookingService.BookingServiceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingServiceExists(bookingService.BookingServiceID))
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
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomType", bookingService.RoomID);
            ViewData["ServiceID"] = new SelectList(_context.Services, "ServiceID", "ServiceName", bookingService.ServiceID);
            return View(bookingService);
        }

        // GET: BookingServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingService = await _context.BookingService
                .Include(b => b.Room)
                .Include(b => b.Service)
                .FirstOrDefaultAsync(m => m.BookingServiceID == id);
            if (bookingService == null)
            {
                return NotFound();
            }

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
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingServiceExists(int id)
        {
            return _context.BookingService.Any(e => e.BookingServiceID == id);
        }
    }
}
