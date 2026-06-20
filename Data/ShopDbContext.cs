using Microsoft.EntityFrameworkCore;
using MyMvcApp.Models;

namespace MyMvcApp.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
            
        }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Item> Items { get; set; }
    }
    
}