using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GtfsService.Models;

namespace GtfsService.Controllers
{
    public class UploadController : Controller
    {
        private readonly IStopRepository stopRepository;

        public UploadController() : this(new StopRepository())
        {
        }

        public UploadController(IStopRepository stopRepository)
        {
			this.stopRepository = stopRepository;
        }




        //
        // GET: /Upload/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Upload/Create

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase filnavn)
        {
            if (filnavn != null)
            {
                bool skipLine = true;
                foreach (string line in ReadFrom(filnavn))
                {
                   if (skipLine)
                   { // skip first line since it only contains name of header
                       skipLine = false;
                       continue;
                   }
                   var readValues = line.Split(",".ToCharArray());
                    var stop = new Stop
                                   {
                                       Lat = readValues[0],
                                       ZoneId = readValues[1],
                                       Lon = readValues[2],
                                       Url = readValues[3],
                                       StopNumber = (readValues[4]),
                                       Description = readValues[5],
                                       Name = readValues[6],
                                       LocationType = (readValues[7])
                                   };
                   stopRepository.InsertOrUpdate(stop);
                }
                stopRepository.Save();


                return RedirectToAction("Create");
            }

            return View();
        }

        static IEnumerable<string> ReadFrom(HttpPostedFileBase file)
        {
            string line;
            using (var sr = new StreamReader(file.InputStream))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

 


        protected override void Dispose(bool disposing)
        {
            stopRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}