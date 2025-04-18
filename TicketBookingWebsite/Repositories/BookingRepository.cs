using TicketBookingWebsite.Data;
using TicketBookingWebsite.Models;
using Microsoft.EntityFrameworkCore;
using TicketBookingWebsite.Repositories.Interfaces;

namespace TicketBookingWebsite.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private WebsiteDbContext context;

        public BookingRepository(WebsiteDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Booking> Bookings => context.Bookings.Include(b => b.Event);

        public void CreateBooking(Booking b)
        {
            context.Bookings.Add(b);
            context.SaveChanges();
        }

        public void UpdateBooking(Booking b)
        {
            context.Bookings.Update(b);
            context.SaveChanges();
        }

        public void DeleteBooking(Booking b)
        {
            context.Bookings.Remove(b);
            context.SaveChanges();
        }

        public Booking? GetBookingById(int id)
        {
            return context.Bookings.Include(b => b.Event).FirstOrDefault(b => b.Id == id);
        }
    }
}
