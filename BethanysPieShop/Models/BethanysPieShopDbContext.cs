using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    public class BethanysPieShopDbContext : DbContext
    {
        public BethanysPieShopDbContext(DbContextOptions<BethanysPieShopDbContext> options): base(options) { }

        // DbSet lets EF Core know that you want to match the entities you have specified. This allows you to load data to these DbSets and you can also perform operations on the data in these DbSets.
        public DbSet<Category> Categories { get; set; }
        public DbSet<Pie> Pies { get; set; } 
    }
}
