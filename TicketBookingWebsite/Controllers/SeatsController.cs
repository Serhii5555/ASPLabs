using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TicketBookingWebsite.Models;

namespace TicketBookingWebsite.Controllers
{
    [Authorize]
    public class SeatsController : Controller
    {
        private static List<Seat> Seats = new List<Seat>
        {
            new Seat { Id = "A1", IsAvailable = true },
            new Seat { Id = "A2", IsAvailable = true },
            new Seat { Id = "A3", IsAvailable = true },
            new Seat { Id = "B1", IsAvailable = true },
            new Seat { Id = "B2", IsAvailable = true }
        };

        public IActionResult Index()
        {
            return View(Seats);
        }

        [HttpPost]
        public IActionResult Book(string id)
        {
            var seat = Seats.FirstOrDefault(s => s.Id == id);
            if (seat != null && seat.IsAvailable)
            {
                seat.IsAvailable = false;
            }

            return RedirectToAction("Index");
        }
    }

}
