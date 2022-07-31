using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOderByUserId(List<ShopingCartItem> shoppingCartItem, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync( string userId, string userRole);
    }
}
