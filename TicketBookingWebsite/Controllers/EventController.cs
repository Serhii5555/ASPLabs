using Microsoft.AspNetCore.Mvc;
using TicketBookingWebsite.Models;
using TicketBookingWebsite.Repositories.Interfaces;

namespace TicketBookingWebsite.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository _repository;

        public EventController(IEventRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.Events.ToList());
        }

        public IActionResult Details(int id)
        {
            var ev = _repository.GetEventById(id);
            if (ev == null) return NotFound();
            return View(ev);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Event ev)
        {
            if (ModelState.IsValid)
            {
                _repository.CreateEvent(ev);
                return RedirectToAction("Index");
            }
            return View(ev);
        }

        public IActionResult Edit(int id)
        {
            var ev = _repository.GetEventById(id);
            if (ev == null) return NotFound();
            return View(ev);
        }

        [HttpPost]
        public IActionResult Edit(Event ev)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateEvent(ev);
                return RedirectToAction("Index");
            }
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(ev);
        }

        public IActionResult Delete(int id)
        {
            var ev = _repository.GetEventById(id);
            if (ev == null) return NotFound();
            return View(ev); 
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var ev = _repository.GetEventById(id);
            if (ev == null) return NotFound();

            _repository.DeleteEvent(ev);
            return RedirectToAction("Index");
        }
    }
}
