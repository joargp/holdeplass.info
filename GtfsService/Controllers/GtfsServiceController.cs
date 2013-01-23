using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GtfsService.Models;


namespace GtfsService.Controllers
{   
    public class GtfsServiceController : Controller
    {
		private readonly IStopRepository stopRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public GtfsServiceController() : this(new StopRepository())
        {
        }

        public GtfsServiceController(IStopRepository stopRepository)
        {
			this.stopRepository = stopRepository;
        }

        //
        // GET: /GtfsService/

        public ViewResult Index()
        {
            return View(stopRepository.All);
        }

        //
        // GET: /GtfsService/Details/5

        public ViewResult Details(int id)
        {
            return View(stopRepository.Find(id));
        }

        //
        // GET: /GtfsService/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /GtfsService/Create

        [HttpPost]
        public ActionResult Create(Stop stop)
        {
            if (ModelState.IsValid) {
                stopRepository.InsertOrUpdate(stop);
                stopRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /GtfsService/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(stopRepository.Find(id));
        }

        //
        // POST: /GtfsService/Edit/5

        [HttpPost]
        public ActionResult Edit(Stop stop)
        {
            if (ModelState.IsValid) {
                stopRepository.InsertOrUpdate(stop);
                stopRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /GtfsService/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(stopRepository.Find(id));
        }

        //
        // POST: /GtfsService/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            stopRepository.Delete(id);
            stopRepository.Save();

            return RedirectToAction("Index");
        }

        static IEnumerable<string> ReadFrom(string file)
        {
            string line;
            using (var reader = System.IO.File.OpenText(file))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                stopRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

