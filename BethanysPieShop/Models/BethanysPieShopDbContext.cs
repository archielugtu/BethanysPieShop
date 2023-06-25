using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    /*
     * In order to start inhering from DbContext, you need to have the EntityFrameworkCore.SqlServer package which is specified in the BethanysPieShop cs project file.
     * A DbContext acts as the intermediary between your code and database.
     * Once you've added the DbSets that you want, you need to add a connection string to the appsettings.json, to let your code know about the databases.
     * NOTE: appsettings.json is a file that is read out automatically when Program.cs is executed.
     * In appsettings.json - ConnectionStrings is a known value in the asp.net, and then then BethanysPieShopDbContextConnection will be the name of your connection string.
     */
    public class BethanysPieShopDbContext : DbContext
    {
        public BethanysPieShopDbContext(DbContextOptions<BethanysPieShopDbContext> options): base(options) { }

        // DbSet<Repository> lets EF Core know that you want to match the repository you have specified to be a database table.
        // This allows you to load data to these DbSets and you can also perform sql operations on the data stored in the table.
        // DbSet corresponds to a table in the database
        public DbSet<Category> Categories { get; set; }
        public DbSet<Pie> Pies { get; set; } 
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
