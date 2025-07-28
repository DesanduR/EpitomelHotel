using EpitomelHotel.Areas.Identity.Data;
using EpitomelHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EpitomelHotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EpitomelHotelDbContext _context;

        public HomeController(ILogger<HomeController> logger, EpitomelHotelDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings
                .Select(b => new { b.CheckIn, b.CheckOut })
                .ToListAsync();

            var unavailableDates = bookings
                .SelectMany(b => Enumerable.Range(0, (b.CheckOut - b.CheckIn).Days)
                                           .Select(offset => b.CheckIn.AddDays(offset).ToString("yyyy-MM-dd")))
                .Distinct()
                .ToList();

            ViewBag.UnavailableDates = unavailableDates;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
