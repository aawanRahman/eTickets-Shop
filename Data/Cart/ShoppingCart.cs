using eTickets.Models;
using eTickets.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Cart
{
    public class ShoppingCart
    {
        private readonly AppDbContext _context;
        public ShoppingCart(AppDbContext context) 
        {
            _context = context;
        }

        public string ShoppingCartId { get; set; }

        public List<ShopingCartItem> ShopingCartItems { get; set; }

        // shopping cart service.........
        public static ShoppingCart GetShoppingCart(IServiceProvider serviceProvider)
        {
            ISession session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = serviceProvider.GetService<AppDbContext>();
            string CartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", CartId);
            return new ShoppingCart(context) {
                ShoppingCartId = CartId };
        }


        public List<ShopingCartItem> getShopingCartItems()
        {
            return ShopingCartItems ?? (ShopingCartItems = _context.shopingCartItems.Where(n=> n.ShopingCardId == ShoppingCartId).Include(n=>n.Movie).ToList()); 
        }

        public double getShoppingCartTotal()
        {

            return _context.shopingCartItems.Where(n=>n.ShopingCardId == ShoppingCartId).Select(n => n.Movie.Price * n.amount).Sum();
        }

        public void AddItemToShoppingCart(Movie movie)
        {

            var shoppingCartItem = _context.shopingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShopingCardId == ShoppingCartId);
            if(shoppingCartItem != null)
            {
                shoppingCartItem.amount = shoppingCartItem.amount + 1;
                
            }
            else
            {
                shoppingCartItem = new ShopingCartItem
                {
                    ShopingCardId = ShoppingCartId,
                    Movie = movie,
                    amount = 1
                };
                _context.shopingCartItems.Add(shoppingCartItem);
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromShoppingCart(Movie movie)
        {
            var shoppingCartItem = _context.shopingCartItems.Where(n => n.Movie.Id == movie.Id && n.ShopingCardId == ShoppingCartId).FirstOrDefault();
            if (shoppingCartItem != null)
            {
                if(shoppingCartItem.amount>1)  shoppingCartItem.amount = shoppingCartItem.amount -1;
                else
                {
                    _context.shopingCartItems.Remove(shoppingCartItem);
                }

            }
      
            _context.SaveChanges();

        }

        public async Task ClearShoppingCartAsync()
        {
            var ShoppingCartItems = await _context.shopingCartItems.Where(n=> n.ShopingCardId == ShoppingCartId).ToListAsync();
            _context.shopingCartItems.RemoveRange(ShoppingCartItems);
           await _context.SaveChangesAsync();
        }



    }
}
