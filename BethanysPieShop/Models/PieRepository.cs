using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
   public class PieRepository : IPieRepository
   {
      private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;

      /**
       * We're using constructor injector here passing in the BethanysPieShopDbContext.
       * We're able to do this since we injected this db context in Program.cs into the dependency injection container
       * Example of DEPENDENCY INJECTION
       */
      public PieRepository(BethanysPieShopDbContext bethanysPieShopDbContext)
      {
         _bethanysPieShopDbContext = bethanysPieShopDbContext;
      }

      public IEnumerable<Pie> AllPies
      {
         get
         {
            return _bethanysPieShopDbContext.Pies.Include(c => c.Category);
         }
      }

      public IEnumerable<Pie> PiesOfTheWeek
      {
         get
         {
            return _bethanysPieShopDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
         }
      }

      public Pie? GetPieById(int pieId)
      {
         return _bethanysPieShopDbContext.Pies.Include(c => c.Category).Where(p => p.PieId == pieId).FirstOrDefault();
      }
   }
}
