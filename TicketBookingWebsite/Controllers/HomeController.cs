using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TicketBookingWebsite.Repositories;
using TicketBookingWebsite.ViewModels;

public class HomeController : Controller
{
    private readonly IWebsiteRepository repository;
    private const int PageSize = 2; // Change as needed

    public HomeController(IWebsiteRepository repo)
    {
        repository = repo;
    }

    public IActionResult Index(int page = 1)
    {
        var totalEvents = repository.Events.Count();

        var events = repository.Events
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
            }
        };

        return View(viewModel);
    }
}
