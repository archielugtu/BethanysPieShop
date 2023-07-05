using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BethanysPieShop.Pages
{
   public class CheckoutPageModel : PageModel
   {

      private readonly IOrderRepository _orderRepository;
      private readonly IShoppingCart _shoppingCart;

      //This puts the values in the Order model. This activates the Model Binding
      [BindProperty]
      public Order Order { get; set; }

      public CheckoutPageModel(IOrderRepository orderRepository, IShoppingCart shoppingCart)
      {
         _orderRepository = orderRepository;
         _shoppingCart = shoppingCart;
      }
      // GET Method
      public void OnGet()
      {
      }

      // POST Method which gets triggered automatically when a post is sent to this page
      public IActionResult OnPost()
      {
         //If the model (i.e., Order model class) is invalid which means there are binding errors then IsValid will be false
         if (!ModelState.IsValid)
         {
            return Page();
         }

         _shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();

         if (_shoppingCart.ShoppingCartItems.Count == 0)
         {
            //Add an error to the ModelState
            ModelState.AddModelError("", "Your cart is empty, add some pies first");
         }

         if (ModelState.IsValid)
         {
            _orderRepository.CreateOrder(Order);
            _shoppingCart.ClearCart();
            return RedirectToPage("CheckoutCompletePage");
         }
         return Page();
      }
   }
}
