using TicketBookingWebsite.Models;

namespace TicketBookingWebsite.Repositories.Interfaces
{
    public interface IWebsiteRepository
    {
        IQueryable<Event> Events { get; }
    }
}
