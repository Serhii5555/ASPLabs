using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketBookingWebsite.Models;
using TicketBookingWebsite.Repositories.Interfaces;

namespace TicketBookingWebsite.Controllers
{
    [Authorize(Roles= "Admin")]
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IEventRepository _eventRepo;

        public BookingController(IBookingRepository bookingRepo, IEventRepository eventRepo)
        {
            _bookingRepo = bookingRepo;
            _eventRepo = eventRepo;
        }

        public IActionResult Index()
        {
            return View(_bookingRepo.Bookings.ToList());
        }

        public IActionResult Details(int id)
        {
            var booking = _bookingRepo.GetBookingById(id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        public IActionResult Create()
        {
            ViewBag.Events = new SelectList(_eventRepo.Events.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _bookingRepo.CreateBooking(booking);
                return RedirectToAction("Index");
            }
            ViewBag.Events = new SelectList(_eventRepo.Events.ToList(), "Id", "Name", booking.EventId);
            return View(booking);
        }

        public IActionResult Edit(int id)
        {
            var booking = _bookingRepo.GetBookingById(id);
            if (booking == null) return NotFound();

            ViewBag.Events = new SelectList(_eventRepo.Events.ToList(), "Id", "Name", booking.EventId);
            return View(booking);
        }

        [HttpPost]
        public IActionResult Edit(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _bookingRepo.UpdateBooking(booking);
                return RedirectToAction("Index");
            }
            ViewBag.Events = new SelectList(_eventRepo.Events.ToList(), "Id", "Name", booking.EventId);
            return View(booking);
        }

        public IActionResult Delete(int id)
        {
            var booking = _bookingRepo.GetBookingById(id);
            if (booking == null) return NotFound();
            return View(booking); // confirmation
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var booking = _bookingRepo.GetBookingById(id);
            if (booking == null) return NotFound();

            _bookingRepo.DeleteBooking(booking);
            return RedirectToAction("Index");
        }
    }
}
