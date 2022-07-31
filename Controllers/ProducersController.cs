using eTickets.Data.Services;
using eTickets.Models;
using eTickets.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private IProducersService producersService; 
        public ProducersController(IProducersService producersService)
        {
            this.producersService = producersService;
        }

    
        public IActionResult Index()
        {
            var allActorsData = producersService.GetAllData();
            return View(allActorsData);
        }

        //filter..

        public IActionResult Filter(string searchString)
        {
            var allActorsData = producersService.GetAllData();
            if (!string.IsNullOrEmpty(searchString))
            {
                var data = allActorsData.Where(n => n.FullName.Contains(searchString)).ToList();
                return View("Index", data);
            }
            return View("Index", allActorsData);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("ProfilePictureUrl,FullName,Bio")] Producer producer)
        {

            if (!ModelState.IsValid)
            {
                return View(producer);

            }

            producersService.Add(producer);
            //return View(actor);

            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Details(int id)
        {
            var actorData = await producersService.GetDataById(id);
            if (actorData == null)
            {
                return View("Empty");

            }
            return View(actorData);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var actorData = await producersService.GetDataById(id);
            if (actorData == null)
            {
                return View("Empty");

            }
            return View(actorData);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FullName, ProfilePictureUrl, Bio")] Producer producer)
        {

            if (!ModelState.IsValid)
            {
                return View(producer);

            }
            await producersService.Update(id, producer);
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int id)
        {
            var actorData = await producersService.GetDataById(id);
            if (actorData == null)
            {
                return View("Empty");

            }
            return View(actorData);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await producersService.GetDataById(id);
            if (actorDetails == null) return View("NotFound");
            await producersService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
