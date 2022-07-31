using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eTickets.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService ordersService;
        public OrdersController(IMoviesService moviesService, ShoppingCart _shoppingCart, IOrdersService ordersService)
        {
            _moviesService = moviesService;
            this._shoppingCart = _shoppingCart;
            this.ordersService = ordersService;
        }
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = await ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }
        public IActionResult ShoppingCart()
        {
            var shoppingCartItems = _shoppingCart.getShopingCartItems();
            _shoppingCart.ShopingCartItems = shoppingCartItems;
            var shoppingCartVm = new ShoppingCartVM()
            {
                shoppingCart = _shoppingCart,
                shoppingCartTotal = _shoppingCart.getShoppingCartTotal(),

            };
            return View(shoppingCartVm);
        }
        public async Task<IActionResult> AddToShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieDataByIdAsync(id);
            if(item != null)   _shoppingCart.AddItemToShoppingCart(item);
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RemoveFromShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieDataByIdAsync(id);
            if (item != null) _shoppingCart.RemoveItemFromShoppingCart(item);
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrders()
        {

            var shoppingCartItems = _shoppingCart.getShopingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);
            await ordersService.StoreOderByUserId(shoppingCartItems, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();
            return View("OrdersCompleted");

        }
    }
}
