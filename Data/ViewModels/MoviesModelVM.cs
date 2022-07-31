using eTickets.Models.Data.@enum;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels
{
    public class MoviesModelVM
    {

        public int Id { get; set; }
        [Display(Description  = "Movie Name")]
        [Required(ErrorMessage = "Movie Name is required")]
        public string Name { get; set; }

        [Display(Description = "Movie Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Description = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Description = "Image Poster")]
        [Required(ErrorMessage = "Movie Image is required")]
        public string ImageURL { get; set; }

        [Display(Description = "Starting Date of the Movie")]
        [Required(ErrorMessage = "Movie StartDate cannot be empty.")]
        public String StartDate { get; set; }

        [Display(Description = "Ending Date of the Movie")]
        [Required(ErrorMessage = "Movie EndDate cannot be empty.")]
        public string EndDate { get; set; }

        [Display(Description = "Select a Movie Category")]
        [Required(ErrorMessage = "Movie Category cannot be empty.")]
        public MovieCategory MovieCategory { get; set; }

        [Display(Description = "Select Actor IDs")]
        [Required(ErrorMessage = "Actor IDs are requireds.")]
        public List<int> ActorIds { get; set; }

        [Display(Description = "Select Cinema ID")]
        [Required(ErrorMessage = "Cinema ID is requireds.")]
        public int CinemaId { get; set; }

        [Display(Description = "Select Producer ID")]
        [Required(ErrorMessage = "Producer ID is requireds.")]
        public int ProducerId { get; set; }
        
    }
}
