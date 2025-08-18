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
    public class RoomsController : Controller
    {
        private readonly EpitomelHotelDbContext _context;

        public RoomsController(EpitomelHotelDbContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            string roomType,       // filter param
            string priceRange,     // filter param
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SelectedRoomType"] = roomType;
            ViewData["SelectedPriceRange"] = priceRange;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var rooms = _context.Rooms.Include(r => r.Status).AsQueryable();

            // ✅ Search by RoomType or RoomNumber
            if (!string.IsNullOrEmpty(searchString))
            {
                rooms = rooms.Where(r => r.RoomType.Contains(searchString)
                                      || r.RoomNumber.Contains(searchString));
            }

            // ✅ Filter by RoomType
            if (!string.IsNullOrEmpty(roomType))
            {
                rooms = rooms.Where(r => r.RoomType == roomType);
            }

            // ✅ Filter by Price Range
            if (!string.IsNullOrEmpty(priceRange))
            {
                switch (priceRange)
                {
                    case "Under 100":
                        rooms = rooms.Where(r => r.Price < 100);
                        break;
                    case "100-200":
                        rooms = rooms.Where(r => r.Price >= 100 && r.Price <= 200);
                        break;
                    case "200-300":
                        rooms = rooms.Where(r => r.Price > 200 && r.Price <= 300);
                        break;
                    case "300+":
                        rooms = rooms.Where(r => r.Price > 300);
                        break;
                }
            }

            // ✅ Sorting example (by price or type)
            rooms = sortOrder switch
            {
                "price_asc" => rooms.OrderBy(r => r.Price),
                "price_desc" => rooms.OrderByDescending(r => r.Price),
                "type_asc" => rooms.OrderBy(r => r.RoomType),
                "type_desc" => rooms.OrderByDescending(r => r.RoomType),
                _ => rooms.OrderBy(r => r.RoomID), // default
            };

            // ✅ Pagination
            int pageSize = 5;
            var resultList = await PaginatedList<Rooms>.CreateAsync(
                rooms.AsNoTracking(), pageNumber ?? 1, pageSize);

            // ✅ Show "no results" flag if filtered but empty
            if (!resultList.Any() && (!string.IsNullOrEmpty(searchString)
                                    || !string.IsNullOrEmpty(roomType)
                                    || !string.IsNullOrEmpty(priceRange)))
            {
                ViewBag.NoResults = true;
            }

            return View(resultList);
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var rooms = await _context.Rooms
                .Include(r => r.Status)
                .FirstOrDefaultAsync(m => m.RoomID == id);

            if (rooms == null) return NotFound();

            return View(rooms);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusName");
            return View();
        }

        // POST: Rooms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomID,RoomType,RoomNumber,Price,Capacity,StatusID")] Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rooms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusName", rooms.StatusID);
            return View(rooms);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var rooms = await _context.Rooms.FindAsync(id);
            if (rooms == null) return NotFound();

            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusName", rooms.StatusID);
            return View(rooms);
        }

        // POST: Rooms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomID,RoomType,RoomNumber,Price,Capacity,StatusID")] Rooms rooms)
        {
            if (id != rooms.RoomID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rooms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomsExists(rooms.RoomID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusName", rooms.StatusID);
            return View(rooms);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var rooms = await _context.Rooms
                .Include(r => r.Status)
                .FirstOrDefaultAsync(m => m.RoomID == id);

            if (rooms == null) return NotFound();

            return View(rooms);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rooms = await _context.Rooms.FindAsync(id);
            if (rooms != null)
            {
                _context.Rooms.Remove(rooms);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool RoomsExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomID == id);
        }
    }
}
