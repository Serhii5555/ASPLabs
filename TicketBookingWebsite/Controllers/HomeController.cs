using Microsoft.AspNetCore.Mvc;
using TicketBookingWebsite.Models;
using TicketBookingWebsite.Repositories;
using TicketBookingWebsite.ViewModels;

public class HomeController : Controller
{
    private readonly IWebsiteRepository repository;
    private const int PageSize = 6;

    public HomeController(IWebsiteRepository repo)
    {
        repository = repo;
    }

    public IActionResult Index(string? category, int page = 1)
    {
        var filteredEvents = string.IsNullOrEmpty(category)
            ? repository.Events
            : repository.Events.Where(e => e.Location == category);

        var totalEvents = filteredEvents.Count();

        var events = filteredEvents
            .OrderBy(e => e.Date)
            .Skip((page - 1) * PageSize)
            .Take(PageSize);

        var viewModel = new EventListViewModel
        {
            Events = events,
            PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                TotalItems = totalEvents,
                ItemsPerPage = PageSize
            },
            CurrentCategory = category
        };

        return View(viewModel);
    }

    public IActionResult AddToCart(int eventId)
    {
        var selectedEvent = repository.Events.FirstOrDefault(e => e.Id == eventId);
        if (selectedEvent != null)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Event>>("Cart") ?? new List<Event>();
            cart.Add(selectedEvent);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
        }

        return RedirectToAction("Index");
    }

    public IActionResult Cart()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<Event>>("Cart");
        var viewModel = new CartViewModel
        {
            Events = cart,
            TotalPrice = cart?.Sum(e => e.Price) ?? 0
        };

        return View(viewModel);
    }

    public IActionResult ClearCart()
    {
        HttpContext.Session.Remove("Cart");
        return RedirectToAction("Cart");
    }
}
