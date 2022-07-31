using eTickets.Models;
using eTickets.Models.Data.@enum;

namespace eTickets.Data.ViewModels
{
    public class NewMovieDropDownVM
    {
        public NewMovieDropDownVM()
        {
            Actors = new List<Actor>();
            Producers = new List<Producer>();
            Cinemas = new List<Cinema>();
            

        }
        public List<Actor> Actors { get; set; }
        public List<Producer> Producers { get; set; }
        public List<Cinema> Cinemas { get; set; }
        

    }
}
