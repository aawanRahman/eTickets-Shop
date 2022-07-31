using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IMoviesService: IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieDataByIdAsync(int id);
        Task<NewMovieDropDownVM> GetMoviesDropDownList();
        Task AddNewMovie(MoviesModelVM newMovie);
        Task UpdateMovie(int id, MoviesModelVM newMovie);
    }
}
