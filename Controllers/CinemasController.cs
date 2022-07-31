using eTickets.Data.Services;
using eTickets.Models;
using eTickets.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        
        
        private ICinemasService cinemasService;
        public CinemasController(ICinemasService cinemasService)
        {
            this.cinemasService = cinemasService;
        }


        public IActionResult Index()
        {
            var allData = cinemasService.GetAllData();
            return View(allData);
        }

        public IActionResult Filter(string searchString)
        {
            var allActorsData = cinemasService.GetAllData();
            if (!string.IsNullOrEmpty(searchString))
            {
                var data = allActorsData.Where(n => n.CinemaName.Contains(searchString) || n.CinemaDescription.Contains(searchString)).ToList();
                return View("Index", data);
            }
            return View("Index", allActorsData);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("CinemaImageUrl,CinemaName,CinemaDescription")] Cinema cinema)
        {

            if (!ModelState.IsValid)
            {
                return View(cinema);

            }

            cinemasService.Add(cinema);
            //return View(actor);

            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Details(int id)
        {
            var actorData = await cinemasService.GetDataById(id);
            if (actorData == null)
            {
                return View("Empty");

            }
            return View(actorData);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var actorData = await cinemasService.GetDataById(id);
            if (actorData == null)
            {
                return View("Empty");

            }
            return View(actorData);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, CinemaImageUrl,CinemaName,CinemaDescription")] Cinema cinema)
        {

            if (!ModelState.IsValid)
            {
                return View(cinema);

            }
            await cinemasService.Update(id, cinema);
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int id)
        {
            var actorData = await cinemasService.GetDataById(id);
            if (actorData == null)
            {
                return View("Empty");

            }
            return View(actorData);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await cinemasService.GetDataById(id);
            if (actorDetails == null) return View("NotFound");
            await cinemasService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
