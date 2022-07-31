namespace eTickets.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int amount { get; set; }
        public int MovieId { get; set; }

        public Movie movie { get; set; }

        public int OrderId { get; set; }

        public Order order { get; set; }
    }
}
