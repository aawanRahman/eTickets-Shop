using eTickets.Models;
using eTickets.Models.Data;
using eTickets.Models.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private IActorsService actorsService;
        public ActorsController(IActorsService actorsService)
        {
           this.actorsService = actorsService;
        }
     
        public IActionResult Index()
        {
            var allActorsData =  actorsService.GetAllData(); 
            return View(allActorsData);
        }

        //Filter
        public IActionResult Filter(string searchString)
        {
            var allActorsData = actorsService.GetAllData();
            if (!string.IsNullOrEmpty(searchString))
            {
                var data = allActorsData.Where(n => n.FullName.Contains(searchString)).ToList();
                return View("Index", data);
            }
            return View("Index",allActorsData);
        }



        public IActionResult Create()
        {
            
            return View();
        }
       
        [HttpPost]
        public IActionResult Create( [Bind("ProfilePictureUrl,FullName,Bio")] Actor actor)
        {

            if (!ModelState.IsValid)
            {
                return View(actor);

            }

            actorsService.Add(actor);
                //return View(actor);
          
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Details(int id)
        {
            var actorData = await actorsService.GetDataById(id);
            if(actorData == null)
            {
               return  View("Empty");

            }
            return View(actorData);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var actorData = await actorsService.GetDataById(id);
            if (actorData == null)
            {
                return View("Empty");

            }
            return View(actorData);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FullName, ProfilePictureUrl, Bio")] Actor actor)
        {

            if (!ModelState.IsValid)
            {
                return View(actor);
                
            }
            await actorsService.Update(id, actor);
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int id)
        {
            var actorData = await actorsService.GetDataById(id);
            if (actorData == null)
            {
                return View("Empty");

            }
            return View(actorData);
        }
        [HttpPost, ActionName ("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await actorsService.GetDataById(id);
            if (actorDetails == null) return View("NotFound");
            await actorsService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
