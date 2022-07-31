using eTickets.Models;
using eTickets.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext appDbContext;
        public OrdersService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async  Task<List<Order>> GetOrdersByUserIdAndRoleAsync( string userId, string userRole)
        {
            var orders = await appDbContext.Orders.Include(n => n.User).Include(n => n.orderItems).ThenInclude(n => n.movie).ToListAsync();
            if(userRole != "Admin")
            {
                orders =  orders.Where(n => n.userId == userId).ToList();
            }
            return orders;
        }

        public async  Task StoreOderByUserId(List<ShopingCartItem> shoppingCartItem, string UserId, string userEmailAddress)
        {
            var order = new Order()
            {
               userId = UserId,

                Email = userEmailAddress
            };
            await appDbContext.AddAsync(order);
            await appDbContext.SaveChangesAsync();
            foreach (var item in shoppingCartItem)
            {
                var orderItem = new OrderItem()
                {
                    Price = item.Movie.Price,
                    amount = item.amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id
                };
               await appDbContext.OrderItems.AddAsync(orderItem);
              
            }
            
           await appDbContext.SaveChangesAsync();

        }

    }
}
