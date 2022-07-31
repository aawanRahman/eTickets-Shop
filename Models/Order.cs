namespace eTickets.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string userId { get; set; }
        public ApplicationUser User { get; set; }

        public List<OrderItem> orderItems { get; set; }
    }
}
