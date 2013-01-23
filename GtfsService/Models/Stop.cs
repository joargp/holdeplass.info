using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GtfsService.Models
{
    public class Stop
    {
        public int Id { get; set; }
        public string Lat { get; set; }
        public string ZoneId { get; set; }
        public string Lon { get; set; }
        public string Url { get; set; }
        public string StopNumber { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string LocationType { get; set; }
    }
}