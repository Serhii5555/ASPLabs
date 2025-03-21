using TicketBookingWebsite.Models;

namespace TicketBookingWebsite.Repositories
{
    public interface IWebsiteRepository
    {
        IQueryable<Event> Events { get; }
    }
}
