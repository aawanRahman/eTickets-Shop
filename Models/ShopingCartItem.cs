using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class ShopingCartItem
    {
        [Key]
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public int amount { get; set; }
        public string ShopingCardId { get; set; }
    }
}
