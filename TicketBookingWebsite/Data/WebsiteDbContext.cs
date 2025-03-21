using Microsoft.EntityFrameworkCore;
using TicketBookingWebsite.Models;

namespace SportsStore.Data
{
    public class WebsiteDbContext : DbContext
    {
        public WebsiteDbContext(DbContextOptions<WebsiteDbContext> options) : base(options) { }

        public DbSet<Event> Events => Set<Event>();
    }
}
