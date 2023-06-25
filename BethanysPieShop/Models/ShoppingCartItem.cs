namespace BethanysPieShop.Models
{
    /*
     * This model represents an item in a shopping cart.
     * ShoppingCartId represents the shopping cart where the shopping cart item is
     */
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public Pie Pie { get; set; } = default!;
        public int Amount { get; set; }
        public string? ShoppingCartId { get; set; }
    }
}
