using Microsoft.AspNetCore.Mvc;
using TicketBookingWebsite.Data;
using System.Linq;

namespace TicketBookingWebsite.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly WebsiteDbContext _context;

        public NavigationMenuViewComponent(WebsiteDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _context.Events
                .Select(e => e.Location) 
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            return View(categories);
        }
    }
}
