using EpitomelHotel.Areas.Identity.Data;
using EpitomelHotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpitomelHotel.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly EpitomelHotelDbContext _context;

        public PaymentsController(EpitomelHotelDbContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var epitomelHotelDbContext = _context.Payments.Include(p => p.Booking);
            return View(await epitomelHotelDbContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payments = await _context.Payments
                .Include(p => p.Booking)
                .FirstOrDefaultAsync(m => m.PaymentID == id);
            if (payments == null)
            {
                return NotFound();
            }

            return View(payments);
        }

        [Authorize]
        public async Task<IActionResult> PayWithStripe(int bookingId)
        {
            // gets booking from database
            var booking = await _context.Bookings
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.BookingID == bookingId);

            if (booking == null)
                return NotFound();
            // this gets the current site domain that the web app is using
            // this is required as stripe needs full absoulte URLs
            var domain = $"{Request.Scheme}://{Request.Host}";

            var options = new SessionCreateOptions
            {
                // only card payments allowed
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                // shows what the user is paying for
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(booking.TotalAmount * 100), // in cents
                        Currency = "nzd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"{booking.Room.RoomType} Room Booking"
                        }
                    },
                    Quantity = 1
                }
            },
                // one time payment 
                // if the payment succeeds they are led to paymentsuccess and if the user cancels the payment they are led to paymentcancel 
                Mode = "payment",
                SuccessUrl = domain + Url.Action("PaymentSuccess", "Payments", new { bookingId }),
                CancelUrl = domain + Url.Action("PaymentCancel", "Payments", new { bookingId }),
            };
            // this creates a checkout session displying information of what the user is paying for
            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }

        public async Task<IActionResult> PaymentSuccess(int bookingId)
        {
            var booking = await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.ApplUser)
                .FirstOrDefaultAsync(b => b.BookingID == bookingId);

            if (booking == null)
                return NotFound();

            // Update booking status
            booking.PaymentStatus = "Paid";

            // Save a payment record
            var payment = new Payments
            {
                BookingID = booking.BookingID,
                Price = booking.TotalAmount,
                PaymentDate = DateTime.Now,
                PaymentMethod = "Stripe",
                TotalAmount = booking.TotalAmount
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            TempData["PaymentSuccess"] = "Payment was successful!";
            return RedirectToAction("Confirmation", "Bookings", new { id = bookingId });
        }

        public IActionResult PaymentCancel(int bookingId)
        {
            TempData["PaymentError"] = "Payment was cancelled.";
            return RedirectToAction("Confirmation", "Bookings", new { id = bookingId });
        }



        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["BookingID"] = new SelectList(_context.Bookings, "BookingID", "ApplUserID");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentID,Price,PaymentDate,PaymentMethod,TotalAmount,BookingID")] Payments payments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingID"] = new SelectList(_context.Bookings, "BookingID", "ApplUserID", payments.BookingID);
            return View(payments);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payments = await _context.Payments.FindAsync(id);
            if (payments == null)
            {
                return NotFound();
            }
            ViewData["BookingID"] = new SelectList(_context.Bookings, "BookingID", "ApplUserID", payments.BookingID);
            return View(payments);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentID,Price,PaymentDate,PaymentMethod,TotalAmount,BookingID")] Payments payments)
        {
            if (id != payments.PaymentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentsExists(payments.PaymentID))
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
            ViewData["BookingID"] = new SelectList(_context.Bookings, "BookingID", "ApplUserID", payments.BookingID);
            return View(payments);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payments = await _context.Payments
                .Include(p => p.Booking)
                .FirstOrDefaultAsync(m => m.PaymentID == id);
            if (payments == null)
            {
                return NotFound();
            }

            return View(payments);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payments = await _context.Payments.FindAsync(id);
            if (payments != null)
            {
                _context.Payments.Remove(payments);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentsExists(int id)
        {
            return _context.Payments.Any(e => e.PaymentID == id);
        }
    }
}
