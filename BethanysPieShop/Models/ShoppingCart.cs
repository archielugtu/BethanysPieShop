using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;

        public string? ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

        private ShoppingCart(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext;
        }

        /**
         * This method gets invoked for each oncoming request. 
         * This is done by calling it from Program.cs. 
         * It checks if the request has a CartId stored in the session, if not then it will generate a new one
         * And returns a new instance of the shopping cart with the CartId
         */
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            // ? - evaluates the first operand and if that's null then the result will be null, this prevent an exception being thrown
            // To use the IHttpContextAccessor, you need to call the AddHttpContextAccessor in Program.cs
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session; 

            BethanysPieShopDbContext context = services.GetService<BethanysPieShopDbContext>() ?? throw new Exception("Error initializing");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        /**
         * Adds a shopping a cart item to the shopping cart
         */
        public void AddToCart(Pie pie)
        {
            // check db if there is a pie in the following shopping cart id
            var shoppingCartItem =
                    _bethanysPieShopDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1
                };

                _bethanysPieShopDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _bethanysPieShopDbContext.SaveChanges();
        }

        /**
         * Removes a shopping a cart item from the shopping cart
         */
        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem =
                    _bethanysPieShopDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _bethanysPieShopDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _bethanysPieShopDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            // if ShoppingCartItems is null then assign the value to it from the database using the ShoppingCartId
            return ShoppingCartItems ??=
                       _bethanysPieShopDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Pie)
                           .ToList();
        }

        /*
         * Clears the shopping cart with the shopping cart id
         */
        public void ClearCart()
        {
            var cartItems = _bethanysPieShopDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _bethanysPieShopDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _bethanysPieShopDbContext.SaveChanges();
        }

        /*
         * Calculates the sum of all the items in the shopping cart
         */ 
        public decimal GetShoppingCartTotal()
        {
            var total = _bethanysPieShopDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Pie.Price * c.Amount).Sum();
            return total;
        }
    }
}
