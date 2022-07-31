using eTickets.Data.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace eTickets.Data.ViewComponents
{
    [ViewComponent]
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart shoppingCart;
        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            this.shoppingCart = shoppingCart;
        }
        public Task<IViewComponentResult> InvokeAsync()
        {
            var amount = shoppingCart.getShopingCartItems().Count();
            return Task.FromResult < IViewComponentResult > (View(amount));


        }

    }
}
