using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GtfsService.Models
{
    public class Upload
    {
        public int Id { get; set; }
        public HttpPostedFile File { get; set; }
    }
}