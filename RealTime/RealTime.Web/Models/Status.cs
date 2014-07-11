using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealTime.Web.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string StatusText { get; set; }
        public DateTime StatusDate { get; set; }
        public List<string> Liked { get; set; }
    }
}