using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GtfsService.Models;

namespace GtfsService.Controllers
{   
    public class TripController : Controller
    {
		private readonly ITripRepository tripRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public TripController() : this(new TripRepository())
        {
        }

        public TripController(ITripRepository tripRepository)
        {
			this.tripRepository = tripRepository;
        }

        //
        // GET: /Trip/

        public ViewResult Index()
        {
            return View(tripRepository.All);
        }

        //
        // GET: /Trip/Details/5

        public ViewResult Details(int id)
        {
            return View(tripRepository.Find(id));
        }

        //
        // GET: /Trip/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Trip/Create

        [HttpPost]
        public ActionResult Create(Trip trip)
        {
            if (ModelState.IsValid) {
                tripRepository.InsertOrUpdate(trip);
                tripRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Trip/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(tripRepository.Find(id));
        }

        //
        // POST: /Trip/Edit/5

        [HttpPost]
        public ActionResult Edit(Trip trip)
        {
            if (ModelState.IsValid) {
                tripRepository.InsertOrUpdate(trip);
                tripRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Trip/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(tripRepository.Find(id));
        }

        //
        // POST: /Trip/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tripRepository.Delete(id);
            tripRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                tripRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

