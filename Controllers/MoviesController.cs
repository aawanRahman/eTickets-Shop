using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using eTickets.Models.Data;
using eTickets.Models.Data.@enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService moviesService;
        public MoviesController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        public IActionResult Index()
        {
            var allActorsData =  moviesService.GetAllData(n=> n.Cinema);
            return  View(allActorsData);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            
            var allActorsData = moviesService.GetAllData(n => n.Cinema);
            if (!string.IsNullOrEmpty(searchString))
            {
                var data = allActorsData.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString) ).ToList();
                return View("Index", data);
            }
            return View("Index", allActorsData);
        }
        public async Task<IActionResult> Details(int id)
        {
            var data = await moviesService.GetMovieDataByIdAsync(id);
            return View(data);

        }

        public async Task<IActionResult> Create()
        {
            var moviesDropDownValues = await moviesService.GetMoviesDropDownList();
            ViewBag.Cinemas = new SelectList(moviesDropDownValues.Cinemas, "Id", "CinemaName");
            ViewBag.Actors = new SelectList(moviesDropDownValues.Actors, "Id", "FullName");
            ViewBag.Producers = new SelectList(moviesDropDownValues.Producers, "Id", "FullName");
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MoviesModelVM newMovie)
        {
            if(!ModelState.IsValid)
            {
                var moviesDropDownValues = await moviesService.GetMoviesDropDownList();
                ViewBag.Cinemas = new SelectList(moviesDropDownValues.Cinemas, "Id", "CinemaName");
                ViewBag.Actors = new SelectList(moviesDropDownValues.Actors, "Id", "FullName");
                ViewBag.Producers = new SelectList(moviesDropDownValues.Producers, "Id", "FullName");
                return View(newMovie);

            }
            await moviesService.AddNewMovie(newMovie);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var movieData = await moviesService.GetMovieDataByIdAsync(id);
            if (movieData == null) return View("NotFound");
            var MoviesModelVM = new MoviesModelVM
            {
                Id = movieData.Id,
                Name = movieData.Name,
                Description = movieData.Description,
                Price = movieData.Price,
                ImageURL = movieData.ImageURL,
                StartDate = movieData.StartDate,
                EndDate = movieData.EndDate,
                MovieCategory = movieData.MovieCategory,
                CinemaId = movieData.CinemaId,
                ProducerId = movieData.ProducerId,
                ActorIds = movieData.Actors_Movies.Where(n => n.ActorId == movieData.Id).Select(a => a.ActorId).ToList()
            };
            var moviesDropDownValues = await moviesService.GetMoviesDropDownList();
            ViewBag.Cinemas = new SelectList(moviesDropDownValues.Cinemas, "Id", "CinemaName");
            ViewBag.Actors = new SelectList(moviesDropDownValues.Actors, "Id", "FullName");
            ViewBag.Producers = new SelectList(moviesDropDownValues.Producers, "Id", "FullName");
                

            return View(MoviesModelVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MoviesModelVM updateMovieData)
        {
            if (updateMovieData == null) return View("NotFound");
            if (!ModelState.IsValid)
            {
                var moviesDropDownValues = await moviesService.GetMoviesDropDownList();
                ViewBag.Cinemas = new SelectList(moviesDropDownValues.Cinemas, "Id", "CinemaName");
                ViewBag.Actors = new SelectList(moviesDropDownValues.Actors, "Id", "FullName");
                ViewBag.Producers = new SelectList(moviesDropDownValues.Producers, "Id", "FullName");
                return View(updateMovieData);

            }
            await moviesService.UpdateMovie(id, updateMovieData);
            return RedirectToAction(nameof(Index));


        }
    }
}
