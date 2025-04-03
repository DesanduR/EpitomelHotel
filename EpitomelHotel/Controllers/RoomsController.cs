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
        public async Task<IActionResult> Index()
        {
            var epitomelHotelDbContext = _context.Rooms.Include(r => r.Staff).Include(r => r.Status);
            return View(await epitomelHotelDbContext.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rooms = await _context.Rooms
                .Include(r => r.Staff)
                .Include(r => r.Status)
                .FirstOrDefaultAsync(m => m.RoomID == id);
            if (rooms == null)
            {
                return NotFound();
            }

            return View(rooms);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "StaffID");
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusID");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomID,RoomType,Price,Capacity,StatusID,StaffID,BookingID")] Rooms rooms)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(rooms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "StaffID", rooms.StaffID);
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusID", rooms.StatusID);
            return View(rooms);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rooms = await _context.Rooms.FindAsync(id);
            if (rooms == null)
            {
                return NotFound();
            }
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "StaffID", rooms.StaffID);
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusID", rooms.StatusID);
            return View(rooms);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomID,RoomType,Price,Capacity,StatusID,StaffID,BookingID")] Rooms rooms)
        {
            if (id != rooms.RoomID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(rooms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomsExists(rooms.RoomID))
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
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "StaffID", rooms.StaffID);
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusID", rooms.StatusID);
            return View(rooms);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rooms = await _context.Rooms
                .Include(r => r.Staff)
                .Include(r => r.Status)
                .FirstOrDefaultAsync(m => m.RoomID == id);
            if (rooms == null)
            {
                return NotFound();
            }

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
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomsExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomID == id);
        }
    }
}
