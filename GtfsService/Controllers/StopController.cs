using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GtfsService.Models;

namespace GtfsService.Controllers
{   
    public class StopController : Controller
    {
		private readonly IStopRepository stopRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public StopController() : this(new StopRepository())
        {
        }

        public StopController(IStopRepository stopRepository)
        {
			this.stopRepository = stopRepository;
        }

        //
        // GET: /Stop/

        public ViewResult Index()
        {
            return View(stopRepository.All);
        }

        //
        // GET: /Stop/Details/5

        public ViewResult Details(int id)
        {
            return View(stopRepository.Find(id));
        }

        public ViewResult Search()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Search(string somevalue)
        {
            string stopsHtml = "";
            var stops = stopRepository.All.Where(s => s.Name.Equals(somevalue));
            if (stops.Any())
            {
                foreach (var stop1 in stops)
                {
                    stopsHtml += new System.Net.WebClient().DownloadString(stop1.Url);
                }   
            }
            ViewBag.SearchResult = stopsHtml;
            return View();
        }

        public ActionResult Autocomplete(string term)
        {
            var items = stopRepository.All.Select(stop => stop.Name);
            var filteredItems = items.Where( item => item.StartsWith(term) ).Distinct();
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Stop/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Stop/Create

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
        // GET: /Stop/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(stopRepository.Find(id));
        }

        //
        // POST: /Stop/Edit/5

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
        // GET: /Stop/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(stopRepository.Find(id));
        }

        //
        // POST: /Stop/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            stopRepository.Delete(id);
            stopRepository.Save();

            return RedirectToAction("Index");
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

