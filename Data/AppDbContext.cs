using Microsoft.EntityFrameworkCore;
using OnlineStoreApi.Models;

namespace OnlineStoreApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}