using Microsoft.EntityFrameworkCore;
using TicketBookingWebsite.Models;

namespace TicketBookingWebsite.Data
{
    public class WebsiteDbContext : DbContext
    {
        public WebsiteDbContext(DbContextOptions<WebsiteDbContext> options) : base(options) { }

        public DbSet<Event> Events => Set<Event>();
    }
}
