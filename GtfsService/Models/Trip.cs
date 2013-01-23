using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GtfsService.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public int TripNumber { get; set; }
        public string Headsign { get; set; }
        public int ShapeId { get; set; }
        public int ServiceId { get; set; }
    }
}