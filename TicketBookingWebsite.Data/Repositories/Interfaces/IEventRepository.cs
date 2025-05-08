using TicketBookingWebsite.Models;

namespace TicketBookingWebsite.Repositories.Interfaces
{
    public interface IEventRepository
    {
        IQueryable<Event> Events { get; }

        void CreateEvent(Event e);
        void UpdateEvent(Event e);
        void DeleteEvent(Event e);
        Event? GetEventById(int id);
    }
}
