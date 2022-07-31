using eTickets.Data.Base;
using eTickets.Models.Data.@enum;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema : IEntityBase
    {

        [ Key ]
        public int Id { get; set; }
        [Display(Name ="Name")]
        public string CinemaName { get; set; }
        [Display(Name = "Image")]
        public string CinemaImageUrl { get; set; }
        [Display(Name = "Description")]
        public string CinemaDescription { get; set; }
        public IList<Movie> Movies { get; } = new List<Movie>();


    }
}
