using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Data;
using System;
using System.Linq;

namespace TicketBookingWebsite.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<WebsiteDbContext>();

                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if (!context.Events.Any())
                {
                    context.Events.AddRange(
                        new Event
                        {
                            Name = "Rock Concert",
                            Description = "Live rock music event.",
                            Date = new DateTime(2025, 5, 20, 19, 0, 0),
                            Location = "Stadium A",
                            Price = 49.99m,
                            AvailableTickets = 200
                        },
                        new Event
                        {
                            Name = "Tech Conference",
                            Description = "Annual technology conference.",
                            Date = new DateTime(2025, 6, 10, 9, 0, 0),
                            Location = "Convention Center",
                            Price = 99.99m,
                            AvailableTickets = 500
                        },
                        new Event
                        {
                            Name = "Stand-up Comedy Show",
                            Description = "A night of laughter with top comedians.",
                            Date = new DateTime(2025, 4, 15, 20, 0, 0),
                            Location = "Comedy Club",
                            Price = 29.99m,
                            AvailableTickets = 150
                        },
                        new Event
                        {
                            Name = "Art Exhibition",
                            Description = "Showcasing modern art masterpieces.",
                            Date = new DateTime(2025, 7, 5, 10, 0, 0),
                            Location = "City Art Gallery",
                            Price = 15.00m,
                            AvailableTickets = 300
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
