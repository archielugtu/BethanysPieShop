using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCart _shoppingCart;

        public OrderController(IOrderRepository orderRepository, IShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }

        //no attributes means it's a GET
        public IActionResult Checkout() //GET
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                //Shows warning to the user
                //ModelState is a side product of the Model Binding. When Model Binding is happening, then any errors that occur 
                //will appear in the ModelState. We can also add extra errors in ModelState like what we are doing here
                ModelState.AddModelError("", "Your cart is empty, add some pies first");
            }

            //IsValid will be true if Model Binding went okay and no extra errors were added in ModelState
            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete"); //Redirects to another action such as the CheckoutComplete controller method.
            }
            return View(order);
        }

        public IActionResult CheckoutComplete()
        {
            //Stores info in ViewBag which is accessible to Views/Order/CheckoutComplete.cshtml 
            ViewBag.CheckoutCompleteMessage = "Thanks for your order. You'll soon enjoy our delicious pies!";
            return View();
        }
    }
}
