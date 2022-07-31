using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using eTickets.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie> , IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewMovie(MoviesModelVM newMovie)
        {
            var movie = new Movie
            {
                Name = newMovie.Name,
                Description = newMovie.Description,
                Price = newMovie.Price,
                ImageURL = newMovie.ImageURL,
                StartDate = newMovie.StartDate.ToString(),
                EndDate = newMovie.EndDate.ToString(),
                MovieCategory = newMovie.MovieCategory,
                CinemaId = newMovie.CinemaId,
                ProducerId = newMovie.ProducerId
            };
            _context.Add(movie);
            await _context.SaveChangesAsync();

            foreach(int actorId in newMovie.ActorIds)
            {
                var actor_movies = new Actor_Movie
                {
                    ActorId = actorId,
                    MovieId = movie.Id
                };
                _context.Add(actor_movies);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieDataByIdAsync(int id)
        {
            var data = await _context.Movies
                .Include(c=>c.Cinema)
                .Include(p=>p.Producer)
                .Include(am => am.Actors_Movies)
                .ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n=> n.Id == id);
            return  data;
        }

        public async Task<NewMovieDropDownVM> GetMoviesDropDownList()
        {
            var moviesDropDownValues =  new NewMovieDropDownVM()
            {
                Actors = await  _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.CinemaName).ToListAsync(),
               
            };
            return  moviesDropDownValues;

        }

        public async Task UpdateMovie(int id, MoviesModelVM newMovie)
        {
           var movie = await _context.Movies.FirstOrDefaultAsync(c => c.Id == id);

            if(movie != null)
            {
                movie.Id = id;
                movie.Name = newMovie.Name;
                movie.Description = newMovie.Description;
                movie.Price = newMovie.Price;
                movie.ImageURL = newMovie.ImageURL;
                movie.StartDate = newMovie.StartDate.ToString();
                movie.EndDate = newMovie.EndDate.ToString();
                movie.MovieCategory = newMovie.MovieCategory;
                movie.CinemaId = newMovie.CinemaId;
                movie.ProducerId = newMovie.ProducerId;

            }
            await _context.SaveChangesAsync();
            
            var removeRowList = _context.Actors_Movies.Where( n=> n.MovieId  == movie.Id);
            _context.Actors_Movies.RemoveRange(removeRowList);
            await _context.SaveChangesAsync();

            foreach (int actorId in newMovie.ActorIds)
            {
                var actor_movies = new Actor_Movie
                {
                    ActorId = actorId,
                    MovieId = movie.Id
                };
                _context.Add(actor_movies);
            }
            await _context.SaveChangesAsync();

        }
    }
}
