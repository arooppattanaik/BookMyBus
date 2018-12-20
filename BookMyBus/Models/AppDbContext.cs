using Microsoft.EntityFrameworkCore;
using BookMyBus.Models;

namespace BookMyBus.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
        {

        }
        public DbSet<BookMyBus.Models.BusRoute> BusRoutes {get; set;}
         public DbSet<BookMyBus.Models.Ticket> Ticket {get; set;}
          public DbSet<BookMyBus.Models.Customer> Customer {get; set;}
    }
}