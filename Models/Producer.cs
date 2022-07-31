using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer : IEntityBase
    {
        [ Key ]
        public int Id { get; set; }
        [Display(Name ="FUll Name")]
        public string FullName { get; set; }
        [Display(Name = "PROFILE PICTURE")]
        public string? ProfilePictureUrl { get; set; }
        [Display(Name = "BIOGRAPHY")]
        public string Bio { get; set; }
        public IList<Movie> Movies { get; } = new List<Movie>();



    }
}
