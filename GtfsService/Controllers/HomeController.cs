using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GtfsService.Models;

namespace GtfsService.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStopRepository stopRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public HomeController() : this(new StopRepository())
        {
        }

        public HomeController(IStopRepository stopRepository)
        {
			this.stopRepository = stopRepository;
        }

        public ViewResult Index(string term = null)
        {
            if (term != null)
            {
                return IndexPost(term);
            }
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ViewResult IndexPost(string somevalue)
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
            var filteredItems = items.Where(item => item.StartsWith(term)).Distinct();
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

    }
}
