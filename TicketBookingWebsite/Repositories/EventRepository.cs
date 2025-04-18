using TicketBookingWebsite.Data;
using TicketBookingWebsite.Models;
using TicketBookingWebsite.Repositories.Interfaces;

namespace TicketBookingWebsite.Repositories
{
    public class EventRepository : IEventRepository
    {
        private WebsiteDbContext context;

        public EventRepository(WebsiteDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Event> Events => context.Events;

        public void CreateEvent(Event e)
        {
            context.Events.Add(e);
            context.SaveChanges();
        }

        public void UpdateEvent(Event e)
        {
            context.Events.Update(e);
            context.SaveChanges();
        }

        public void DeleteEvent(Event e)
        {
            context.Events.Remove(e);
            context.SaveChanges();
        }

        public Event? GetEventById(int id)
        {
            return context.Events.FirstOrDefault(e => e.Id == id);
        }
    }
}
